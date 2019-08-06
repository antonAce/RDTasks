using System.Data.SqlClient;
using System.Collections.Generic;

using ADODAL.Models;

namespace ADODAL.TableGateways
{
    public class ProductTableGateway : GatewayTemplate<Product, string>
    {
        public ProductTableGateway(string connectionName) : base(connectionName) { }

        public override void Add(Product entity)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@gtin", entity.GTIN),
                new SqlParameter("@name", entity.Name),
                new SqlParameter("@description", entity.Description),
                new SqlParameter("@price", entity.Price),
                new SqlParameter("@category_id", entity.CategoryId),
                new SqlParameter("@vendor_id", entity.VendorId)
            };

            command.CommandText = "INSERT INTO [Product] ([product_gtin], [product_name], [product_description], [product_price], [category_id], [vendor_id]) VALUES(@gtin, @name, @description, @price, @category_id, @vendor_id)";
            command.Parameters.AddRange(Params);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }

        public override void Delete(Product entity)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@gtin", entity.GTIN),
                new SqlParameter("@name", entity.Name),
                new SqlParameter("@description", entity.Description),
                new SqlParameter("@price", entity.Price),
                new SqlParameter("@category_id", entity.CategoryId),
                new SqlParameter("@vendor_id", entity.VendorId)
            };

            command.CommandText = "DELETE FROM [Product] WHERE [product_gtin] = @gtin AND [product_name] = @name AND [product_description] = @description AND [product_price] = @price AND [category_id] = @category_id AND [vendor_id] = @vendor_id;";
            command.Parameters.AddRange(Params);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }

        public override void DeleteByKey(string key)
        {
            SqlParameter idParam = new SqlParameter("@gtin", key);
            command.CommandText = "DELETE FROM [Product] WHERE [product_gtin] = @gtin;";
            command.Parameters.Add(idParam);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }

        public override IEnumerable<Product> GetAll()
        {
            Queue<Product> resultQueue = new Queue<Product>();

            command.CommandText = "SELECT [product_gtin], [product_name], [product_description], [product_price], [category_id], [vendor_id] FROM [Product];";
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                    resultQueue.Enqueue(new Product {
                        GTIN = reader.GetString(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        CategoryId = reader.GetInt32(4),
                        VendorId = reader.GetInt32(5)
                    });
            }

            return resultQueue;
        }

        public override Product GetByKey(string key)
        {
            SqlParameter idParam = new SqlParameter("@gtin", key);
            command.CommandText = "SELECT [product_gtin], [product_name], [product_description], [product_price], [category_id], [vendor_id] FROM [Product] WHERE [product_gtin] = @gtin;";
            command.Parameters.Add(idParam);
            reader = command.ExecuteReader();

            command.Parameters.Clear();

            if (reader.HasRows)
                return new Product { GTIN = reader.GetString(0), Name = reader.GetString(1), Description = reader.GetString(2), Price = reader.GetDecimal(3), CategoryId = reader.GetInt32(4), VendorId = reader.GetInt32(5) };
            else
                return null;
        }

        public override void Update(Product entity)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@gtin", entity.GTIN),
                new SqlParameter("@name", entity.Name),
                new SqlParameter("@description", entity.Description),
                new SqlParameter("@price", entity.Price),
                new SqlParameter("@category_id", entity.CategoryId),
                new SqlParameter("@vendor_id", entity.VendorId)
            };

            command.CommandText = "UPDATE [Product] SET [product_name] = @name, [product_description] = @description, [product_price] = @price, [category_id] = @category_id, [vendor_id] = @vendor_id WHERE [product_gtin] = @gtin;";
            command.Parameters.AddRange(Params);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }
    }
}
