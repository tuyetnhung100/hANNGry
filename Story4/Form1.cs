/*
 * Programmer(s) Alex Matthias
 * Date: 10/22/2019
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


            
            
        }
        // Creates the data table add that data to datagridview
        private void findButton_Click(object sender, EventArgs e)
        {

            DateTime startTime = TimeReset.ResetTimeToStartOfDay(firstDateTimePicker.Value);
            
            DateTime endTime = TimeReset.ResetTimeToEndOfDay(secondDateTimePicker.Value);

            List<Notification> notifications = new List<Notification>();
            NotificationDB.Load(startTime, endTime, ref notifications);
            DataTable datatable = new DataTable();
            datatable.Columns.Add("Sender");
            datatable.Columns.Add("Subject");
            datatable.Columns.Add("Message");
            datatable.Columns.Add("Date Sent");
            datatable.Columns.Add("Number Sent");

            foreach (Notification notification in notifications)
            {
                datatable.Rows.Add(
                    notification.SenderName,
                    notification.Subject,
                    notification.Message,
                    notification.SentDate.ToString("MM/dd/yyyy HH:mm"),
                    notification.NumberSent
                );
            }

            notificationDataGridView.DataSource = datatable;

            notificationDataGridView.Columns[0].Width = 75;
            notificationDataGridView.Columns[1].Width = 85;
            notificationDataGridView.Columns[2].Width = 585;
            notificationDataGridView.Columns[3].Width = 100;
            notificationDataGridView.Columns[4].Width = 50;





            //foreach (DataGridViewRow r in notificationDataGridView.Rows)
            //{
            //    r.Height = 100;
            //}
            // notificationDataGridView.Rows[0].Height = 50;



        }


    }


    
}
