/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data model of Account.
 */

using System;

namespace AccountLibrary
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Carrier { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Activated { get; set; }
        public Location Location { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Code { get; set; }
    }
}
