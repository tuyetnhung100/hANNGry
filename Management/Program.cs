using AccountLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Management
{
    static class Program
    {
        public static Account account;
        public static Form mainForm;
        public static Form story2;
        public static Form story3;
        public static Form story4;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Account fakeEmployee = new Account
            {
                AccountId = 1,
                Role = Role.Employee,
                Name = "Brad Pitt",
                Email = "emp@pcc.edu"
            };
            Account fakeManager = new Account
            {
                AccountId = 1,
                Role = Role.Manager,
                Name = "Tom Cruise",
                Email = "emp@pcc.edu"
            };

            // mainForm = new MainForm(fakeEmployee);
            mainForm = new MainForm(fakeManager);
            Application.Run(mainForm);
        }

        public static void InitializeButtonList(Form form)
        {
            Font font = new Font("Arial Narrow", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

            Button sendNotificationsButton = new Button
            {
                Font = font,
                Location = new Point(10, 12),
                Name = "sendNotificationsButton",
                Size = new Size(210, 42),
                Text = "Send &Notifications",
                UseVisualStyleBackColor = true
            };
            sendNotificationsButton.Click += (sender, e) =>
            {
                if (form is Story2.Story2)
                {
                    return;
                }
                story2.Show();
                form.Hide();
            };
            form.Controls.Add(sendNotificationsButton);

            Button viewLogsButton = new Button
            {
                Font = font,
                Location = new Point(235, 12),
                Name = "viewLogsButton",
                Size = new Size(210, 42),
                Text = "&View Logs",
                UseVisualStyleBackColor = true
            };
            viewLogsButton.Click += (sender, e) =>
            {
                if (form is Story4.Form1)
                {
                    return;
                }
                story4.Show();
                form.Hide();
            };
            form.Controls.Add(viewLogsButton);

            if (account.Role == Role.Manager)
            {
                Button createTemplateButton = new Button
                {
                    Font = font,
                    Location = new Point(460, 12),
                    Name = "generateTemplateButton",
                    Size = new Size(210, 42),
                    Text = "&Generate Template",
                    UseVisualStyleBackColor = true
                };
                createTemplateButton.Click += (sender, e) =>
                {
                    if (form is Story3.templateCreator)
                    {
                        return;
                    }
                    story3.Show();
                    form.Hide();
                };
                form.Controls.Add(createTemplateButton);
            }

            Label nameLabel = new Label
            {
                Font = font,
                Location = account.Role == Role.Manager ? new Point(685, 16) : new Point(460, 16),
                Name = "nameLabel",
                Size = new Size(400, 42),
                Text = account.Role.ToString() + ", " + account.Name
            };
            form.Controls.Add(nameLabel);
        }
    }
}
