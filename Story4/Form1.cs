/*
 * Programmer(s) Alex Matthias
 * Date: 10/22/2019
 * What the code does: Retrieve notifications sent
 */

using AccountLibrary;
using NotificationLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Story4
{
    public partial class Form1 : Form
    {
        public static Account LoginedEmployee;

        public Form1()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clears data grid view
            notificationDataGridView.DataSource = null;
            notificationDataGridView.Rows.Clear();
            MessageBox.Show("Cleared Successfully");
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            DateTime startTime = firstDateTimePicker.Value;
            DateTime endTime = secondDateTimePicker.Value;

            List<Notification> notifications = new List<Notification>();
            NotificationDB.Load(startTime, endTime, ref notifications);
            DataTable datatable = new DataTable();
            datatable.Columns.Add("Sender");
            datatable.Columns.Add("Subject");
            datatable.Columns.Add("Message");
            datatable.Columns.Add("Date Sent");

            foreach (Notification notification in notifications)
            {
                datatable.Rows.Add(notification.SentAccountId, notification.Subject, notification.Message, notification.SentDate);
            }

            notificationDataGridView.DataSource = datatable;

            notificationDataGridView.Columns[0].Width = 100;
            notificationDataGridView.Columns[1].Width = 100;
            notificationDataGridView.Columns[2].Width = 510;
            notificationDataGridView.Columns[3].Width = 125;
        }
    }
}
