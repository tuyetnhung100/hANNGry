﻿/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 10/20/2019
    Purpose: To handle user's requests.
*/

using Story1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountLibrary;
using System.Security.Cryptography;
using System.Windows.Forms;
using Story2;

namespace Story1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }

        // Validate user's login (username and password)
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            model.message = "";
            model.errMessage = "";

            if (model.uname == null || model.psw == null)
            {
                model.errMessage = "Login failed";
                return View(model);
            }

            Account myAccount = AccountDB.FindAccount(model.uname);
            if (myAccount==null)
            {
                model.errMessage = "Login failed";
                return View(model);
            }
            else
            {
                model.name = myAccount.Name;
            }

            string userHash = AccountDB.CreateHash(model.psw, myAccount.PasswordSalt);
            model.role = myAccount.Role;
            if (myAccount.Username == model.uname && userHash == myAccount.PasswordHash)
            {
                if (myAccount.Role == Role.Employee || myAccount.Role == Role.Manager) 
                {
                    model.message = "Hi staff!";
                    Story2.Story2 myStory = new Story2.Story2();
                    myStory.ShowDialog();
                }
                else if (myAccount.Role == Role.Subscriber)
                {
                    model.message = "Login successfully";
                }
            }
            else
            {
                model.errMessage = "Login failed";
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Your register page.";
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        // Validate inputs and create an account
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account existingAccount = AccountDB.FindAccount(model.username, model.email);
                if (existingAccount == null)
                {
                    Account myAccount = new Account();
                    myAccount.Username = model.username;
                    myAccount.Name = model.name;
                    myAccount.Email = model.email;
                    myAccount.PasswordHash = model.psw;

                    AccountDB.Add(myAccount);
                    model.message = "Register successfully";

                }
                else if (model.email == existingAccount.Email)
                {
                    model.errMessage = "Account email already exists. Please enter a different Email.";
                }
                else
                {
                    model.errMessage = "Account username already exists. Please enter a different Username.";
                }
            }
            else
            {
                model.errMessage = "Please enter valid information.";
            }
            return View(model);
        }
    }
}