/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: Data model of Notification.
 */

using System;

namespace NotificationLibrary
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int? TemplateId { get; set; }
        public int SentAccountId { get; set; }
        public DateTime SentDate { get; set; }
        public string SenderName { get; set; }
        public int NumberSent { get; set; }
    }
}
