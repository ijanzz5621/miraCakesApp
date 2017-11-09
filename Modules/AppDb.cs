using System;
using MySql.Data.MySqlClient;

namespace miraCakesApp.Modules
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection;
        private String connStr;

        public AppDb()
        {
            Connection = new MySqlConnection(connStr);
        }

        public AppDb(string connectionString)
        {
            connStr = connectionString;
            Console.WriteLine(connectionString);
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}