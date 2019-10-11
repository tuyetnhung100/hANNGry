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
    }
}
