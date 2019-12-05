/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 11/15/2019
    Purpose: To maintain the data of the UserAccount (Account Settings) webpage.
*/

using AccountLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Story1.Models.ViewModels
{
    // Return value and assign a new value for properties
    // Retrieve and store model state in the DB, set validations for user input
    public class UserAccountViewModel
    {
        public UserAccountViewModel()
        {
            list = new SelectList(new string[] {
                "AT&T",
                "T-Mobile",
                "Verizon"
            });
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string acctName { get; set; }

        //[Display(Name = "Username")]
        //public string acctUname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        [Display(Name = "Email")]
        public string acctEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Phone Number")]
        public string acctPhoneNumber { get; set; }

        public string carrier { get; set; }
        public SelectList list { get; set; }

        public bool isEmailNotiType { get; set; }
        public bool isTextNotiType { get; set; }

        public bool isSYLocation { get; set; }
        public bool isRCLocation { get; set; }
        public bool isCASLocation { get; set; }
        public bool isSELocation { get; set; }

        public string message { get; set; }
        public string errMessage { get; set; }
    }
}