/*
 * Programmer(s):      Gong-Hao
 * Date:               11/14/2019
 * What the code does: Send notification via SMS
 */

using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Notifier
{
    public static class SMSNotifier
    {
        private const string senderPhoneNumber = "+12028662369";

        public static event NotifyCompletedEventHandler NotifyCompleted;

        private static bool isInit = false;

        /// <summary>
        /// Initialize Twilio
        /// </summary>
        private static void InitializeTwilio()
        {
            const string accountSid = "AC91fed9ae12a1edddc4c417e04557c2fe";
            const string authToken = "69ef3129ef9eb5aacb823742d5fa4b04";

            TwilioClient.Init(accountSid, authToken);
        }

        /// <summary>
        /// Send SMS.
        /// </summary>
        /// <param name="phoneNumber">The phone number</param>
        /// <param name="body">The text body</param>
        public static void SendSMS(string phoneNumber, string body)
        {
            if (!isInit)
            {
                InitializeTwilio();
                isInit = true;
            }
            string e164PhoneNumber = PhoneValidator.GetE164PhoneNumber(phoneNumber);
            MessageResource.Create(
                body: body,
                from: new PhoneNumber(senderPhoneNumber),
                to: new PhoneNumber(e164PhoneNumber)
            );
        }

        /// <summary>
        /// Send SMS asynchronously.
        /// </summary>
        /// <param name="phoneNumber">The phone number</param>
        /// <param name="body">The text body</param>
        /// <param name="userToken">The userToken</param>
        public static void SendSMSAsync(string phoneNumber, string body, object userToken)
        {
            if (!isInit)
            {
                InitializeTwilio();
                isInit = true;
            }
            Task.Run(async () =>
            {
                try
                {
                    string e164PhoneNumber = PhoneValidator.GetE164PhoneNumber(phoneNumber);
                    await MessageResource.CreateAsync(
                        body: body,
                        from: new PhoneNumber(senderPhoneNumber),
                        to: new PhoneNumber(phoneNumber)
                    );
                    if (NotifyCompleted != null)
                    {
                        NotifyCompletedEventArgs eventArgs = new NotifyCompletedEventArgs(userToken);
                        NotifyCompleted.Invoke(eventArgs);
                    }
                }
                catch (FormatException error)
                {
                    if (NotifyCompleted != null)
                    {
                        NotifyCompletedEventArgs eventArgs = new NotifyCompletedEventArgs(
                            cancelled: false,
                            error: error,
                            userState: userToken
                        );
                        NotifyCompleted.Invoke(eventArgs);
                    }
                }
            });
        }
    }
}
