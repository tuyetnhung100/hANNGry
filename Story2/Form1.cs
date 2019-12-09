/*
 * Programmer(s):      Gong-Hao
 * Date:               12/04/2019
 * What the code does: 1. Using a template to from a notification message
 *                     2. Sending the notification to subscribers
 */

using AccountLibrary;
using NotificationLibrary;
using Notifier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TagLibrary;
using TemplateLibrary;

namespace Story2
{
    public partial class NotificationSender : Form
    {
        public static Account LoginedEmployee;

        private const string NoneOption = "None";
        private const string DatabaseError = "Database Error";
        private const string SMTPError = "SMTP Error";
        private const string DataError = "Data Error";
        private const string StudentNameTag = "student name";
        private const string EmployeeNameTag = "employee name";

        private List<Tag> tags = new List<Tag>();
        private List<Template> templates = new List<Template>();
        private List<Account> subscribers = new List<Account>();
        private List<MessageBlock> messageBlocks = new List<MessageBlock>();
        private List<MessageBlock> tagBlocks = new List<MessageBlock>();

        private Notification notification = null;

        private int sendingNotificationCount = 0;
        private int succeededCount = 0;
        private int failedCount = 0;

        private int sendingAccountId = 0;
        private bool sendingSucceeded = false;
        private string sendingErrorMessage = null;

        public NotificationSender()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize controls and set default settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Story2_Load(object sender, EventArgs e)
        {
            InitializeTags();
            InitializeTemplateComboBox();
            InitializeSendingLabels();
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
        /// Remove texts of sending labels
        /// </summary>
        private void InitializeSendingLabels()
        {
            sendingEmailsLabel.Text = string.Empty;
            succeededNotificationsLabel.Text = string.Empty;
            failedNotificationsLabel.Text = string.Empty;
        }

        /// <summary>
        /// When drop down closed, applies selected template into messageRichTextBox.
        /// If selected "None", then clears text of messageRichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateComboBox_DropDownClosed(object sender, EventArgs e)
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
        private void TemplateComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (templateComboBox.Text == NoneOption)
            {
                templateComboBox.SelectedIndex = 0;
                TemplateComboBox_DropDownClosed(sender, e);
            }
            else
            {
                // skip the None option
                for (int i = 1; i < templateComboBox.Items.Count; i++)
                {
                    Template template = templateComboBox.Items[i] as Template;
                    if (template.Subject == templateComboBox.Text)
                    {
                        templateComboBox.SelectedIndex = i;
                        TemplateComboBox_DropDownClosed(sender, e);
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
            // get and validate Notification data from controls
            notification = GetNotification();
            if (!ValidateNotification(notification))
            {
                return;
            }

            // make a confirmation to prevent the accidentally button click
            DialogResult dialogResult = ShowConfirmMessageBox(
                "Send Notification Confirmation",
                "Do you want to send the notification?"
            );
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            // insert and get subscribers
            SendNotificationResult result = NotificationDB.SendNotification(notification, ref subscribers);
            switch (result)
            {
                case SendNotificationResult.NoSubscribers:
                    ShowInfoMessageBox("Sent Notification Terminated", "No any subscriber");
                    return;
                case SendNotificationResult.DatabaseError:
                    ShowErrorMessageBox(DatabaseError, "Insert notification failed!");
                    return;
            }

            // lock controls until finished
            SetControlsEnabledRecursion(this, false);

            sendingNotificationCount = 1;
            succeededNotificationsLabel.Text = "Succeeded: 0";
            failedNotificationsLabel.Text = "Failed: 0";
            sendingEmailsLabel.Text = "Sending notifications... ( 1 / " + subscribers.Count + " )";
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Send email.
        /// </summary>
        private void SendEmail(Account subscriber)
        {
            string email = subscriber.Email;
            string subject = notification.Subject;
            string body = ReplaceDatabaseField(subscriber, notification.Message, ref tags);
            EmailNotifier.SendEmail(email, subject, body);
        }

        /// <summary>
        /// Send SMS.
        /// </summary>
        private void SendSMS(Account subscriber)
        {
            string carrier = subscriber.Carrier;
            string phoneNumber = subscriber.PhoneNumber;
            string body = ReplaceDatabaseField(subscriber, notification.Message, ref tags);
            string subject = notification.Subject;
            EmailNotifier.SendSmsByEmail(carrier, phoneNumber, subject, body);
        }

        /// <summary>
        /// Validate Notification data with errorProvider.
        /// </summary>
        /// <param name="notification">The Notification object that will be validated</param>
        /// <returns>Whether the notification is valid</returns>
        private bool ValidateNotification(Notification notification)
        {
            // using bottom-up to validate
            bool isValid = true;
            errorProvider.Clear();

            // check Tags
            for (int i = tagBlocks.Count - 1; i >= 0; i--)
            {
                MessageBlock tagBlock = tagBlocks[i];
                if (tagBlock.Tag.Type == TagType.UserInput && string.IsNullOrWhiteSpace(tagBlock.Input.Text))
                {
                    errorProvider.SetError(tagBlock.Input, "Please enter Tag " + tagBlock.Tag.Name);
                    tagBlock.Input.Focus();
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

            // check Location
            if (
                !sylvaniaCheckBox.Checked &&
                !rockCreekCheckBox.Checked &&
                !southeastCheckBox.Checked &&
                !cascadeCheckBox.Checked
            )
            {
                errorProvider.SetError(cascadeCheckBox, "Please select at least one Location");
                sylvaniaCheckBox.Focus();
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

            if (sylvaniaCheckBox.Checked)
            {
                notification.Location |= AccountLibrary.Location.Sylvania;
            }
            if (rockCreekCheckBox.Checked)
            {
                notification.Location |= AccountLibrary.Location.RockCreek;
            }
            if (southeastCheckBox.Checked)
            {
                notification.Location |= AccountLibrary.Location.Southeast;
            }
            if (cascadeCheckBox.Checked)
            {
                notification.Location |= AccountLibrary.Location.Cascade;
            }

            bool isUsingTemplate = templateComboBox.SelectedIndex != 0;
            if (isUsingTemplate)
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
            bool isNone = templateComboBox.SelectedIndex == 0;
            if (isNone)
            {
                return messageRichTextBox.Text;
            }

            StringBuilder message = new StringBuilder();

            // concatenate messageBlocks to be filled message
            // only left DatabaseField as original tag format for later use
            foreach (MessageBlock messageBlock in messageBlocks)
            {
                if (!messageBlock.IsTag)
                {
                    message.Append(messageBlock.Message);
                    continue;
                }

                switch (messageBlock.Tag.Type)
                {
                    case TagType.DatabaseField:
                        message.Append(messageBlock.Tag.Syntax);
                        break;
                    case TagType.UserInput:
                        message.Append(messageBlock.Input.Text);
                        break;
                }
            }

            return message.ToString();
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
                if (tag.Type != TagType.DatabaseField)
                {
                    continue;
                }

                switch (tag.Name)
                {
                    case StudentNameTag:
                        message = message.Replace(tag.Syntax, subscriber.Name);
                        break;
                    case EmployeeNameTag:
                        message = message.Replace(tag.Syntax, LoginedEmployee.Name);
                        break;
                }
            }
            return message;
        }

        /// <summary>
        /// Clear and reset inputs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // make a confirmation to prevent the accidentally button click
            DialogResult dialogResult = ShowConfirmMessageBox(
                "Clear Inputs Confirmation",
                "Do you want to clear everything?"
            );
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
                        ShowErrorMessageBox(DataError, "Tag " + tagName + " is missing in DB");
                        messageBlocks.Clear();
                        tagBlocks.Clear();
                        return;
                    }
                    MessageBlock messageBlock = new MessageBlock
                    {
                        Tag = tag,
                        IsTag = true,
                        Message = null,
                        Input = null
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
            // use a dictionary to solve duplicated tags
            Dictionary<string, TextBox> textboxDictionary = new Dictionary<string, TextBox>();

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
                // skip the DatabaseField
                if (tagBlock.Tag.Type == TagType.DatabaseField)
                {
                    continue;
                }

                TextBox tagTextBox;
                if (textboxDictionary.ContainsKey(tagBlock.Tag.Name))
                {
                    tagTextBox = textboxDictionary[tagBlock.Tag.Name];
                }
                else
                {
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
                    tagTextBox = new TextBox
                    {
                        Font = font,
                        Location = new Point(startX + 220, startY - 4 + (index * space)),
                        Name = tagBlock.Tag.Name + "TextBox",
                        Size = new Size(360, 50),
                        TabIndex = tabIndex++
                    };
                    tagTextBox.TextChanged += (s, e) => ColorifyText();
                    tagsPanel.Controls.Add(tagTextBox);
                    textboxDictionary.Add(tagBlock.Tag.Name, tagTextBox);
                    if (index == 0)
                    {
                        tagTextBox.Focus();
                    }
                    index++;
                }
                tagBlock.Input = tagTextBox;
            }
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
                    continue;
                }

                switch (messageBlock.Tag.Type)
                {
                    case TagType.DatabaseField:
                        // if TagType is DatabaseField, set tag text to be blue
                        messageRichTextBox.SelectionColor = Color.Blue;
                        messageRichTextBox.AppendText(messageBlock.Tag.Name);
                        break;
                    case TagType.UserInput:
                        // if TagType is UserInput, set tag text to be red
                        // if there has user input, set tag text to be green
                        bool hasInput = !string.IsNullOrWhiteSpace(messageBlock.Input.Text);
                        messageRichTextBox.SelectionColor = hasInput ? Color.Green : Color.Red;
                        messageRichTextBox.AppendText(hasInput ? messageBlock.Input.Text : messageBlock.Tag.Name);
                        break;
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
        /// Show the info message.
        /// </summary>
        /// <param name="title">The title of the action</param>
        /// <param name="message">The message needs to show</param>
        private void ShowInfoMessageBox(string title, string message)
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

        /// <summary>
        /// Loop and send a notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < subscribers.Count; i++)
            {
                Account subscriber = subscribers[i];
                sendingAccountId = subscriber.AccountId;
                try
                {
                    if (subscriber.NotificationType.HasFlag(NotificationType.Email))
                    {
                        SendEmail(subscriber);
                    }
                    if (subscriber.NotificationType.HasFlag(NotificationType.SMS))
                    {
                        SendSMS(subscriber);
                    }
                    sendingSucceeded = true;
                }
                catch (Exception error)
                {
                    sendingSucceeded = false;
                    sendingErrorMessage = error.Message;
                }
                backgroundWorker.ReportProgress(i);
            }
        }

        /// <summary>
        /// When sending a notification finished, Update UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (sendingSucceeded)
            {
                NotificationDB.UpdateSubscriberNotification(
                    notification.NotificationId,
                    sendingAccountId,
                    succeeded: true,
                    cancelled: false,
                    errorMessage: null
                );
                succeededCount++;
            }
            else
            {
                NotificationDB.UpdateSubscriberNotification(
                    notification.NotificationId,
                    sendingAccountId,
                    succeeded: false,
                    cancelled: false,
                    errorMessage: sendingErrorMessage
                );
                failedCount++;
            }
            if (sendingNotificationCount < subscribers.Count)
            {
                sendingNotificationCount++;
            }
            succeededNotificationsLabel.Text = "Succeeded: " + succeededCount;
            failedNotificationsLabel.Text = "Failed: " + failedCount;
            sendingEmailsLabel.Text = "Sending notifications... ( " + sendingNotificationCount + " / " + subscribers.Count + " )";
        }

        /// <summary>
        /// When sending all notifications finished, Reset variables and UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowInfoMessageBox("Sent Notification Completed", "Notification sent successfully!");

            // unlock controls
            SetControlsEnabledRecursion(this, true);

            // clear labels
            InitializeSendingLabels();

            // reset variables
            subscribers.Clear();
            notification = null;
            sendingNotificationCount = 0;
            succeededCount = 0;
            failedCount = 0;

            // clear data to prevent submitting again
            ClearData();

            subjectTextBox.Focus();
        }
    }
}
