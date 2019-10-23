using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Story1.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string username { get; set; }
        public string email { get; set; }
        public string psw { get; set; }
        public string pswRepeat { get; set; }
        public string name { get; set; }
    }
}