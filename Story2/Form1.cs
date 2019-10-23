/*
 * Programmer(s):      Gong-Hao
 * Date:               10/21/2019
 * What the code does: 1. Using a template to from a notification message
 *                     2. Sending the notification to subscribers
 */

using AccountLibrary;
using NotificationLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TagLibrary;
using TemplateLibrary;

namespace Story2
{
    public partial class Story2 : Form
    {
        private Account staff = new Account();
        private List<Tag> tags = new List<Tag>();
        private List<Template> templates = new List<Template>();
        private List<MessageBlock> messageBlocks = new List<MessageBlock>();
        private List<MessageBlock> tagBlocks = new List<MessageBlock>();

        public Story2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize controls and set default settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Story2_Load(object sender, EventArgs e)
        {
            LoadLoginedStaff();
            InitializeTemplateComboBox();
        }

        /// <summary>
        /// Load Logined Staff
        /// </summary>
        private void LoadLoginedStaff()
        {
            if (!AccountDB.GetLoginedStaff(ref staff))
            {
                MessageBox.Show("Loading Logined Staff failed!");
            }
        }

        /// <summary>
        /// Initialize and load items of templateComboBox.
        /// </summary>
        private void InitializeTemplateComboBox()
        {
            // set the default option to be "None"
            templateComboBox.Items.Clear();
            templateComboBox.Items.Add("None");
            templateComboBox.SelectedIndex = 0;

            // load templates
            if (TemplateDB.Load(ref templates))
            {
                templateComboBox.Items.AddRange(templates.ToArray());
            }
            else
            {
                MessageBox.Show("Loading templates failed!");
            }
        }

        /// <summary>
        /// When changed, applies selected template into messageRichTextBox.
        /// If selected "None", then clears text of messageRichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateComboBox.SelectedIndex == 0)
            {
                // not using a templat - enable messageRichTextBox and rely on only text
                messageRichTextBox.Enabled = true;
                ReloadTags(null);
                ReloadBlocks(string.Empty);
                ReloadTagInputs();
                messageRichTextBox.Text = string.Empty;
            }
            else
            {
                // using a templat - disable messageRichTextBox and rely on tags
                Template template = templates[templateComboBox.SelectedIndex - 1];
                messageRichTextBox.Enabled = false;
                ReloadTags(template);
                ReloadBlocks(template.Message);
                ReloadTagInputs();
                ColorifyText();
            }
        }

        /// <summary>
        /// When clicked, sends the notification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, EventArgs e)
        {
            // get Notification data from controls
            Notification notification = GetNotification();
            if (!ValidateNotification(notification))
            {
                return;
            }

            // insert and get subscriberIds
            List<Account> subscribers = new List<Account>();
            if (!NotificationDB.SendNotification(notification, ref subscribers))
            {
                MessageBox.Show("Insert notification failed!");
                return;
            }
            try
            {
                SmtpClient smtpClient = GetSmtpClient();

                // loop subscriberIds and send emails
                foreach (Account subscriber in subscribers)
                {
                    string email = subscriber.Email;
                    string subject = notification.Subject;
                    string body = ReplaceDatabaseField(subscriber, notification.Message, ref tags);
                    SendEmail(smtpClient, email, subject, body);
                    break;
                }

                smtpClient.Dispose();

                MessageBox.Show("Notification sent successfully!");

                // clear data to prevent submitting again
                clearButton.PerformClick();
            }
            catch (Exception)
            {
                MessageBox.Show("Send email failed!");
            }
        }

        /// <summary>
        /// Validate Notification data
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        private bool ValidateNotification(Notification notification)
        {
            string errorMessage = string.Empty;

            // check Subject
            if (string.IsNullOrWhiteSpace(notification.Subject))
            {
                errorMessage += "Subject is required!" + Environment.NewLine;
            }

            // check Message
            if (string.IsNullOrWhiteSpace(notification.Subject))
            {
                errorMessage += "Message is required!" + Environment.NewLine;
            }

            // check tags
            foreach (MessageBlock tagBlock in tagBlocks)
            {
                if (tagBlock.Tag.Type == TagType.UserInput && string.IsNullOrWhiteSpace(tagBlock.Input))
                {
                    errorMessage += "Tag \"" + tagBlock.Tag.Name + "\" is required!" + Environment.NewLine;
                }
            }

            // show errorMessage if any
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get Notification data from controls
        /// </summary>
        /// <returns></returns>
        private Notification GetNotification()
        {
            string message = GetInputFilledMessage();

            Notification notification = new Notification
            {
                Subject = subjectTextBox.Text,
                Message = message,
                SentAccountId = staff.AccountId
            };

            if (templateComboBox.SelectedIndex != 0)
            {
                Template template = templates[templateComboBox.SelectedIndex - 1];
                notification.TemplateId = template.TemplateId;
            }

            return notification;
        }

        /// <summary>
        /// Get message that user inputs has been filled
        /// </summary>
        /// <returns></returns>
        private string GetInputFilledMessage()
        {
            string message = string.Empty;

            foreach (MessageBlock messageBlock in messageBlocks)
            {
                if (messageBlock.IsTag)
                {
                    if (messageBlock.Tag.Type == TagType.UserInput)
                    {
                        message += messageBlock.Input;
                    }
                    else
                    {
                        message += "{$" + messageBlock.Tag.Name + "}";
                    }
                }
                else
                {
                    message += messageBlock.Message;
                }
            }

            return message;
        }

        /// <summary>
        /// Replace DatabaseField of message
        /// </summary>
        /// <param name="subscriber">The subscriber</param>
        /// <param name="message">Message from the template</param>
        /// <param name="tags">tags of the template</param>
        /// <returns></returns>
        private string ReplaceDatabaseField(Account subscriber, string message, ref List<Tag> tags)
        {
            foreach (Tag tag in tags)
            {
                if (tag.Type == TagType.DatabaseField)
                {
                    switch (tag.Name)
                    {
                        case "Student Name":
                            message = message.Replace("{$Student Name}", subscriber.Name);
                            break;
                        case "Staff Name":
                            message = message.Replace("{$Staff Name}", staff.Name);
                            break;
                    }
                }
            }
            return message;
        }

        /// <summary>
        /// Clear and reset inputs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            // reset subject
            subjectTextBox.Text = string.Empty;

            // reset tag inputs
            foreach (Control control in tagsPanel.Controls)
            {
                if (control is TextBox)
                {
                    (control as TextBox).Text = string.Empty;
                }
            }

            // reset messageRichTextBox
            ColorifyText();
        }

        /// <summary>
        /// Load tags by TemplateId
        /// </summary>
        /// <param name="template"></param>
        private void ReloadTags(Template template)
        {
            if (template != null)
            {
                // load tags
                if (!TagDB.LoadByTemplateId(ref tags, template.TemplateId))
                {
                    MessageBox.Show("Loading tags failed!");
                }
            }
            else
            {
                tags.Clear();
            }
        }

        /// <summary>
        /// Analyze and set messageBlocks that represent blocks of template content.
        /// </summary>
        /// <param name="message">message of template</param>
        private void ReloadBlocks(string message)
        {
            messageBlocks.Clear();
            tagBlocks.Clear();

            // Regex tested by https://regexr.com/4mokk
            // it splits {$foo} into blocks
            string[] blocks = Regex.Split(message, @"(\{\$.*?\})");
            foreach (string block in blocks)
            {
                // Regex tested by https://regexr.com/4mokh
                // it matchs {$foo}
                bool isTag = Regex.IsMatch(block, @"^\{\$.*?\}$");
                if (isTag)
                {
                    string tagName = block.Substring(2, block.Length - 3);
                    Tag tag = tags.Find(x => x.Name == tagName);
                    MessageBlock messageBlock = new MessageBlock
                    {
                        Tag = tag,
                        IsTag = true,
                        Message = tagName,
                        Input = string.Empty
                    };
                    messageBlocks.Add(messageBlock);
                    tagBlocks.Add(messageBlock);
                }
                else
                {
                    MessageBlock messageBlock = new MessageBlock
                    {
                        Tag = null,
                        IsTag = false,
                        Message = block,
                        Input = null
                    };
                    messageBlocks.Add(messageBlock);
                }
            }
        }

        /// <summary>
        /// Load tagBlocks into tagsDataGridView.
        /// </summary>
        private void ReloadTagInputs()
        {
            // reset tag inputs
            tagsPanel.Controls.Clear();
            tagsPanel.Width = 800;
            int index = 0;
            int tabIndex = 0;
            Font font = new Font("Arial Narrow", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // add tagBlock into tag inputs
            foreach (MessageBlock tagBlock in tagBlocks)
            {
                if (tagBlock.Tag.Type == TagType.DatabaseField)
                {
                    continue;
                }
                Label tagLabel = new Label
                {
                    AutoSize = true,
                    Font = font,
                    Location = new Point(20, 84 + (index * 56)),
                    Name = tagBlock.Tag.Name + "Label",
                    Size = new Size(120, 43),
                    TabIndex = tabIndex++,
                    Text = "&" + (index + 1) + ". " + tagBlock.Tag.Name
                };
                tagsPanel.Controls.Add(tagLabel);
                TextBox tagTextBox = new TextBox
                {
                    Font = font,
                    Location = new Point(230, 80 + (index * 56)),
                    Name = tagBlock.Tag.Name + "TextBox",
                    Size = new Size(240, 50),
                    TabIndex = tabIndex++
                };
                tagTextBox.TextChanged += TagTextBox_TextChanged;
                tagsPanel.Controls.Add(tagTextBox);
                index++;
            }
        }

        /// <summary>
        /// Update messageRichTextBox when tagInput's text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string tagName = textBox.Name.Replace("TextBox", "");
            MessageBlock tagBlock = tagBlocks.Find(x => x.Tag.Name == tagName);
            tagBlock.Input = textBox.Text;
            ColorifyText();
        }

        /// <summary>
        /// Colorify messageRichTextBox's text.
        /// </summary>
        private void ColorifyText()
        {
            // reset text value
            messageRichTextBox.Clear();

            // append all blocks to be text value of messageRichTextBox
            foreach (MessageBlock messageBlock in messageBlocks)
            {
                if (!messageBlock.IsTag)
                {
                    // set normal text color to be black
                    messageRichTextBox.SelectionColor = Color.Black;
                    messageRichTextBox.AppendText(messageBlock.Message);
                }
                else
                {
                    switch (messageBlock.Tag.Type)
                    {
                        case TagType.DatabaseField:
                            // if TagType is DatabaseField, set tag text to be blue
                            messageRichTextBox.SelectionColor = Color.Blue;
                            messageRichTextBox.AppendText(messageBlock.Message);
                            break;
                        case TagType.UserInput:
                            // if TagType is UserInput, set tag text to be red
                            // if there has user input, set tag text to be green
                            bool hasInput = !string.IsNullOrWhiteSpace(messageBlock.Input);
                            messageRichTextBox.SelectionColor = hasInput ? Color.Green : Color.Red;
                            messageRichTextBox.AppendText(hasInput ? messageBlock.Input : messageBlock.Message);
                            break;
                    }
                }
            }
        }

        private SmtpClient GetSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient();
            return smtpClient;
        }

        private void SendEmail(SmtpClient client, string email, string subject, string body)
        {
            MailAddress from = new MailAddress(
                "gonghao.wei@pcc.edu",
                "Gong-Hao " + (char)0xD8 + " Wei",
                Encoding.UTF8
            );
            MailAddress to = new MailAddress("gonghao.wei@pcc.edu");
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            // client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            client.SendAsync(message, email);
            message.Dispose();
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            string email = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", email);
            }
            else if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", email, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
    }
}
