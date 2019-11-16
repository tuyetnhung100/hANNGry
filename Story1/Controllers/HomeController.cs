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
        [HttpGet]
        public ActionResult Login()
        {
            if (IsLoggedIn())
            {
                return RedirectToAction("UserAccount");
            }
            ViewBag.Title = "Login";
            return View();
        }

        // Validate user's login (username and password)
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            model.message = "";
            model.errMessage = "";

            if (model.uname == null || model.psw == null) // If username and password are blank, display an error msg.
            {
                model.errMessage = "Please enter a valid username and password.";
                return View(model);
            }

            Account myAccount = AccountDB.FindAccount(model.uname);
            if (myAccount == null) // If account not found, displays an error msg.
            {
                model.errMessage = "Please enter a valid username and password.";
                return View(model);
            }
            else
            {
                model.name = myAccount.Name;
            }

            string userHash = AccountDB.CreateHash(model.psw, myAccount.PasswordSalt); // Create a string to store entered passwordHash from user.
            model.role = myAccount.Role;
            if (myAccount.Username == model.uname && userHash == myAccount.PasswordHash) // Compare entered on screen username + passsword with the ones stored in DB.
            {
                if (myAccount.Role == Role.Employee || myAccount.Role == Role.Manager) // Validate Roles, if staff then goes to Story2.
                {
                    model.message = "Hi staff!";
                    Thread thread = new Thread(() =>
                    {
                        Form mainForm = new MainForm(myAccount);
                        mainForm.ShowDialog();
                    });
                    thread.Start();
                }
                else if (myAccount.Role == Role.Subscriber) // Validate Roles, if subscriber then promts a success login message.
                {
                    Session["account"] = myAccount; // Stores login user
                    model.message = "Login successfully";
                }
            }
            else if (userHash != myAccount.PasswordHash) // Validate entered password in login.
            {
                model.errMessage = "Incorrect password. Please enter a valid username and password.";
            }
            else
            {
                model.errMessage = "Please enter a valid username and password."; // display an error message if username and password not found in DB.
            }
            return View(model);
        }

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

        // Validate inputs and create an account
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account existingAccount = AccountDB.FindAccount(model.username, model.email); // Validate whether account already exists.
                if (existingAccount == null) // If no match, then create a new account.
                {
                    Account myAccount = new Account();
                    myAccount.Username = model.username;
                    myAccount.Name = model.name;
                    myAccount.Email = model.email;
                    myAccount.PasswordHash = model.psw;

                    AccountDB.Add(myAccount);
                    model.message = "Register successfully";
                }
                else if (model.email == existingAccount.Email) // Validate for an existing email.
                {
                    model.errMessage = "Email already exists. Please re-enter a new one.";
                }
                else if (model.username == existingAccount.Username) // Validate for an existing username.
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

        [HttpPost]
        public ActionResult UserAccount(UserAccountViewModel model)
        {
            Account account = Session["account"] as Account;
            Account updatedAccount = new Account
            {
                Username = model.acctUname,
                Name = model.acctName,
                Email = model.acctEmail,
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
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["account"] = null;
            return View();
        }

        private bool IsLoggedIn()
        {
            Account account = Session["account"] as Account;
            bool isLoggedIn = account != null;
            return isLoggedIn;
        }
    }
}
