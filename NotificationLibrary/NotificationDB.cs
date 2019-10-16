using System;
using System.Collections.Generic;

namespace NotificationLibrary
{
    public class NotificationDB
    {
        public static Boolean Load(ref List<Notification> notifications)
        {
            LoadFakeNotifications(ref notifications);
            return true;
        }
        private static void LoadFakeNotifications(ref List<Notification> notifications)
        {
            Notification note1 = new Notification();
            note1.Subject = "Food";
            note1.Message = "BLAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH" + Environment.NewLine + "BLAAAHHHHHHHHHHHHHHHHAAH";
            note1.SentDate = DateTime.Now;

            Notification note2 = new Notification();
            note2.Subject = "DOOF";
            note2.Message = "BLAHHHHHHHHHHHHHHHHsdfsdfsdfsfsdfHHHHHHHHHHHHHHHHHHHHHHH";
            note2.SentDate = DateTime.Now;

            Notification note3 = new Notification();
            note3.Subject = "DOOF";
            note3.Message = "BLAHHHHHHHHHHHHHHHHsdfsdfsdfsfsdfHHHHHHHHHHHHHHdgsbdgsfdhHHHHHHHHH";
            note3.SentDate = DateTime.Now;

            notifications.Add(note1);
            notifications.Add(note2);
            notifications.Add(note3);


        }
    }
}
