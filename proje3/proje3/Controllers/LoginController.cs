using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string psw)
        {
            if(email == "admin@admin.com" && psw == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.LoginError = "Hatalı kullanıcı adı veya şifre!";
                return View();
            }
        }
    }
}