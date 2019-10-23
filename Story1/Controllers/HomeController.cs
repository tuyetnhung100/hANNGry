using Story1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountLibrary;

namespace Story1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Register()
        {
            ViewBag.Message = "Your register page.";
            RegisterViewModel model = new RegisterViewModel
            {
                name = "foo"
            };
            return View(model);
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            Account myAccount = new Account();

            myAccount.Name = model.name;
            myAccount.Email = model.email;
            myAccount.PasswordHash = model.psw;

            AccountDB.Add(myAccount);

            return View(model);
        }

        public ActionResult Register2()
        {
            ViewBag.Message = "Your register page.";

            return View();
        }

        [HttpPost]
        public ActionResult Register2(RegisterViewModel model)
        {
            ViewBag.Message = "Your register page.";

            return View();
        }
    }
}