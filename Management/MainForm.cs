using AccountLibrary;
using Story2;
using Story3;
using Story4;
using System.Drawing;
using System.Windows.Forms;

namespace Management
{
    public partial class MainForm : Form
    {
        public Account account;
        public NotificationSender notificationSender;
        public TemplateCreator templateCreator;
        public LogViewer logViewer;

        public MainForm(Account account)
        {
            InitializeComponent();
            this.account = account;

            notificationSender = new NotificationSender();
            NotificationSender.LoginedEmployee = account;
            notificationSender.FormClosed += (sender, e) => Close();
            InitializeButtonList(notificationSender);

            templateCreator = new TemplateCreator();
            TemplateCreator.LoginedEmployee = account;
            templateCreator.FormClosed += (sender, e) => Close();
            InitializeButtonList(templateCreator);

            logViewer = new LogViewer();
            LogViewer.LoginedEmployee = account;
            logViewer.FormClosed += (sender, e) => Close();
            InitializeButtonList(logViewer);

            ShowInTaskbar = false;
        }

        public void InitializeButtonList(Form form)
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
                if (form is NotificationSender)
                {
                    return;
                }
                notificationSender.Show();
                notificationSender.Story2_Load(null, null);
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
                if (form is LogViewer)
                {
                    return;
                }
                logViewer.Show();
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
                    if (form is TemplateCreator)
                    {
                        return;
                    }
                    templateCreator.Show();
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

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            notificationSender.Show();
        }
    }
}
