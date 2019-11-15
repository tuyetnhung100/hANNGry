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
            ViewBag.Title = "UserAccount";
            return View();
        }

    }
}
