/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 10/20/2019
    Purpose: To maintain the data of the Login webpage.
*/

using AccountLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Story1.Models.ViewModels
{
    // Return value and assign a new value for properties
    // Retrieve and store model state in the DB, set validations for user input
    public class LoginViewModel
    {
        [Required]     
        [Display(Name = "Username")]
        public string uname { get; set; }

        [Required]      
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string psw { get; set; }

        public string name { get; set; }
        public Role role { get; set; }
        public string message { get; set; }
        public string errMessage { get; set; }
    }
}