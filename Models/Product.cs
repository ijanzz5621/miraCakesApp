using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

using miraCakesApp.Modules;

namespace miraCakesApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
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
            cmd.CommandText = @"INSERT INTO `product` (`productid`, `productname`) VALUES (@productid, @productname);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            ProductId = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `product` SET `productname` = @productname WHERE `productid` = @productid;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `product` WHERE `productid` = @productid;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@productid",
                DbType = DbType.Int32,
                Value = ProductId,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@productname",
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