/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 10/20/2019
    Purpose: To maintain the data of the Login form.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Story1.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "*Username is required.")]
        public string uname { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*Password is required.")]
        public string psw { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public string errMessage { get; set; }
    }
}