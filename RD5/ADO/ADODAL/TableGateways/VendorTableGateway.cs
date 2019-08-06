using System.Data.SqlClient;
using System.Collections.Generic;

using ADODAL.Models;

namespace ADODAL.TableGateways
{
    public class VendorTableGateway : GatewayTemplate<Vendor, int>
    {
        public VendorTableGateway(string connectionName) : base(connectionName) { }

        public override void Add(Vendor entity)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@name", entity.Name),
                new SqlParameter("@address", entity.Address)
            };

            command.CommandText = "INSERT INTO [Vendor] ([vendor_name], [vendor_address]) VALUES (@name, @address)";
            command.Parameters.AddRange(Params);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }

        public override void Delete(Vendor entity)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@id", entity.Id),
                new SqlParameter("@name", entity.Name),
                new SqlParameter("@address", entity.Address)
            };

            command.CommandText = "DELETE FROM [Vendor] WHERE [vendor_id] = @id AND [vendor_name] = @name AND [vendor_address] = @address;";
            command.Parameters.AddRange(Params);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }

        public override void DeleteByKey(int key)
        {
            SqlParameter idParam = new SqlParameter("@id", key);
            command.CommandText = "DELETE FROM [Vendor] WHERE [category_id] = @id;";
            command.Parameters.Add(idParam);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }

        public override IEnumerable<Vendor> GetAll()
        {
            Queue<Vendor> resultQueue = new Queue<Vendor>();

            command.CommandText = "SELECT [vendor_id], [vendor_name], [vendor_address] FROM [Vendor];";
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                    resultQueue.Enqueue(new Vendor { Id = reader.GetInt32(0), Name = reader.GetString(1), Address = reader.GetString(2) });
            }

            return resultQueue;
        }

        public override Vendor GetByKey(int key)
        {
            SqlParameter idParam = new SqlParameter("@id", key);
            command.CommandText = "SELECT [vendor_id], [vendor_name], [vendor_address] FROM [dbo].[Vendor] WHERE [vendor_id] = @id;";
            command.Parameters.Add(idParam);
            reader = command.ExecuteReader();

            command.Parameters.Clear();

            if (reader.HasRows)
                return new Vendor { Id = reader.GetInt32(0), Name = reader.GetString(1), Address = reader.GetString(2) };
            else
                return null;
        }

        public override void Update(Vendor entity)
        {
            SqlParameter[] Params = new SqlParameter[]
            {
                new SqlParameter("@id", entity.Id),
                new SqlParameter("@name", entity.Name),
                new SqlParameter("@address", entity.Address)
            };

            command.CommandText = "UPDATE [Vendor] SET [vendor_name] = @name, [vendor_address] = @address WHERE [vendor_id] = @id;";
            command.Parameters.AddRange(Params);
            command.ExecuteNonQuery();

            command.Parameters.Clear();
        }
    }
}
