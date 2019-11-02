/*
 * Programmer(s):      Gong-Hao
 * Date:               10/30/2019
 * What the code does: 1. Using a template to from a notification message
 *                     2. Sending the notification to subscribers
 */

using AccountLibrary;
using NotificationLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
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
        public static Account LoginedEmployee;

        private const string NoneOption = "None";
        private const string DatabaseError = "Database Error";
        private const string SMTPError = "SMTP Error";
        private const string DataError = "Data Error";

        private List<Tag> tags = new List<Tag>();
        private List<Template> templates = new List<Template>();
        private List<MessageBlock> messageBlocks = new List<MessageBlock>();
        private List<MessageBlock> tagBlocks = new List<MessageBlock>();

        private SmtpClient smtpClient = null;
        private List<Account> subscribers = new List<Account>();
        private Notification notification = null;
        private int sendingEmailCount = 0;
        private int succeededCount = 0;
        private int cancelledCount = 0;
        private int failedCount = 0;

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
            InitializeTags();
            InitializeTemplateComboBox();
            InitializeLabels();
            InitializeSmtpClient();
            subjectTextBox.Focus();
        }

        /// <summary>
        /// Load tags.
        /// </summary>
        private void InitializeTags()
        {
            if (!TagDB.Load(ref tags))
            {
                ShowErrorMessageBox(DatabaseError, "Loading tags failed!");
            }
        }

        /// <summary>
        /// Initialize and load items of templateComboBox.
        /// </summary>
        private void InitializeTemplateComboBox()
        {
            // set the default option to be "None"
            templateComboBox.Items.Clear();
            templateComboBox.Items.Add(NoneOption);
            templateComboBox.SelectedIndex = 0;

            // load templates
            if (TemplateDB.Load(ref templates))
            {
                templateComboBox.Items.AddRange(templates.ToArray());
            }
            else
            {
                ShowErrorMessageBox(DatabaseError, "Loading templates failed!");
            }
        }

        /// <summary>
        /// Remove texts of labels
        /// </summary>
        private void InitializeLabels()
        {
            sendingEmailsLabel.Text = string.Empty;
            succeededEmailsLabel.Text = string.Empty;
            failedEmailsLabel.Text = string.Empty;
        }

        /// <summary>
        /// Initialize SmtpClient
        /// </summary>
        private void InitializeSmtpClient()
        {
            NetworkCredential credentials = new NetworkCredential("hANNGry2019@gmail.com", "RUSerious?");
            smtpClient = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            smtpClient.SendCompleted += SmtpClient_SendCompleted;
        }

        /// <summary>
        /// The SendCompleted callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            int accountId = (int)e.UserState;
            if (e.Cancelled)
            {
                NotificationDB.UpdateSubscriberNotification(notification.NotificationId, accountId, false, true, null);
                cancelledCount++;
            }
            else if (e.Error != null)
            {
                NotificationDB.UpdateSubscriberNotification(notification.NotificationId, accountId, false, false, e.Error.ToString());
                failedCount++;
            }
            else
            {
                NotificationDB.UpdateSubscriberNotification(notification.NotificationId, accountId, true, false, null);
                succeededCount++;
            }
            SendNextEmail();
        }

        /// <summary>
        /// When drop down closed, applies selected template into messageRichTextBox.
        /// If selected "None", then clears text of messageRichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void templateComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (templateComboBox.SelectedIndex == -1)
            {
                return;
            }
            errorProvider.Clear();
            if (templateComboBox.SelectedIndex == 0)
            {
                // not using a templat - enable messageRichTextBox and rely on only text
                messageRichTextBox.ReadOnly = false;
                messageRichTextBox.Text = string.Empty;
                ReloadBlocks(string.Empty);
                ReloadTagInputs();
                subjectTextBox.Text = string.Empty;
                subjectTextBox.Focus();
            }
            else
            {
                // using a templat - disable messageRichTextBox and rely on tags
                Template template = templateComboBox.SelectedItem as Template;
                messageRichTextBox.ReadOnly = true;
                ReloadBlocks(template.Message);
                ReloadTagInputs();
                ColorifyText();
                subjectTextBox.Text = template.Subject;
            }
        }

        /// <summary>
        /// When press Enter key, checks selected value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void templateComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (templateComboBox.Text == NoneOption)
                {
                    templateComboBox.SelectedIndex = 0;
                    templateComboBox_DropDownClosed(sender, e);
                }
                else
                {
                    for (int i = 1; i < templateComboBox.Items.Count; i++)
                    {
                        Template template = templateComboBox.Items[i] as Template;
                        if (template.Subject == templateComboBox.Text)
                        {
                            templateComboBox.SelectedIndex = i;
                            templateComboBox_DropDownClosed(sender, e);
                        }
                    }
                }
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
            notification = GetNotification();
            if (!ValidateNotification(notification))
            {
                return;
            }

            // make a confirmation to prevent the accidentally button click
            DialogResult dialogResult = ShowConfirmMessageBox("Send Notification Confirmation", "Do you want to send the notification?");
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            // insert and get subscribers
            if (!NotificationDB.SendNotification(notification, ref subscribers))
            {
                ShowErrorMessageBox(DatabaseError, "Insert notification failed!");
                return;
            }

            // this is only for testing
            // UseFakeSubscribers(ref subscribers);

            // lock controls until finished
            SetControlsEnabled(false);

            SendNextEmail();
        }

        // send to myself
        // private void UseFakeSubscribers(ref List<Account> subscribers)
        // {
        //     subscribers.Clear();
        //     subscribers.Add(new Account
        //     {
        //         AccountId = 5,
        //         Email = "gonghao.wei@pcc.edu",
        //         Name = "Gong-Hao Wei"
        //     });
        //     subscribers.Add(new Account
        //     {
        //         AccountId = 1,
        //         Email = "weig@my.lanecc.edu",
        //         Name = "Gong-Hao"
        //     });
        // }

        /// <summary>
        /// Send email to the next subscriber.
        /// </summary>
        private void SendNextEmail()
        {
            sendingEmailCount++;
            succeededEmailsLabel.Text = "Succeeded: " + succeededCount;
            failedEmailsLabel.Text = "Failed: " + failedCount;

            if (sendingEmailCount <= subscribers.Count)
            {
                sendingEmailsLabel.Text = "Sending emails... ( " + sendingEmailCount + " / " + subscribers.Count + " )";
                Account subscriber = subscribers[sendingEmailCount - 1];
                SendEmail(subscriber);
            }
            else
            {
                FinishSendingEmails();
            }
        }

        /// <summary>
        /// Send email asynchronously.
        /// </summary>
        /// <param name="subscriber">The target subscriber</param>
        private void SendEmail(Account subscriber)
        {
            string email = subscriber.Email;
            string subject = notification.Subject;
            string body = ReplaceDatabaseField(subscriber, notification.Message, ref tags);
            MailMessage mailMessage = GetMailMessage(smtpClient, email, subject, body);
            smtpClient.SendAsync(mailMessage, subscriber.AccountId);
        }

        /// <summary>
        /// Set and return MailMessage object.
        /// </summary>
        /// <param name="client">The SmtpClient</param>
        /// <param name="email">The receiver's email address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        /// <returns>The MailMessage object</returns>
        private MailMessage GetMailMessage(SmtpClient client, string email, string subject, string body)
        {
            MailAddress from = new MailAddress("hANNGry2019@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            return message;
        }

        /// <summary>
        /// When sending emails finished, Reset variables and UI.
        /// </summary>
        private void FinishSendingEmails()
        {
            ShowSuccessMessageBox("Sent Notification Completed", "Notification sent successfully!");

            // unlock controls
            SetControlsEnabled(true);

            // clear labels
            InitializeLabels();

            // reset variables
            subscribers.Clear();
            notification = null;
            sendingEmailCount = 0;
            succeededCount = 0;
            cancelledCount = 0;
            failedCount = 0;

            // clear data to prevent submitting again
            ClearData();

            if (templateComboBox.SelectedIndex == 0)
            {
                messageRichTextBox.Focus();
            }
            else if (tagsPanel.Controls.Count > 0)
            {
                TextBox firstTagTextBox = tagsPanel.Controls[1] as TextBox;
                firstTagTextBox.Focus();
            }
        }

        /// <summary>
        /// Validate Notification data with errorProvider.
        /// </summary>
        /// <param name="notification">The Notification object that will be validated</param>
        /// <returns>Whether the notification is valid</returns>
        private bool ValidateNotification(Notification notification)
        {
            bool isValid = true;
            errorProvider.Clear();

            // check Tags bottom-up
            for (int i = tagBlocks.Count - 1; i >= 0; i--)
            {
                MessageBlock tagBlock = tagBlocks[i];
                if (tagBlock.Tag.Type == TagType.UserInput && string.IsNullOrWhiteSpace(tagBlock.Input))
                {
                    TextBox tagTextBox = tagsPanel.Controls.Find(tagBlock.Tag.Name + "TextBox", false)[0] as TextBox;
                    errorProvider.SetError(tagTextBox, "Please enter Tag " + tagBlock.Tag.Name);
                    tagTextBox.Focus();
                    isValid = false;
                }
            }

            // check Message
            if (string.IsNullOrWhiteSpace(messageRichTextBox.Text))
            {
                errorProvider.SetError(messageRichTextBox, "Please enter Message");
                messageRichTextBox.Focus();
                isValid = false;
            }

            // check Subject
            if (string.IsNullOrWhiteSpace(notification.Subject))
            {
                errorProvider.SetError(subjectTextBox, "Please enter Subject");
                subjectTextBox.Focus();
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Get Notification data from controls.
        /// </summary>
        /// <returns>The Notification object</returns>
        private Notification GetNotification()
        {
            string message = GetInputFilledMessage();

            Notification notification = new Notification
            {
                Subject = subjectTextBox.Text,
                Message = message,
                SentAccountId = LoginedEmployee.AccountId
            };

            if (templateComboBox.SelectedIndex != 0)
            {
                Template template = templateComboBox.SelectedItem as Template;
                notification.TemplateId = template.TemplateId;
            }

            return notification;
        }

        /// <summary>
        /// Get the message that combines with filled user inputs.
        /// </summary>
        /// <returns>The filled message</returns>
        private string GetInputFilledMessage()
        {
            // when the template is None, just return the text of the messageRichTextBox
            if (templateComboBox.SelectedIndex == 0)
            {
                return messageRichTextBox.Text;
            }

            string message = string.Empty;

            // concatenate messageBlocks to be filled message
            // only left DatabaseField as original tag format for later use
            foreach (MessageBlock messageBlock in messageBlocks)
            {
                if (messageBlock.IsTag)
                {
                    switch (messageBlock.Tag.Type)
                    {
                        case TagType.DatabaseField:
                            message += "{$" + messageBlock.Tag.Name + "}";
                            break;
                        case TagType.UserInput:
                            message += messageBlock.Input;
                            break;
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
        /// Replace DatabaseField of message.
        /// </summary>
        /// <param name="subscriber">The subscriber</param>
        /// <param name="message">The message from the template</param>
        /// <param name="tags">The tags of the template</param>
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
                        case "Employee Name":
                            message = message.Replace("{$Employee Name}", LoginedEmployee.Name);
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
            // make a confirmation to prevent the accidentally button click
            DialogResult dialogResult = ShowConfirmMessageBox("Clear Inputs Confirmation", "Do you want to clear everything?");
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            ClearData();
        }

        /// <summary>
        /// Clear Data
        /// </summary>
        private void ClearData()
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
                    // tag should never be null unless data in the DB is missing
                    if (tag == null)
                    {
                        ShowErrorMessageBox(DataError, "Tag " + tagName + " is missing from DB");
                        messageBlocks.Clear();
                        tagBlocks.Clear();
                        return;
                    }
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
        /// Reload tag inputs.
        /// </summary>
        private void ReloadTagInputs()
        {
            // reset tag inputs area
            tagsPanel.Controls.Clear();
            tagsPanel.Width = 800;

            int index = 0;
            int tabIndex = 0;
            int space = 50;
            int startX = 0;
            int startY = 5;
            Font font = new Font("Arial Narrow", 22.125F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // loop tagBlocks and add dynamic labels and textBoxs into tagsPanel
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
                    Location = new Point(startX, startY + (index * space)),
                    Name = tagBlock.Tag.Name + "Label",
                    Size = new Size(120, 43),
                    TabIndex = tabIndex++,
                    Text = "&" + (index + 1) + ". " + tagBlock.Tag.Name
                };
                tagsPanel.Controls.Add(tagLabel);
                TextBox tagTextBox = new TextBox
                {
                    Font = font,
                    Location = new Point(startX + 220, startY - 4 + (index * space)),
                    Name = tagBlock.Tag.Name + "TextBox",
                    Size = new Size(360, 50),
                    TabIndex = tabIndex++
                };
                tagTextBox.TextChanged += TagTextBox_TextChanged;
                tagsPanel.Controls.Add(tagTextBox);
                if (index == 0)
                {
                    tagTextBox.Focus();
                }
                index++;
            }
        }

        /// <summary>
        /// When tagInput's text changed, update messageRichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string tagName = textBox.Name.Replace("TextBox", string.Empty);
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

        /// <summary>
        /// Show the unexpected error, such as SQL exceptions.
        /// </summary>
        /// <param name="title">The title of the error</param>
        /// <param name="message">The message needs to show</param>
        private void ShowErrorMessageBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Show the success message.
        /// </summary>
        /// <param name="title">The title of the action</param>
        /// <param name="message">The message needs to show</param>
        private void ShowSuccessMessageBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Show the confirm box.
        /// </summary>
        /// <param name="title">The title of the action</param>
        /// <param name="message">The message needs to show</param>
        /// <returns>The DialogResult object</returns>
        private DialogResult ShowConfirmMessageBox(string title, string message)
        {
            DialogResult dialogResult = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );
            return dialogResult;
        }

        /// <summary>
        /// Enable or disable all controls
        /// </summary>
        /// <param name="enabled">Whether controls are enabled</param>
        private void SetControlsEnabled(bool enabled)
        {
            SetControlsEnabledRecursion(this, enabled);
        }

        /// <summary>
        /// Use recursion to enable or disable all controls
        /// </summary>
        /// <param name="target">The target control</param>
        /// <param name="enabled">Whether controls are enabled</param>
        private void SetControlsEnabledRecursion(Control target, bool enabled)
        {
            foreach (Control control in target.Controls)
            {
                if (!(control is Label))
                {
                    control.Enabled = enabled;
                }
                if (control.Controls.Count > 0)
                {
                    foreach (Control child in control.Controls)
                    {
                        SetControlsEnabledRecursion(child, enabled);
                    }
                }
            }
        }
    }
}
