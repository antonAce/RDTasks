using System;
using System.Linq;
using Trees;
using Primitives;
using System.IO;
using System.Xml.Serialization;

namespace RDTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "dbcore.xml";
            bool cycleProgram = true;
            XmlSerializer formatter = new XmlSerializer(typeof(Test[]));

            Console.WriteLine("TEST SYSTEM 'Sirik-9000' initialised");
            Console.WriteLine("");

            BTree<Test> tests = new BTree<Test>(default(Test));

            while (cycleProgram)
            {
                string query = "";
                Console.Write("> ");
                query = Console.ReadLine();
                Console.Write("");
                var queryTokens = query.Split(' ');

                switch (queryTokens[0])
                {
                    case "exit":
                        {
                            Console.WriteLine("Exiting program...");
                            cycleProgram = false;
                            break;
                        }
                    case "create":
                        {
                            try
                            {
                                if (tests.Value is null)
                                    tests.Value = new Test { StudentName = queryTokens[1].ToString(), TestName = queryTokens[2].ToString(), TestPassDate = DateTime.Parse(queryTokens[3].ToString()), Mark = int.Parse(queryTokens[4]) };
                                else
                                    tests.BranchLeft(new Test { StudentName = queryTokens[1].ToString(), TestName = queryTokens[2].ToString(), TestPassDate = DateTime.Parse(queryTokens[3].ToString()), Mark = int.Parse(queryTokens[4]) });
                            }
                            catch
                            {
                                Console.WriteLine("Invalid arguments!");
                            }

                            break;
                        }
                    case "read":
                        {
                            try
                            {
                                if (queryTokens[1] == "-a")
                                {
                                    int i = 0;
                                    var resultquery = tests.ToArray();
                                    Console.WriteLine("All records:");
                                    foreach (Test item in resultquery)
                                    {
                                        Console.WriteLine($"{i}. '{item.StudentName}' wrote the test '{item.TestName}' in {item.TestPassDate.Year}-{item.TestPassDate.Month}-{item.TestPassDate.Day} with total points {item.Mark.ToString()}");
                                        i++;
                                    }
                                    Console.WriteLine("");
                                }
                                else if (queryTokens[1] == "-p")
                                {
                                    var resultquery = from t in tests.ToArray()
                                                      orderby t.Mark descending
                                                      where t.Mark >= int.Parse(queryTokens[2])
                                                      select t;

                                    Console.WriteLine($"Students, that passed {queryTokens[2]} mark criteria:");
                                    foreach (Test item in resultquery)
                                        Console.WriteLine($"'{item.StudentName}' wrote the test '{item.TestName}' in {item.TestPassDate.Year}-{item.TestPassDate.Month}-{item.TestPassDate.Day} with total points {item.Mark.ToString()}");
                                    Console.WriteLine("");
                                }
                                else if (queryTokens[1] == "-i")
                                {
                                    var resultquery = tests.ToArray()[int.Parse(queryTokens[2])];

                                    Console.WriteLine($"Student with index {int.Parse(queryTokens[2])}");
                                    Console.WriteLine($"'{resultquery.StudentName}' wrote the test '{resultquery.TestName}' in {resultquery.TestPassDate.Year}-{resultquery.TestPassDate.Month}-{resultquery.TestPassDate.Day} with total points {resultquery.Mark.ToString()}");
                                    Console.WriteLine("");
                                }
                                else if (queryTokens[1] == "-c")
                                {
                                    int resultquery = tests.ToArray().Length;
                                    Console.WriteLine($"Records in DB {resultquery}");
                                    Console.WriteLine("");
                                }
                                else if (queryTokens[1] == "-s")
                                {
                                    int i = 0;
                                    var resultquery = from t in tests.ToArray()
                                                      orderby t.Mark descending
                                                      select t;

                                    Console.WriteLine("Sorted records:");
                                    foreach (Test item in resultquery)
                                    {
                                        Console.WriteLine($"{i}. '{item.StudentName}' wrote the test '{item.TestName}' in {item.TestPassDate.Year}-{item.TestPassDate.Month}-{item.TestPassDate.Day} with total points {item.Mark.ToString()}");
                                        i++;
                                    }
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid arguments!");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Invalid arguments!");
                            }

                            break;
                        }
                    case "update":
                        {
                            try
                            {
                                if (queryTokens[1] == "-n")
                                    tests[int.Parse(queryTokens[2])].Value.StudentName = queryTokens[3].ToString();
                                else if (queryTokens[1] == "-t")
                                    tests[int.Parse(queryTokens[2])].Value.TestName = queryTokens[3].ToString();
                                else if (queryTokens[1] == "-d")
                                    tests[int.Parse(queryTokens[2])].Value.TestPassDate = DateTime.Parse(queryTokens[3]);
                                else if (queryTokens[1] == "-m")
                                    tests[int.Parse(queryTokens[2])].Value.Mark = int.Parse(queryTokens[3]);
                                else
                                    Console.WriteLine("Invalid arguments!");
                            }
                            catch
                            {
                                Console.WriteLine("Invalid arguments!");
                            }
                            break;
                        }
                    case "drop":
                        {
                            try
                            {
                                if ((tests[int.Parse(queryTokens[1]) - 1]?.LeftLink?.Value is null))
                                    tests[int.Parse(queryTokens[1]) - 1] = new BTerminal<Test>(default(Test));
                                else
                                    tests[int.Parse(queryTokens[1]) - 1] = tests[int.Parse(queryTokens[1]) - 1].LeftLink;
                            }
                            catch
                            {
                                Console.WriteLine("Invalid arguments!");
                            }

                            break;
                        }
                    case "commit":
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                                formatter.Serialize(fs, tests.ToArray());
                            break;
                        }
                    case "fetch":
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                tests = new BTree<Test>(default(Test));
                                Test[] fetched_tests = (Test[])formatter.Deserialize(fs);
                                tests.Value = fetched_tests[0];
                                for (int i = 1; i < fetched_tests.Length; i++)
                                    tests.BranchLeft(fetched_tests[i]);
                            }
                            break;
                        }
                    case "commands":
                        {
                            Console.WriteLine("'create <student_name> <test_name> <yyyy-mm-dd> <mark>' - write dowm new record");
                            Console.WriteLine("'read [-i <index>] [-p <mark>] [-a] [-c] [-s]' - get record (-i: by index, -p: students that passed the criteria, -a: all records, -c: count of records, -s: sorted records)");
                            Console.WriteLine("'update [-n] [-t] [-d] [-m] <index> <new_value>' - change record by index ('-n' students name, '-t' test name, '-d' date, '-m' mark)");
                            Console.WriteLine("'drop <index>' - delete data with index");
                            Console.WriteLine("'commit' - serialize data");
                            Console.WriteLine("'fetch' - deserialize data");
                            Console.WriteLine("'exit' - leave the system");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid query");
                            break;
                        }
                }
            }
        }
    }
}
