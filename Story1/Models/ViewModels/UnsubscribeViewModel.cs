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