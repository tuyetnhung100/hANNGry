/*
 * Programmer(s):      Gong-Hao
 * Date:               11/19/2019
 * What the code does: Handel phone format
 */

using System;
using System.Text.RegularExpressions;

namespace Notifier
{
    public static class PhoneValidator
    {
        /// <summary>
        /// Convert formatted phone number to E164 format
        /// </summary>
        /// <param name="phoneNumber">The original, formatted phone number</param>
        /// <param name="countryCode">The country code of the phone number</param>
        /// <param name="length">The length of the phone number</param>
        /// <returns></returns>
        public static string GetE164PhoneNumber(string phoneNumber, string countryCode = "1", int length = 10)
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
            string rawPhoneNumber = digitsOnly.Replace(phoneNumber, string.Empty);
            if (rawPhoneNumber.Length == length)
            {
                string e164PhoneNumber = "+" + countryCode + rawPhoneNumber;
                return e164PhoneNumber;
            }
            else if (
                rawPhoneNumber.Length == countryCode.Length + length &&
                rawPhoneNumber.IndexOf(countryCode) == 0
            )
            {
                string e164PhoneNumber = "+" + rawPhoneNumber;
                return e164PhoneNumber;
            }
            else
            {
                throw new FormatException("incorrect phone number format " + phoneNumber);
            }
        }
    }
}
