using System;
using System.Linq;
using System.Collections.Generic;

using EFBLL.Interfaces;
using EFBLL.Dependencies;
using EFBLL.Services;
using EFBLL.DTO;

using Ninject;
using Ninject.Modules;

using RDPresentationLayer.Dependencies;

namespace RDPresentationLayer
{
    public static class StringExtend
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

    class Program
    {
        private const string CONNECTION_FILE_NAME = "dbconfig.json";
        private const string CONNECTION_NAME = "ProductManagerConnection";
        static bool AppIsRunning = true;

        static void Main(string[] args)
        {
            StandardKernel kernel = new StandardKernel(
                new ConnectionByFileModule(CONNECTION_FILE_NAME, CONNECTION_NAME),
                new CategoryServiceModule(),
                new ProductServiceModule(),
                new VendorServiceModule()
            );

            PrintStartScreen();
            PrintCommands();

            while (AppIsRunning)
            {
                Console.WriteLine();
                Console.Write("> ");
                string inputQuery = Console.ReadLine();
                string[] parsedQuery = inputQuery.Split(" ");

                switch (parsedQuery[0])
                {
                    case "quit":
                    {
                        AppIsRunning = false;
                        break;
                    };
                    case "help":
                    {
                        PrintCommands();
                        break;
                    };
                    case "add":
                    {
                        if (parsedQuery[1] == "-vendor")
                        {
                            using (IVendorService vs = kernel.Get<IVendorService>())
                            {
                                Console.Write("Vendor's name:");
                                string vName = Console.ReadLine();

                                Console.Write("Vendor's address:");
                                string vAddress = Console.ReadLine();
                                
                                vs.AddVendor(new VendorDTO { Name = vName.Replace('_', ' '), Address = vAddress.Replace('_', ' ') });
                            }
                        }
                        else if (parsedQuery[1] == "-product")
                        {
                            using (IProductService ps = kernel.Get<IProductService>())
                            {
                                Console.Write("Product's GTIN:");
                                string pGTIN = Console.ReadLine();

                                Console.Write("Product's name:");
                                string pName = Console.ReadLine();

                                Console.Write("Product's description:");
                                string pDescription = Console.ReadLine();

                                Console.Write("Product's price:");
                                decimal pPrice = Convert.ToDecimal(Console.ReadLine());

                                ps.AddProduct(new ProductDTO { GTIN = pGTIN, Name = pName, Description = pDescription, Price = pPrice });
                            }
                        }
                        else if (parsedQuery[1] == "-category")
                        {
                            using (ICategoryService cs = kernel.Get<ICategoryService>())
                            {
                                Console.Write("Category's name:");
                                string cName = Console.ReadLine();

                                cs.AddCategory(new CategoryDTO { Name = cName });
                            }
                        }
                        else
                            Console.WriteLine("Wrong param!");
                        break;
                    }
                    case "delete":
                    {
                        if (parsedQuery[1] == "-vendor")
                        {
                            using (IVendorService vs = kernel.Get<IVendorService>())
                            {
                                vs.RemoveVendor(new VendorDTO { Id = int.Parse(parsedQuery[2]) });
                            }
                        }
                        else if (parsedQuery[1] == "-product")
                        {
                            using (IProductService ps = kernel.Get<IProductService>())
                            {
                                ps.RemoveProduct(new ProductDTO { GTIN = parsedQuery[2] });
                            }
                        }
                        else if (parsedQuery[1] == "-category")
                        {
                            using (ICategoryService cs = kernel.Get<ICategoryService>())
                            {
                                cs.RemoveCategory(new CategoryDTO { Id = int.Parse(parsedQuery[2]) });
                            }
                        }
                        break;
                    }
                    case "product":
                    {
                        using (IProductService ps = kernel.Get<IProductService>())
                        {
                            List<ProductDTO> products = new List<ProductDTO>();

                            if (parsedQuery[1] == "-all")
                                products = ps.GetAll().ToList();
                            else if (parsedQuery[1] == "-gtin")
                            {
                                try { products = ps.GetWhere(p => p.GTIN == parsedQuery[2]).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-ofvendor")
                            {
                                try { products = ps.GetByVendor(new VendorDTO { Id = int.Parse(parsedQuery[2]) }).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-ofcategory")
                            {
                                try { products = ps.GetByCategory(new CategoryDTO { Id = int.Parse(parsedQuery[2]) }).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-startswith")
                            {
                                try { products = ps.GetWhere(p => p.Name.StartsWith(char.Parse(parsedQuery[2]))).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-price")
                            {
                                if (parsedQuery[2] == "max")
                                {
                                    decimal? maxPrice = ps.GetAll().Select(p => p.Price).Max();
                                    products = ps.GetWhere(p => p.Price == maxPrice).ToList();
                                }
                                else if (parsedQuery[2] == "min")
                                {
                                    decimal? minPrice = ps.GetAll().Select(p => p.Price).Min();
                                    products = ps.GetWhere(p => p.Price == minPrice).ToList();
                                }
                                else
                                    Console.WriteLine("Wrong param!");
                            }
                            else if (parsedQuery[1] == "-attachvendor")
                            {
                                try { ps.SetVendor(new ProductDTO { GTIN = parsedQuery[2]}, new VendorDTO { Id = int.Parse(parsedQuery[3]) }); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-attachcategory")
                            {
                                try { ps.SetCategory(new ProductDTO { GTIN = parsedQuery[2] }, new CategoryDTO { Id = int.Parse(parsedQuery[3]) }); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else
                            {
                                Console.WriteLine("Wrong param!");
                                break;
                            }
                            PrintProductDTO(products);
                        }
                        break;
                    }
                    case "vendor":
                    {
                        using (IVendorService vs = kernel.Get<IVendorService>())
                        {
                            List<VendorDTO> vendors = new List<VendorDTO>();

                            if (parsedQuery[1] == "-all")
                                vendors = vs.GetAll().ToList();
                            else if (parsedQuery[1] == "-ofcategory")
                            {
                                try { vendors = vs.GetByCategory(new CategoryDTO { Id = int.Parse(parsedQuery[2]) }).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-location")
                            {
                                try { vendors = vs.GetWhere(v => v.Address == parsedQuery[2].Replace('_', ' ')).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else if (parsedQuery[1] == "-id")
                            {
                                try { vendors = vs.GetWhere(c => c.Id == int.Parse(parsedQuery[2])).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else
                            {
                                Console.WriteLine("Wrong param!");
                                break;
                            }
                            PrintVendorsDTO(vendors);
                        }
                        break;
                    }
                    case "category":
                    {
                        using (ICategoryService cs = kernel.Get<ICategoryService>())
                        {
                            List<CategoryDTO> categories = new List<CategoryDTO>();

                            if (parsedQuery[1] == "-all")
                                categories = cs.GetAll().ToList();
                            else if (parsedQuery[1] == "-id")
                            {
                                try { categories = cs.GetWhere(c => c.Id == int.Parse(parsedQuery[2])).ToList(); }
                                catch (Exception exception) { Console.WriteLine($"Query failed due to: {exception.Message}"); break; }
                            }
                            else
                            {
                                Console.WriteLine("Wrong param!");
                                break;
                            }
                            PrintCategoriesDTO(categories);
                        }
                        break;
                    }
                    default :
                    {
                        Console.WriteLine("This command doesn't exist!");
                        break;
                    }
                }
            }
        }

        static void PrintStartScreen()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(@" ____                _            _     __  __                                      ____ _     ___ ");
            Console.WriteLine(@"|  _ \ _ __ ___   __| |_   _  ___| |_  |  \/  | __ _ _ __   __ _  __ _  ___ _ __   / ___| |   |_ _|");
            Console.WriteLine(@"| |_) | '__/ _ \ / _` | | | |/ __| __| | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '__| | |   | |    | | ");
            Console.WriteLine(@"|  __/| | | (_) | (_| | |_| | (__| |_  | |  | | (_| | | | | (_| | (_| |  __/ |    | |___| |___ | | ");
            Console.WriteLine(@"|_|   |_|  \___/ \__,_|\__,_|\___|\__| |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_|     \____|_____|___|");
            Console.WriteLine(@"                                                                 |___/                             ");

            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintCommands()
        {
            Console.WriteLine("\nAvailable commands:\n");
            Console.WriteLine("'help' - print all available commands");
            Console.WriteLine("'quit' - shut down the application");
            Console.WriteLine("'add [-vendor] [-product] [-category]' - record new data");
            Console.WriteLine("'delete [-vendor {id}] [-product {GTIN}] [-category {id}]' - delete specific data");
            Console.WriteLine("'product");
            Console.WriteLine("[-all]' - get a list of all products");
            Console.WriteLine("[-gtin {gtin}]' - get a product by 'Global Trade Item Number'");
            Console.WriteLine("[-ofvendor {Id}]' - get a list of products, that belong to a specific vendor");
            Console.WriteLine("[-ofcategory {Id}]' - get a list of products, that belong to a specific category");
            Console.WriteLine("[-startswith {symbol}] - get a list of products, whose name starts with symbol");
            Console.WriteLine("[-price max/min] - get product(s) with max/min price");
            Console.WriteLine("[-attachvendor {ProductGTIN} {VendorId}] - set vendor of the product as an owner");
            Console.WriteLine("[-attachcategory {ProductGTIN} {CategoryId}] - set category of the product");
            Console.WriteLine("'vendor");
            Console.WriteLine("[-all]' - get a list of all products");
            Console.WriteLine("[-id {Id}]' - get a vendor by Id");
            Console.WriteLine("[-ofcategory {Id}]' - get a list of vendors, whose products belong to a specific category");
            Console.WriteLine("[-location {address}]' - get a list of vendors, that located at specific address (instead of space symbol use '_' in input, if address contain several words)");
            Console.WriteLine("'category");
            Console.WriteLine("[-all]' - get a list of all product categories");
            Console.WriteLine("[-id {Id}]' - get a product category by Id");
        }

        static void PrintVendorsDTO(IEnumerable<VendorDTO> vendors)
        {
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 50)}+{new string('-', 50)}+");
            Console.WriteLine(string.Format("|{0,8}|{1,50}|{2,50}|", "Id", "Name", "Address"));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 50)}+{new string('-', 50)}+");
            foreach (var vendor in vendors)
                Console.WriteLine(string.Format("|{0,8}|{1,50}|{2,50}|", vendor.Id, vendor.Name.Truncate(50), vendor.Address.Truncate(50)));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 50)}+{new string('-', 50)}+");
        }

        static void PrintProductDTO(IEnumerable<ProductDTO> products)
        {
            Console.WriteLine($"+{new string('-', 14)}+{new string('-', 25)}+{new string('-', 50)}+{new string('-', 10)}+");
            Console.WriteLine(string.Format("|{0,14}|{1,25}|{2,50}|{3,10}|", "GTIN", "Name", "Description", "Price"));
            Console.WriteLine($"+{new string('-', 14)}+{new string('-', 25)}+{new string('-', 50)}+{new string('-', 10)}+");
            foreach (var item in products)
                Console.WriteLine(string.Format("|{0,14}|{1,25}|{2,50}|{3,10:C2}|", item.GTIN, item.Name.Truncate(25), item.Description.Truncate(50), item.Price));
            Console.WriteLine($"+{new string('-', 14)}+{new string('-', 25)}+{new string('-', 50)}+{new string('-', 10)}+");
        }

        static void PrintCategoriesDTO(IEnumerable<CategoryDTO> categories)
        {
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 75)}+");
            Console.WriteLine(string.Format("|{0,8}|{1,75}|", "Id", "Name"));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 75)}+");
            foreach (var category in categories)
                Console.WriteLine(string.Format("|{0,8}|{1,75}|", category.Id, category.Name.Truncate(75)));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 75)}+");
        }
    }
}
