/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data model of Account.
 */

using System;
using System.Text.RegularExpressions;

namespace AccountLibrary
{
    public class Account
    {
        string phoneNumber;
        public int AccountId { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = GetCleanPhoneNumber(value);
            }
        }
        public string Carrier { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Activated { get; set; }
        public Location Location { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Get clean phone number without format
        /// </summary>
        /// <param name="phoneNumber">The original, formatted phone number</param>
        /// <returns></returns>
        private static string GetCleanPhoneNumber(string phoneNumber)
        {
            // Replace any format
            //   (503) 123 - 4567
            //   503 - 123 - 4567
            //   503 123 4567
            //   503 1234567
            //   503123 4567
            //   5 0 3 1 2 3 4 5 6 7
            // to digits only string
            //   5031234567
            Regex digitsOnly = new Regex(@"[^\d]");
            string cleanPhoneNumber = digitsOnly.Replace(phoneNumber, string.Empty);
            if (cleanPhoneNumber.Length == 10)
            {
                return cleanPhoneNumber;
            }
            else
            {
                return null;
            }
        }
    }
}
