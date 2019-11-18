﻿/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 10/20/2019
    Purpose: To maintain the data of the Register webpage.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Story1.Models.ViewModels
{
    // Return value and assign a new value for properties
    // Retrieve and store model state in the DB, set validations for user input
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Display(Name = "Password")]
        public string psw { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("psw", ErrorMessage = "The password and repeat password do not match.")]
        [Display(Name = "Repeat Password")]
        public string pswRepeat { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string name { get; set; }

        public string errMessage { get; set; }
        public string message { get; set; }
    }
}