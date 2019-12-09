/*
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
            LoginViewModel model = new LoginViewModel
            {
                //uname = "gonghao",
                //psw = "Test123!"
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
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        // Validate inputs and create an account.
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.errMessage = "Please enter valid information.";
                return View(model);
            }

            // Validate whether account already exists.
            Account existingAccount = AccountDB.CheckAccountAvailability(model.username, model.email);
            // If no match, then create a new account.
            if (existingAccount == null)
            {
                Account myAccount = new Account
                {
                    Username = model.username,
                    Name = model.name,
                    Email = model.email,
                    PasswordHash = model.psw,
                    PhoneNumber = model.phoneNbr,
                    Carrier = model.carrier
                };

                if (model.isEmailNotiType)
                {
                    myAccount.NotificationType |= NotificationType.Email;
                }
                if (model.isTextNotiType)
                {
                    myAccount.NotificationType |= NotificationType.SMS;
                }
                if (model.isSYLocation)
                {
                    myAccount.Location |= Location.Sylvania;
                }
                if (model.isRCLocation)
                {
                    myAccount.Location |= Location.RockCreek;
                }
                if (model.isCASLocation)
                {
                    myAccount.Location |= Location.Cascade;
                }
                if (model.isSELocation)
                {
                    myAccount.Location |= Location.Southeast;
                }

                myAccount.Code = Guid.NewGuid().ToString();
                AccountDB.CreateAccount(myAccount);
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
            string username = (Session["account"] as Account).Username;
            Account account = AccountDB.FindActivatedAccount(username);
            UserAccountViewModel model = new UserAccountViewModel
            {
                acctName = account.Name,
                acctEmail = account.Email,
                acctPhoneNumber = account.PhoneNumber,
                acctCarrier = account.Carrier
            };

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
            if (!ModelState.IsValid)
            {
                model.errMessage = "Please enter valid information.";
                return View(model);
            }

            Account account = Session["account"] as Account;
            bool emailChanged = model.acctEmail != account.Email;
            bool phoneChanged = model.acctPhoneNumber != account.PhoneNumber;
            CheckValidInfoResult result = null;
            if (emailChanged || phoneChanged)
            {
                result = AccountDB.CheckValidInfo(model.acctEmail, model.acctPhoneNumber);
            }
            if (emailChanged && result.EmailCount > 0)
            {
                model.errMessage = "Email already exists. Please enter a new one.";
                return View(model);
            }
            if (phoneChanged && result.PhoneNumberCount > 0)
            {
                model.errMessage = "Phone Number already exists. Please enter a new one.";
                return View(model);
            }

            Account updatedAccount = new Account
            {
                Name = model.acctName,
                Email = model.acctEmail,
                PhoneNumber = model.acctPhoneNumber,
                Carrier = model.acctCarrier,
                AccountId = account.AccountId
            };

            if (model.isEmailNotiType)
            {
                updatedAccount.NotificationType |= NotificationType.Email;
            }
            if (model.isTextNotiType)
            {
                updatedAccount.NotificationType |= NotificationType.SMS;
            }
            if (model.isSYLocation)
            {
                updatedAccount.Location |= Location.Sylvania;
            }
            if (model.isRCLocation)
            {
                updatedAccount.Location |= Location.RockCreek;
            }
            if (model.isCASLocation)
            {
                updatedAccount.Location |= Location.Cascade;
            }
            if (model.isSELocation)
            {
                updatedAccount.Location |= Location.Southeast;
            }
            AccountDB.UpdateAccount(updatedAccount);
            model.message = "Saved successfully";
            account.Name = updatedAccount.Name;
            account.Email = updatedAccount.Email;
            account.PhoneNumber = updatedAccount.PhoneNumber;
            account.Carrier = updatedAccount.Carrier;
            Session["account"] = account;
            return View(model);
        }

        // Logout from Account Settings and Display a logout webpage.
        [HttpGet]
        public ActionResult Logout()
        {
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
            Account account = Session["account"] as Account;
            UnsubscribeViewModel model = new UnsubscribeViewModel
            {
                uname = account.Username,
                email = account.Email
            };
            return View(model);
        }

        // return a new web form after user deleted account.
        [HttpGet]
        public ActionResult UnsubscribeSucceeded()
        {
            return View();
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
            return RedirectToAction("UnsubscribeSucceeded");
        }

        // Display Change Password webpage.
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login");
            }
            Account account = Session["account"] as Account;
            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                name = account.Name
            };
            return View(model);
        }

        // Update password.
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.errMessage = "Please enter valid information.";
                return View(model);
            }

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
        public ActionResult ForgotPassword()
        {
            if (IsLoggedIn())
            {
                return RedirectToAction("UserAccount");
            }
            return View();
        }

        // Email a link to reset password.
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.errMessage = "Please enter valid information.";
                return View(model);
            }

            Account account = AccountDB.FindActivatedAccount(model.username);
            if (account == null)
            {
                model.errMessage = "Account not found";
                return View(model);
            }

            string code = Guid.NewGuid().ToString();
            AccountDB.UpdateCode(account.Username, code);

            string subject = "Password Reset Link.";
            string url = "http://localhost:4841/Home/ResetPassword?code=" + code;
            string body = "Hi, " + account.Name + @"

<p><a href='" + url + @"'>" + url + @"<a/></p>

Thank you, <br>
hANNGry
";
            EmailNotifier.SendHtmlEmail(account.Email, subject, body);
            model.message = "Link Sent";
            return View(model);
        }

        // After clicking the link in email, user will be asked to enter new password.
        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            if (IsLoggedIn())
            {
                return RedirectToAction("UserAccount");
            }

            Account existingAccount = AccountDB.FindAccountByCode(code);
            if (existingAccount == null)
            {
                return RedirectToAction("Login");
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                name = existingAccount.Name,
                code = code
            };
            return View(model);
        }

        // Reset password via email link.
        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.errMessage = "Please enter valid information.";
                return View(model);
            }

            Account existingAccount = AccountDB.FindAccountByCode(model.code);
            if (existingAccount == null)
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                AccountDB.ResetPassword(model.code, model.psw);
                model.message = "Reset successfully";
            }
            else
            {
                model.errMessage = "Please enter valid information.";
            }
            return View(model);
        }
    }
}
