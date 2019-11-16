/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 11/15/2019
    Purpose: To maintain the data of the UserAccount form.
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
    public class UserAccountViewModel
    {
        [Display(Name = "Name")]
        public string acctName { get; set; }

        [Display(Name = "Username")]
        public string acctUname { get; set; }

        [Display(Name = "Email")]
        public string acctEmail { get; set; }

        public bool isEmailNotiType { get; set; }
        public bool isTextNotiType { get; set; }
        public bool isBothNotiType { get; set; }

        public bool isSYLocation { get; set; }
        public bool isRCLocation { get; set; }
        public bool isCASLocation { get; set; }
        public bool isSELocation { get; set; }
    }
}