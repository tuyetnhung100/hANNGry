/*
 * Programmer(s) Alex Matthias
 * Date: 10/15/2019
 * What the code does: Retrieve notifications sent
 */

using NotificationLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Story4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            // This is supply fake data for the DataGridView for testing

            List<Notification> notifications = new List<Notification>();
            NotificationDB.Load(ref notifications);
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

            
        }
    }
}
