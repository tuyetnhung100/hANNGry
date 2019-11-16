using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Story1.Models.ViewModels
{
    public class MyViewModel
    {
        public Story1.Models.ViewModels.UserAccountViewModel U { get; set; }
        public Story1.Models.ViewModels.LoginViewModel L { get; set; }
    }
}