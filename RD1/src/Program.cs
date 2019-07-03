using System;
using System.IO;
using Matrixes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;

namespace RDMatrixes
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializableMatrix s1Matrix = new SerializableMatrix(new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            s1Matrix.SerializationFormatter = new BinaryFormatter();
            Console.WriteLine(s1Matrix.ToString());

            s1Matrix.SerializeMatrix("matrix1.dat");
            s1Matrix.DeserializeMatrix("matrix.dat");
  
            Console.WriteLine(s1Matrix.ToString());
            Console.ReadKey();
        }
    }
}
