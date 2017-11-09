using Microsoft.AspNetCore.Mvc;
using System;

using miraCakesApp.Models;

namespace miraCakesApp.Controllers
{
    public class AdminController: Controller
    {
        public IActionResult Login()
        {
            return View("~/Views/Admin/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(LoginModel model) 
        {
            Console.WriteLine(model.Username, model.Password);
            return RedirectToAction("Index");
        }

        public IActionResult Index() {
            return View();
        }
    }

}