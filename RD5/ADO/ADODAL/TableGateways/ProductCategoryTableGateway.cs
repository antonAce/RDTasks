using System.Data.SqlClient;
using System.Collections.Generic;

using ADODAL.Models;

namespace ADODAL.TableGateways
{
    public class ProductCategoryTableGateway : GatewayTemplate<ProductCategory, int>
    {
        public ProductCategoryTableGateway(string connectionName) : base(connectionName) {}

        public override void Add(ProductCategory entity)
        {
            SqlParameter nameParam = new SqlParameter("@name", entity.Name);

            command.CommandText = "INSERT INTO [ProductCategory] ([category_name]) VALUES(@name);";
            command.Parameters.Add(nameParam);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public override void Delete(ProductCategory entity)
        {
            SqlParameter idParam = new SqlParameter("@id", entity.Id);
            SqlParameter nameParam = new SqlParameter("@name", entity.Name);

            command.CommandText = "DELETE FROM [ProductCategory] WHERE [category_id] = @id AND [category_name] = @name;";
            command.Parameters.AddRange(new SqlParameter[] { idParam, nameParam });
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public override void DeleteByKey(int key)
        {
            SqlParameter idParam = new SqlParameter("@id", key);
            command.CommandText = "DELETE FROM [ProductCategory] WHERE [category_id] = @id;";
            command.Parameters.Add(idParam);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public override IEnumerable<ProductCategory> GetAll()
        {
            Queue<ProductCategory> resultQueue = new Queue<ProductCategory>();

            command.CommandText = "SELECT [category_id], [category_name] FROM [ProductCategory];";
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                    resultQueue.Enqueue(new ProductCategory { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }

            return resultQueue;
        }

        public override ProductCategory GetByKey(int key)
        {
            SqlParameter idParam = new SqlParameter("@id", key);
            command.CommandText = "SELECT [category_id], [category_name] FROM [ProductCategory] WHERE [category_id] = @id;";
            command.Parameters.Add(idParam);
            reader = command.ExecuteReader();

            command.Parameters.Clear();

            if (reader.HasRows)
                return new ProductCategory { Id = reader.GetInt32(0), Name = reader.GetString(1) };
            else
                return null;
        }

        public override void Update(ProductCategory entity)
        {
            SqlParameter idParam = new SqlParameter("@id", entity.Id);
            SqlParameter nameParam = new SqlParameter("@name", entity.Name);

            command.CommandText = "UPDATE [ProductCategory] SET [category_name] = @name WHERE [category_id] = @id;";
            command.Parameters.AddRange(new SqlParameter[] { idParam, nameParam });
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }
    }
}
