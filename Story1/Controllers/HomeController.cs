/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 10/20/2019
    Purpose: To handle user's requests.
*/

using AccountLibrary;
using Management;
using Story1.Models.ViewModels;
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
            return View();
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
                model.errMessage = "Please enter a valid username and password.";
                return View(model);
            }

            Account myAccount = AccountDB.FindAccount(model.uname);
            // If account not found, displays an error msg.
            if (myAccount == null) 
            {
                model.errMessage = "Please enter a valid username and password.";
                return View(model);
            }
            else
            {
                model.name = myAccount.Name;
            }

            // Create a string to store entered passwordHash from user.
            string userHash = AccountDB.CreateHash(model.psw, myAccount.PasswordSalt); 
            model.role = myAccount.Role;
            // Compare entered on screen username + passsword with the ones stored in DB.
            if (model.uname == myAccount.Username && userHash == myAccount.PasswordHash) 
            {
                // Validate Roles, if staff then goes to Story2.
                if (myAccount.Role == Role.Employee || myAccount.Role == Role.Manager) 
                {
                    model.message = "Hi staff!";
                    Thread thread = new Thread(() =>
                    {
                        Form mainForm = new MainForm(myAccount);
                        mainForm.ShowDialog();
                    });
                    thread.Start();
                }
                // Validate Roles, if subscriber then promts a success login message.
                else if (myAccount.Role == Role.Subscriber) 
                {
                    // Enum to store login user in session
                    Session["account"] = myAccount; 
                    model.message = "Login successfully";
                }
            }
            // Validate entered username in login.
            else if (model.uname != myAccount.Username)
            {
                model.errMessage = "Incorrect username. Please enter a valid username and password.";
            }
            // Validate entered password in login.
            else if (userHash != myAccount.PasswordHash) 
            {
                model.errMessage = "Incorrect password. Please enter a valid username and password.";
            }
            else
            {
                // display an error message if username and password not found in DB.
                model.errMessage = "Please enter a valid username and password."; 
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
                Account existingAccount = AccountDB.FindAccount(model.username, model.email);
                // If no match, then create a new account.
                if (existingAccount == null) 
                {
                    Account myAccount = new Account();
                    myAccount.Username = model.username;
                    myAccount.Name = model.name;
                    myAccount.Email = model.email;
                    myAccount.PasswordHash = model.psw;
                    myAccount.PhoneNumber = "";

                    AccountDB.Add(myAccount);
                    model.message = "Register successfully";
                }
                // Validate for an existing email.
                else if (model.email == existingAccount.Email) 
                {
                    model.errMessage = "Email already exists. Please re-enter a new one.";
                }
                // Validate for an existing username.
                else if (model.username == existingAccount.Username) 
                {
                    model.errMessage = "Username already exists. Please re-enter a new one.";
                }
            }
            else
            {
                model.errMessage = "Please enter valid information.";
            }
            return View(model);
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
            Account account = Session["account"] as Account;
            UserAccountViewModel model = new UserAccountViewModel();
            model.acctUname = account.Username;
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
            Account account = Session["account"] as Account;
            Account updatedAccount = new Account
            {
                Username = model.acctUname,
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

        // Check for logged in account (account in session)
        private bool IsLoggedIn()
        {
            Account account = Session["account"] as Account;
            bool isLoggedIn = account != null;
            return isLoggedIn;
        }
    }
}
