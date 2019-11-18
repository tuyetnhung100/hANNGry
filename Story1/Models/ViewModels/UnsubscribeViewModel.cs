/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 11/17/2019
    Purpose: To maintain the data of the Unsubscribe webpage.
*/

using AccountLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Story1.Models.ViewModels
{
    public class UnsubscribeViewModel
    {
        [Display(Name = "Username")]
        public string uname { get; set; }

        public string message { get; set; }
    }
}