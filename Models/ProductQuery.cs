using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using miraCakesApp.Modules;

namespace miraCakesApp.Models
{
    public class ProductQuery
    {
        public readonly AppDb Db;
        public ProductQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Product> FindOneAsync(int productid)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT `product_id`, `product_name` FROM `products` WHERE `product_id` = @product_id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@product_id",
                DbType = DbType.UInt32,
                Value = productid,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Product>> LatestProductsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `product_id`, `product_name` FROM `products` ORDER BY `product_id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM `products`";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Product>> ReadAllAsync(DbDataReader reader)
        {
            var products = new List<Product>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var product = new Product(Db)
                    {
                        ProductId = await reader.GetFieldValueAsync<System.UInt32>(0),
                        ProductName = await reader.GetFieldValueAsync<string>(1)
                    };
                    products.Add(product);
                }
            }
            return products;
        }
    }
}