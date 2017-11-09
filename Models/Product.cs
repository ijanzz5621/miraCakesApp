using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

using miraCakesApp.Modules;

namespace miraCakesApp.Models
{
    public class Product
    {
        public System.UInt32 ProductId { get; set; }
        public string ProductName { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public Product(AppDb db=null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `products` (`product_id`, `product_name`) VALUES (@product_id, @product_name);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            ProductId = (System.UInt32) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `products` SET `product_name` = @product_name WHERE `product_id` = @product_id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `products` WHERE `product_id` = @product_id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@product_id",
                DbType = DbType.UInt32,
                Value = ProductId,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@product_name",
                DbType = DbType.String,
                Value = ProductName,
            });
            // cmd.Parameters.Add(new MySqlParameter
            // {
            //     ParameterName = "@content",
            //     DbType = DbType.String,
            //     Value = Content,
            // });
        }

    }
}