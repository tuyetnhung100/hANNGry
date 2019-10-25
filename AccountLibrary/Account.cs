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
        String _email;
        String _name;
        String _passwordHash;

        public int AccountId { get; set; }
        public Role Role { get; set; }
        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }
        public string PasswordSalt { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public DateTime CreatedDate { get; set; }
    }
}
