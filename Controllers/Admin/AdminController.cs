using Microsoft.AspNetCore.Mvc;

namespace miraCakesApp.Controllers
{
    public class AdminController: Controller
    {
        public IActionResult Login()
        {
            return View("~/Views/Admin/Login.cshtml");
        }
    }

}