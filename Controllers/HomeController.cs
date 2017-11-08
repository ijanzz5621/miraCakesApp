using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using miraCakesApp.Models;

using MySql.Data.MySqlClient;

namespace miraCakesApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //TestMySql();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(){
            return View ("Views/Home/Login2.cshtml");
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel){
            string username = loginModel.Username;
            string password = loginModel.Password;

            Console.WriteLine("Username is: " + username);

            return RedirectToAction("/");
        }

        private void TestMySql(){

            MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = "server=localhost;user id=root;password=ch@rm1n9;port=3306;database=miracakesdb"
            };

            try
            {
                
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM product;", connection);
    
                using (MySqlDataReader reader =  command.ExecuteReader())
                {
                    System.Console.WriteLine("Product ID\t\tProduct Name");
                    while (reader.Read())
                    {
                    string row = $"{reader["productid"]}\t\t{reader["productname"]}";
                    System.Console.WriteLine(row);
                    }
                }
    
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                connection.Close();
            }
 
            System.Console.ReadKey();   

        }
    }
}
