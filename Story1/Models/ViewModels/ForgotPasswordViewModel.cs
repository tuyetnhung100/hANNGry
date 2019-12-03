/*
    Programmer: Nina Hoang
    Class: CIS234A
    Date: 11/26/2019
    Purpose: To maintain the data of the ChangePasswordByEmail webpage.
*/

using System.ComponentModel.DataAnnotations;

namespace Story1.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        public string errMessage { get; set; }
        public string message { get; set; }
    }
}