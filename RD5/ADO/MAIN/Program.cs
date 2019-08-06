using System;
using System.Collections.Generic;

using ADOBLL.Dependencies;
using ADOBLL.Interfaces;
using ADOBLL.Services;
using ADOBLL.DTO;

using Ninject;
using Ninject.Modules;

using RDTask5.Dependencies;

namespace RDTask5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool appIsRunning = true;

            StandardKernel kernel = new StandardKernel(
                new VendorServiceModule(),
                new ProductServiceModule(),
                new ProductCategoryServiceModule(),
                new ServiceModule("ADOConnection")
            );

            Console.WriteLine(@"/ 'Product manager 9000' CLI \");
            PrintCommands();
            Console.WriteLine("");

            while (appIsRunning)
            {
                Console.Write("> ");
                string inputQuery = Console.ReadLine();
                string[] arguments = inputQuery.Split(" ");

                if (arguments[0] == "help")
                    PrintCommands();
                else if (arguments[0] == "quit")
                    appIsRunning = false;
                else if (arguments[0] == "custom")
                {
                    if (arguments[1] == "-ppc")
                    {
                        using (IProductService ps = kernel.Get<IProductService>())
                        {
                            var products = ps.GetProductsByCategoty(new ProductCategoryDTO { Id = int.Parse(arguments[2]) });
                            Console.WriteLine($"All products, related to the category #{int.Parse(arguments[2])}");
                            PrintProductDTO(products);
                        }
                    }
                    else if (arguments[1] == "-pv")
                    {
                        using (IProductService ps = kernel.Get<IProductService>())
                        {
                            var products = ps.GetProductsOfVendor(new VendorDTO { Id = int.Parse(arguments[2]) });
                            Console.WriteLine($"All products, related to the vendor #{int.Parse(arguments[2])}");
                            PrintProductDTO(products);
                        }
                    }
                    else if (arguments[1] == "-vpc")
                    {
                        using (IVendorService vs = kernel.Get<IVendorService>())
                        {
                            var vendors = vs.GetVendorsByProductCategoty(new ProductCategoryDTO { Id = int.Parse(arguments[2]) });
                            Console.WriteLine($"All vendors, related to the category #{int.Parse(arguments[2])}");
                            PrintVendorsDTO(vendors);
                        }
                    }
                    else if (arguments[1] == "-topvendors")
                    {
                        using (IVendorService vs = kernel.Get<IVendorService>())
                        {
                            var vendors = vs.GetMostVariousCategoriesVendors();
                            Console.WriteLine("All vendors, which products have the biggest ammount of different categories");
                            PrintVendorsDTO(vendors);
                        }
                    }
                }
                else if (arguments[0] == "add")
                {
                    if (arguments[1] == "-vendor")
                    {
                        using (IVendorService vs = kernel.Get<IVendorService>())
                        {
                            try {
                                vs.RegisterVendor(new VendorDTO { Id = int.Parse(arguments[2]), Name = arguments[3], Address = arguments[4] });
                                Console.WriteLine("New vendor was added successfully!");
                            }
                            catch(Exception exception) { Console.WriteLine($"Failed to add new Vendor due to: {exception.Message}"); }
                        }
                    }
                    else if (arguments[1] == "-product")
                    {
                        using (IProductService ps = kernel.Get<IProductService>())
                        {
                            try
                            {
                                ps.RegisterProduct(new ProductDTO {
                                    GTIN = arguments[2],
                                    Name = arguments[3],
                                    Description = arguments[4],
                                    Price = decimal.Parse(arguments[5]),
                                    CategoryId = int.Parse(arguments[6]),
                                    VendorId = int.Parse(arguments[7])
                                });
                                Console.WriteLine("New product was added successfully!");
                            }
                            catch (Exception exception) { Console.WriteLine($"Failed to add new Product due to: {exception.Message}"); }
                        }
                    }
                    else if (arguments[1] == "-category")
                    {
                        using (IProductCategoryService pcs = kernel.Get<IProductCategoryService>())
                        {
                            try
                            {
                                pcs.RegisterCategory(new ProductCategoryDTO { Id = int.Parse(arguments[2]), Name = arguments[3] });
                                Console.WriteLine("New category was added successfully!");
                            }
                            catch (Exception exception) { Console.WriteLine($"Failed to add new Category due to: {exception.Message}"); }
                        }
                    }
                }
                else if (arguments[0] == "delete")
                {
                    if (arguments[1] == "-vendor")
                    {
                        using (IVendorService vs = kernel.Get<IVendorService>())
                        {
                            try
                            {
                                vs.RemoveVendor(new VendorDTO { Id = int.Parse(arguments[2]), Name = arguments[3], Address = arguments[4] });
                                Console.WriteLine("Vendor was deleted successfully!");
                            }
                            catch (Exception exception) { Console.WriteLine($"Failed to delete Vendor due to: {exception.Message}"); }
                        }
                    }
                    else if (arguments[1] == "-product")
                    {
                        using (IProductService ps = kernel.Get<IProductService>())
                        {
                            try
                            {
                                ps.RemoveProduct(new ProductDTO { GTIN = arguments[2], Name = arguments[3], Description = arguments[4], Price = decimal.Parse(arguments[5])});
                                Console.WriteLine("Product was deleted successfully!");
                            }
                            catch (Exception exception) { Console.WriteLine($"Failed to delete Product due to: {exception.Message}"); }
                        }
                    }
                    else if (arguments[1] == "-category")
                    {
                        using (IProductCategoryService pcs = kernel.Get<IProductCategoryService>())
                        {
                            try
                            {
                                pcs.RemoveCategory(new ProductCategoryDTO { Id = int.Parse(arguments[2]), Name = arguments[3] });
                                Console.WriteLine("Category was deleted successfully!");
                            }
                            catch (Exception exception) { Console.WriteLine($"Failed to delete Category due to: {exception.Message}"); }
                        }
                    }
                }
                else if (arguments[0] == "print")
                {
                    if (arguments[1] == "-vendor")
                    {
                        using (IVendorService vs = kernel.Get<IVendorService>())
                        {
                            Console.WriteLine($"All vendors in database");
                            PrintVendorsDTO(vs.GetAllVendors());
                        }
                    }
                    else if (arguments[1] == "-product")
                    {
                        using (IProductService ps = kernel.Get<IProductService>())
                        {
                            Console.WriteLine($"All products in database");
                            PrintProductDTO(ps.GetAllProducts());
                        }
                    }
                    else if (arguments[1] == "-category")
                    {
                        using (IProductCategoryService pcs = kernel.Get<IProductCategoryService>())
                        {
                            Console.WriteLine($"All categories in database");
                            PrintCategoriesDTO(pcs.GetAllCategories());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong query!");
                }
            }
        }

        static void PrintVendorsDTO(IEnumerable<VendorDTO> vendors)
        {
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 50)}+{new string('-', 50)}+");
            Console.WriteLine(string.Format("|{0,8}|{1,50}|{2,50}|", "Id", "Name", "Address"));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 50)}+{new string('-', 50)}+");
            foreach (var vendor in vendors)
                Console.WriteLine(string.Format("|{0,8}|{1,50}|{2,50}|", vendor.Id, vendor.Name.Truncate(50).CleanUp(), vendor.Address.Truncate(50).CleanUp()));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 50)}+{new string('-', 50)}+");
        }

        static void PrintProductDTO(IEnumerable<ProductDTO> products)
        {
            Console.WriteLine($"+{new string('-', 14)}+{new string('-', 25)}+{new string('-', 50)}+{new string('-', 10)}+");
            Console.WriteLine(string.Format("|{0,14}|{1,25}|{2,50}|{3,10}|", "GTIN", "Name", "Description", "Price"));
            Console.WriteLine($"+{new string('-', 14)}+{new string('-', 25)}+{new string('-', 50)}+{new string('-', 10)}+");
            foreach (var item in products)
                Console.WriteLine(string.Format("|{0,14}|{1,25}|{2,50}|{3,10:C2}|", item.GTIN, item.Name.Truncate(25).CleanUp(), item.Description.Truncate(50).CleanUp(), item.Price));
            Console.WriteLine($"+{new string('-', 14)}+{new string('-', 25)}+{new string('-', 50)}+{new string('-', 10)}+");
        }

        static void PrintCategoriesDTO(IEnumerable<ProductCategoryDTO> categories)
        {
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 75)}+");
            Console.WriteLine(string.Format("|{0,8}|{1,75}|", "Id", "Name"));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 75)}+");
            foreach (var category in categories)
                Console.WriteLine(string.Format("|{0,8}|{1,75}|", category.Id, category.Name.Truncate(75).CleanUp()));
            Console.WriteLine($"+{new string('-', 8)}+{new string('-', 75)}+");
        }

        static void PrintCommands()
        {
            Console.WriteLine("Available commands:\n");
            Console.WriteLine("help: print all commands");
            Console.WriteLine("quit: shut down the application");
            Console.WriteLine("custom: execute specific query");
            Console.WriteLine("[-ppc {Id}]: get all products of given product category id");
            Console.WriteLine("[-vpc {Id}]: get all vendors, which products belong to given product category id");
            Console.WriteLine("[-pv {Id}]: get all products, of given vendor id");
            Console.WriteLine("[-topvendors]: get all vendors, which products have the biggest ammount of different categories");
            Console.WriteLine("add: insert new record");
            Console.WriteLine("[-vendor {Id} {Name} {Address}]");
            Console.WriteLine("[-product {Id} {Name} {Description} {Price} {CategoryId} {VendorId}]");
            Console.WriteLine("[-category {Id} {Name}]");
            Console.WriteLine("print [-vendor], [-product], [-category]: get all records");
            Console.WriteLine("delete [-vendor {Id} {Name} {Address}], [-product {Id} {Name} {Description} {Price} {CategoryId} {VendorId}], [-category {Id} {Name}]: delete specific record");
        }
    }

    public static class StringExtend
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string CleanUp(this string value)
        {
            return value.Replace("\r", "").Replace("\n", "");
        }
    }
}
