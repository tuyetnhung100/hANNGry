namespace Story2
{
    partial class NotificationSender
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationSender));
            this.sendButton = new System.Windows.Forms.Button();
            this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.templateLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.tagsPanel = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.locationsLabel = new System.Windows.Forms.Label();
            this.cascadeCheckBox = new System.Windows.Forms.CheckBox();
            this.southeastCheckBox = new System.Windows.Forms.CheckBox();
            this.rockCreekCheckBox = new System.Windows.Forms.CheckBox();
            this.sylvaniaCheckBox = new System.Windows.Forms.CheckBox();
            this.sendingEmailsLabel = new System.Windows.Forms.Label();
            this.succeededNotificationsLabel = new System.Windows.Forms.Label();
            this.failedNotificationsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendButton.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendButton.Location = new System.Drawing.Point(136, 568);
            this.sendButton.Margin = new System.Windows.Forms.Padding(1);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(180, 53);
            this.sendButton.TabIndex = 9999;
            this.sendButton.Text = "&Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.messageRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.messageRichTextBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F);
            this.messageRichTextBox.Location = new System.Drawing.Point(136, 212);
            this.messageRichTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.Size = new System.Drawing.Size(370, 349);
            this.messageRichTextBox.TabIndex = 10;
            this.messageRichTextBox.Text = "";
            // 
            // templateComboBox
            // 
            this.templateComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.templateComboBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(136, 110);
            this.templateComboBox.Margin = new System.Windows.Forms.Padding(1);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(370, 41);
            this.templateComboBox.TabIndex = 6;
            this.templateComboBox.DropDownClosed += new System.EventHandler(this.templateComboBox_DropDownClosed);
            this.templateComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.templateComboBox_KeyUp);
            // 
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateLabel.Location = new System.Drawing.Point(10, 113);
            this.templateLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(118, 35);
            this.templateLabel.TabIndex = 5;
            this.templateLabel.Text = "&Template";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(10, 215);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(115, 35);
            this.messageLabel.TabIndex = 9;
            this.messageLabel.Text = "&Message";
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectLabel.Location = new System.Drawing.Point(10, 164);
            this.subjectLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(97, 35);
            this.subjectLabel.TabIndex = 7;
            this.subjectLabel.Text = "S&ubject";
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectTextBox.Location = new System.Drawing.Point(136, 161);
            this.subjectTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(370, 41);
            this.subjectTextBox.TabIndex = 8;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(326, 568);
            this.clearButton.Margin = new System.Windows.Forms.Padding(1);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(180, 53);
            this.clearButton.TabIndex = 10000;
            this.clearButton.Text = "&Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // tagsPanel
            // 
            this.tagsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tagsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tagsPanel.Location = new System.Drawing.Point(534, 212);
            this.tagsPanel.Margin = new System.Windows.Forms.Padding(1);
            this.tagsPanel.Name = "tagsPanel";
            this.tagsPanel.Size = new System.Drawing.Size(370, 349);
            this.tagsPanel.TabIndex = 11;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // locationsLabel
            // 
            this.locationsLabel.AutoSize = true;
            this.locationsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationsLabel.Location = new System.Drawing.Point(10, 62);
            this.locationsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.locationsLabel.Name = "locationsLabel";
            this.locationsLabel.Size = new System.Drawing.Size(121, 35);
            this.locationsLabel.TabIndex = 0;
            this.locationsLabel.Text = "Locations";
            // 
            // cascadeCheckBox
            // 
            this.cascadeCheckBox.AutoSize = true;
            this.cascadeCheckBox.Checked = true;
            this.cascadeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cascadeCheckBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F);
            this.cascadeCheckBox.Location = new System.Drawing.Point(613, 60);
            this.cascadeCheckBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cascadeCheckBox.Name = "cascadeCheckBox";
            this.cascadeCheckBox.Size = new System.Drawing.Size(132, 39);
            this.cascadeCheckBox.TabIndex = 4;
            this.cascadeCheckBox.Text = "C&ascade";
            this.cascadeCheckBox.UseVisualStyleBackColor = true;
            // 
            // southeastCheckBox
            // 
            this.southeastCheckBox.AutoSize = true;
            this.southeastCheckBox.Checked = true;
            this.southeastCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.southeastCheckBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F);
            this.southeastCheckBox.Location = new System.Drawing.Point(453, 60);
            this.southeastCheckBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.southeastCheckBox.Name = "southeastCheckBox";
            this.southeastCheckBox.Size = new System.Drawing.Size(146, 39);
            this.southeastCheckBox.TabIndex = 3;
            this.southeastCheckBox.Text = "S&outheast";
            this.southeastCheckBox.UseVisualStyleBackColor = true;
            // 
            // rockCreekCheckBox
            // 
            this.rockCreekCheckBox.AutoSize = true;
            this.rockCreekCheckBox.Checked = true;
            this.rockCreekCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rockCreekCheckBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F);
            this.rockCreekCheckBox.Location = new System.Drawing.Point(276, 60);
            this.rockCreekCheckBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.rockCreekCheckBox.Name = "rockCreekCheckBox";
            this.rockCreekCheckBox.Size = new System.Drawing.Size(163, 39);
            this.rockCreekCheckBox.TabIndex = 2;
            this.rockCreekCheckBox.Text = "&Rock Creek";
            this.rockCreekCheckBox.UseVisualStyleBackColor = true;
            // 
            // sylvaniaCheckBox
            // 
            this.sylvaniaCheckBox.AutoSize = true;
            this.sylvaniaCheckBox.Checked = true;
            this.sylvaniaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sylvaniaCheckBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F);
            this.sylvaniaCheckBox.Location = new System.Drawing.Point(136, 60);
            this.sylvaniaCheckBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.sylvaniaCheckBox.Name = "sylvaniaCheckBox";
            this.sylvaniaCheckBox.Size = new System.Drawing.Size(126, 39);
            this.sylvaniaCheckBox.TabIndex = 1;
            this.sylvaniaCheckBox.Text = "S&ylvania";
            this.sylvaniaCheckBox.UseVisualStyleBackColor = true;
            // 
            // sendingEmailsLabel
            // 
            this.sendingEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendingEmailsLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.sendingEmailsLabel.Location = new System.Drawing.Point(528, 105);
            this.sendingEmailsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.sendingEmailsLabel.Name = "sendingEmailsLabel";
            this.sendingEmailsLabel.Size = new System.Drawing.Size(417, 35);
            this.sendingEmailsLabel.TabIndex = 12;
            this.sendingEmailsLabel.Text = "sendingEmailsLabel";
            // 
            // succeededNotificationsLabel
            // 
            this.succeededNotificationsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.succeededNotificationsLabel.ForeColor = System.Drawing.Color.ForestGreen;
            this.succeededNotificationsLabel.Location = new System.Drawing.Point(528, 140);
            this.succeededNotificationsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.succeededNotificationsLabel.Name = "succeededNotificationsLabel";
            this.succeededNotificationsLabel.Size = new System.Drawing.Size(417, 35);
            this.succeededNotificationsLabel.TabIndex = 13;
            this.succeededNotificationsLabel.Text = "succeededEmailsLabel";
            // 
            // failedNotificationsLabel
            // 
            this.failedNotificationsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedNotificationsLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.failedNotificationsLabel.Location = new System.Drawing.Point(528, 175);
            this.failedNotificationsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.failedNotificationsLabel.Name = "failedNotificationsLabel";
            this.failedNotificationsLabel.Size = new System.Drawing.Size(417, 35);
            this.failedNotificationsLabel.TabIndex = 14;
            this.failedNotificationsLabel.Text = "failedEmailsLabel";
            // 
            // NotificationSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 631);
            this.Controls.Add(this.sendingEmailsLabel);
            this.Controls.Add(this.succeededNotificationsLabel);
            this.Controls.Add(this.failedNotificationsLabel);
            this.Controls.Add(this.locationsLabel);
            this.Controls.Add(this.cascadeCheckBox);
            this.Controls.Add(this.southeastCheckBox);
            this.Controls.Add(this.rockCreekCheckBox);
            this.Controls.Add(this.sylvaniaCheckBox);
            this.Controls.Add(this.messageRichTextBox);
            this.Controls.Add(this.tagsPanel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.templateComboBox);
            this.Controls.Add(this.templateLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.subjectTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 670);
            this.Name = "NotificationSender";
            this.Text = "Send Notifications";
            this.Load += new System.EventHandler(this.Story2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RichTextBox messageRichTextBox;
        private System.Windows.Forms.ComboBox templateComboBox;
        private System.Windows.Forms.Label templateLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label subjectLabel;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel tagsPanel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label locationsLabel;
        private System.Windows.Forms.CheckBox cascadeCheckBox;
        private System.Windows.Forms.CheckBox southeastCheckBox;
        private System.Windows.Forms.CheckBox rockCreekCheckBox;
        private System.Windows.Forms.CheckBox sylvaniaCheckBox;
        private System.Windows.Forms.Label sendingEmailsLabel;
        private System.Windows.Forms.Label succeededNotificationsLabel;
        private System.Windows.Forms.Label failedNotificationsLabel;
    }
}

