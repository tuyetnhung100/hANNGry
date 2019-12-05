/*
 * Programmer(s):      Gong-Hao
 * Date:               11/14/2019
 * What the code does: Send notification via email
 */

using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Notifier
{
    public static class EmailNotifier
    {
        private static SmtpClient SmtpClient = InitializeSmtpClient();

        /// <summary>
        /// Initialize SmtpClient
        /// </summary>
        /// <returns></returns>
        private static SmtpClient InitializeSmtpClient()
        {
            NetworkCredential credentials = new NetworkCredential(
                "hANNGry2019@gmail.com",
                "RUSerious?"
            );
            SmtpClient smtpClient = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            return smtpClient;
        }

        /// <summary>
        /// Set and return MailMessage object.
        /// </summary>
        /// <param name="email">The email address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        /// <returns>The MailMessage object</returns>
        private static MailMessage GetMailMessage(string email, string subject, string body)
        {
            MailAddress from = new MailAddress("hANNGry2019@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            return message;
        }

        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="email">The email address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        public static void SendEmail(string email, string subject, string body)
        {
            MailMessage mailMessage = GetMailMessage(email, subject, body);
            SmtpClient.Send(mailMessage);
        }

        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="email">The email address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        public static void SendHtmlEmail(string email, string subject, string body)
        {
            MailMessage mailMessage = GetMailMessage(email, subject, body);
            mailMessage.IsBodyHtml = true;
            SmtpClient.Send(mailMessage);
        }

        /// <summary>      
        /// Send SMS by email.
        /// </summary>
        /// <param name="carrier">The carrier of the phone number</param>
        /// <param name="phoneNumber">The phone number</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        public static void SendSmsByEmail(string carrier, string phoneNumber, string subject, string body)
        {
            string email = GetEmailByCarrier(carrier, phoneNumber);
            SendEmail(email, subject, body);
        }

        /// <summary>
        /// Get email address of email to text service
        /// </summary>
        /// <param name="carrier">The carrier of the phone number</param>
        /// <param name="phoneNumber">The phone number</param>
        /// <returns></returns>
        private static string GetEmailByCarrier(string carrier, string phoneNumber)
        {
            switch (carrier)
            {
                case "AT&T":
                    return phoneNumber + "@txt.att.net";
                case "T-Mobile":
                    return phoneNumber + "@tmomail.net";
                case "Verizon":
                    return phoneNumber + "@vtext.com";
                default:
                    throw new Exception("Carrier not defined");
            }
        }
    }
}
