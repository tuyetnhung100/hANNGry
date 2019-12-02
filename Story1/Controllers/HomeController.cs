﻿/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 10/20/2019
    Purpose: To handle user's requests.
*/

using AccountLibrary;
using Management;
using Notifier;
using Story1.Models.ViewModels;
using System;
using System.Threading;
using System.Web.Mvc;
using System.Windows.Forms;

namespace Story1.Controllers
{
    public class HomeController : Controller
    {
        // Display Login webpage.
        [HttpGet]
        public ActionResult Login()
        {
            // If user login successfully then redirect user to Account Settings page
            if (IsLoggedIn())
            {
                return RedirectToAction("UserAccount");
            }
            ViewBag.Title = "Login";

            LoginViewModel model = new LoginViewModel
            {
                uname = "gonghao",
                psw = "Test123!"
            };
            return View(model);
        }

        // Validate user's login (username and password).
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            model.message = "";
            model.errMessage = "";

            // If username and password are blank, display an error msg.
            if (model.uname == null || model.psw == null)
            {
                model.errMessage = "Please enter valid username and password.";
                return View(model);
            }

            Account activatedAccount = AccountDB.FindActivatedAccount(model.uname);
            // If account not found, displays an error msg.
            if (activatedAccount == null)
            {
                model.errMessage = "Please enter valid username and password.";
                return View(model);
            }
            else
            {
                model.name = activatedAccount.Name;
            }

            // Create a string to store entered passwordHash from user.
            string userHash = AccountDB.CreateHash(model.psw, activatedAccount.PasswordSalt);
            model.role = activatedAccount.Role;
            // Compare entered on screen username + passsword with the ones stored in DB.
            if (model.uname == activatedAccount.Username && userHash == activatedAccount.PasswordHash)
            {
                // Validate Roles, if staff then goes to Story2.
                if (activatedAccount.Role == Role.Employee || activatedAccount.Role == Role.Manager)
                {
                    model.message = "Hi staff!";
                    Thread thread = new Thread(() =>
                    {
                        Form mainForm = new MainForm(activatedAccount);
                        mainForm.ShowDialog();
                    });
                    thread.Start();
                }
                // Validate Roles, if subscriber then promts a success login message.
                else if (activatedAccount.Role == Role.Subscriber)
                {
                    // Special object to store login user in session
                    Session["account"] = activatedAccount;
                    return RedirectToAction("UserAccount");
                }
            }
            // Validate entered username in login.
            else if (model.uname != activatedAccount.Username)
            {
                model.errMessage = "Please enter valid username and password.";
            }
            // Validate entered password in login.
            else if (userHash != activatedAccount.PasswordHash)
            {
                model.errMessage = "Please enter valid username and password.";
            }
            else
            {
                // display an error message if username and password not found in DB.
                model.errMessage = "Please enter valid username and password.";
            }       
            return View(model);
        }

        // Display Register webpage.
        [HttpGet]
        public ActionResult Register()
        {
            if (IsLoggedIn())
            {
                return RedirectToAction("UserAccount");
            }
            ViewBag.Title = "Register";
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        // Validate inputs and create an account.
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate whether account already exists.
                Account existingAccount = AccountDB.CheckAccountAvailability(model.username, model.email);
                // If no match, then create a new account.
                if (existingAccount == null) 
                {
                    Account myAccount = new Account();
                    myAccount.Username = model.username;
                    myAccount.Name = model.name;
                    myAccount.Email = model.email;
                    myAccount.PasswordHash = model.psw;
                    myAccount.PhoneNumber = model.phoneNbr;
                    myAccount.Code = Guid.NewGuid().ToString();
                    AccountDB.Add(myAccount);
                    string subject = "Please confirm your email.";
                    string url = "http://localhost:4841/Home/Activated?code=" + myAccount.Code;
                    string body = "Hi, " + myAccount.Name + @"

<p><a href='" + url + @"'>" + url + @"<a/></p>

hANNGry
";       
                    EmailNotifier.SendHtmlEmail(myAccount.Email, subject, body);
                    model.message = "Register successfully";
                }
                // Validate for an existing email.
                else if (model.email == existingAccount.Email) 
                {
                    model.errMessage = "Email already exists. Please enter a new one.";
                }
                // Validate for an existing username.
                else if (model.username == existingAccount.Username) 
                {
                    model.errMessage = "Username already exists. Please enter a new one.";
                }
                // Validate for an existing phone number.
                else if (model.phoneNbr == existingAccount.PhoneNumber)
                {
                    model.errMessage = "Phone number already exists. Please enter a new one.";
                }
            }
            else
            {
                model.errMessage = "Please enter valid information.";
            }
            return View(model);
        }

        // Validate code and activate account.
        [HttpGet]
        public ActionResult Activated(string code)
        {
            bool isAvtivated = AccountDB.ActivateAccount(code);
            return View(isAvtivated);
        }

        // Display subscriber's information on Account Settings webpage.
        [HttpGet]
        public ActionResult UserAccount()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login");
            }
            ViewBag.Title = "UserAccount";
            // Account account = Session["account"] as Account;
            string username = (Session["account"] as Account).Username;
            Account account = AccountDB.FindActivatedAccount(username);
            UserAccountViewModel model = new UserAccountViewModel();
            model.acctName = account.Name;
            model.acctEmail = account.Email;
            model.acctPhoneNumber = account.PhoneNumber;

            if (account.NotificationType.HasFlag(NotificationType.Email))
            {
                model.isEmailNotiType = true;
            }
            if (account.NotificationType.HasFlag(NotificationType.SMS))
            {
                model.isTextNotiType = true;
            }

            if (account.Location.HasFlag(Location.Sylvania))
            {
                model.isSYLocation = true;
            }
            if (account.Location.HasFlag(Location.RockCreek))
            {
                model.isRCLocation = true;
            }
            if (account.Location.HasFlag(Location.Cascade))
            {
                model.isCASLocation = true;
            }
            if (account.Location.HasFlag(Location.Southeast))
            {
                model.isSELocation = true;
            }
            
            return View(model);
        }

        // Update account settings.
        [HttpPost]
        public ActionResult UserAccount(UserAccountViewModel model)
        {
            Account existingAccount;
            Account account = Session["account"] as Account;
            bool emailChanged = model.acctEmail != account.Email;
            bool phoneChanged = model.acctPhoneNumber != account.PhoneNumber;
            if (emailChanged || phoneChanged)
            {
                //existingAccount = AccountDB.SelectAccount(model.acctEmail, model.acctPhoneNumber);

                //if(existingAccount != null)
                //{
                //    model.errMessage = "Email or Phone already exist. Please enter a new one.";
                //    return View(model);
                //}

                existingAccount = AccountDB.CheckValidInfo(model.acctEmail, model.acctPhoneNumber);

                if (existingAccount.EmailCount != 0)
                {
                    model.errMessage = "Email already exists. Please enter a new one.";
                }
                else if (existingAccount.PhoneNumberCount != 0)
                {
                    model.errMessage = "Phone Number already exists. Please enter a new one.";
                }
            }
            Account updatedAccount = new Account
            {
                Name = model.acctName,
                Email = model.acctEmail,
                PhoneNumber = model.acctPhoneNumber,
                AccountId = account.AccountId
            };

            if (model.isEmailNotiType)
            {
                updatedAccount.NotificationType = updatedAccount.NotificationType | NotificationType.Email;
            }
            if (model.isTextNotiType)
            {
                updatedAccount.NotificationType = updatedAccount.NotificationType | NotificationType.SMS;
            }
            if (model.isSYLocation)
            {
                updatedAccount.Location = updatedAccount.Location | Location.Sylvania;
            }
            if (model.isRCLocation)
            {
                updatedAccount.Location = updatedAccount.Location | Location.RockCreek;
            }
            if (model.isCASLocation)
            {
                updatedAccount.Location = updatedAccount.Location | Location.Cascade;
            }
            if (model.isSELocation)
            {
                updatedAccount.Location = updatedAccount.Location | Location.Southeast;
            }
            AccountDB.Update(updatedAccount);
            model.message = "Saved successfully";
          
            return View(model);
        }

        // Logout from Account Settings and Display a logout webpage.
        [HttpGet]
        public ActionResult Logout()
        {
            ViewBag.Title = "Logout";
            Session["account"] = null;
            return View();
        }

        // Display Cancel Account webpage.
        [HttpGet]
        public ActionResult Unsubscribe()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login");
            }
            ViewBag.Title = "Unsubscribe";
            Account account = Session["account"] as Account;
            UnsubscribeViewModel model = new UnsubscribeViewModel();
            model.uname = account.Username;
            return View(model);
        }

        // Delete unsubscribe account.
        [HttpPost]
        public ActionResult Unsubscribe(UnsubscribeViewModel model)
        {
            model.message = "";

            Account account = Session["account"] as Account;
            Account myAccount = new Account
            {
                Username = model.uname,
                AccountId = account.AccountId
            };
            AccountDB.Delete(myAccount);
            model.message = "Account deleted";
            Logout();
            return View(model);
        }

        // Display Change Password webpage.
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login");
            }
            ViewBag.Title = "ChangePassword";
            Account account = Session["account"] as Account;
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            model.name = account.Name;
            return View(model);
        }

        // Update password.
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            model.message = "";
            model.errMessage = "";

            Account account = Session["account"] as Account;           
            Account myAccount = new Account
            {
                PasswordHash = model.psw,
                AccountId = account.AccountId
            };
            AccountDB.UpdatePassword(myAccount);
            model.message = "Reset successfully";
            return View(model);
        }

        // Check for logged in account (account in session).
        private bool IsLoggedIn()
        {
            Account account = Session["account"] as Account;
            bool isLoggedIn = account != null;
            return isLoggedIn;
        }

        // Display webpage for user that forgot password.
        [HttpGet]
        public ActionResult ChangePasswordByEmail()
        {
            return View();
        }

        // Email a link to reset password.
        [HttpPost]
        public ActionResult ChangePasswordByEmail(ChangePasswordByEmailViewModel model)
        {
            Account myAccount = AccountDB.FindActivatedAccount(model.username);
            if (myAccount == null)
            {

            }
            else
            {

            }
            myAccount.Code = Guid.NewGuid().ToString();
            AccountDB.UpdateCode(myAccount.Username);

            string subject = "Password Reset Link.";
            string url = "http://localhost:4841/Home/Resetpsw?code=" + myAccount.Code;
            string body = "Hi, " + myAccount.Name + @"

<p><a href='" + url + @"'>" + url + @"<a/></p>

Thank you,
hANNGry
";
            EmailNotifier.SendHtmlEmail(myAccount.Email, subject, body);
            return View(model);
        }

        // After clicking the link in email, user will be asked to enter new password.
        [HttpGet]
        public ActionResult ResetPsw(string code)
        {
            Account myAccount = AccountDB.FindAccountByCode(code);
            return View();          
        }

        //[HttpPost]
        //public ActionResult ResetPsw(string code)
        //{
        //    bool found = AccountDB.ChangePswViaEmail(code);
        //    return View(model);
        //}
    }
}
