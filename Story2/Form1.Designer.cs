namespace Story2
{
    partial class Story2
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
            this.sendButton = new System.Windows.Forms.Button();
            this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.templateLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.tagsPanel = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.sendingEmailsLabel = new System.Windows.Forms.Label();
            this.succeededEmailsLabel = new System.Windows.Forms.Label();
            this.failedEmailsLabel = new System.Windows.Forms.Label();
            this.cancelledEmailsLabel = new System.Windows.Forms.Label();
            this.messagePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.messagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendButton.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendButton.Location = new System.Drawing.Point(120, 568);
            this.sendButton.Margin = new System.Windows.Forms.Padding(1);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(180, 53);
            this.sendButton.TabIndex = 7;
            this.sendButton.Text = "&Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.messageRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageRichTextBox.Enabled = false;
            this.messageRichTextBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F);
            this.messageRichTextBox.Location = new System.Drawing.Point(2, 3);
            this.messageRichTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.Size = new System.Drawing.Size(365, 397);
            this.messageRichTextBox.TabIndex = 6;
            this.messageRichTextBox.Text = "";
            // 
            // templateComboBox
            // 
            this.templateComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.templateComboBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(120, 53);
            this.templateComboBox.Margin = new System.Windows.Forms.Padding(1);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(371, 41);
            this.templateComboBox.TabIndex = 2;
            this.templateComboBox.SelectedIndexChanged += new System.EventHandler(this.TemplateComboBox_SelectedIndexChanged);
            // 
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateLabel.Location = new System.Drawing.Point(5, 55);
            this.templateLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(118, 35);
            this.templateLabel.TabIndex = 1;
            this.templateLabel.Text = "&Template";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(5, 158);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(115, 35);
            this.messageLabel.TabIndex = 5;
            this.messageLabel.Text = "&Message";
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectLabel.Location = new System.Drawing.Point(5, 107);
            this.subjectLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(97, 35);
            this.subjectLabel.TabIndex = 3;
            this.subjectLabel.Text = "S&ubject";
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectTextBox.Location = new System.Drawing.Point(120, 105);
            this.subjectTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(371, 41);
            this.subjectTextBox.TabIndex = 4;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(5, 9);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(263, 43);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Send Notification";
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(311, 568);
            this.clearButton.Margin = new System.Windows.Forms.Padding(1);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(180, 53);
            this.clearButton.TabIndex = 8;
            this.clearButton.Text = "&Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // tagsPanel
            // 
            this.tagsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tagsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tagsPanel.Location = new System.Drawing.Point(518, 157);
            this.tagsPanel.Margin = new System.Windows.Forms.Padding(1);
            this.tagsPanel.Name = "tagsPanel";
            this.tagsPanel.Size = new System.Drawing.Size(369, 405);
            this.tagsPanel.TabIndex = 9;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // sendingEmailsLabel
            // 
            this.sendingEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendingEmailsLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.sendingEmailsLabel.Location = new System.Drawing.Point(512, 10);
            this.sendingEmailsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.sendingEmailsLabel.Name = "sendingEmailsLabel";
            this.sendingEmailsLabel.Size = new System.Drawing.Size(417, 35);
            this.sendingEmailsLabel.TabIndex = 10;
            this.sendingEmailsLabel.Text = "sendingEmailsLabel";
            // 
            // succeededEmailsLabel
            // 
            this.succeededEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.succeededEmailsLabel.ForeColor = System.Drawing.Color.ForestGreen;
            this.succeededEmailsLabel.Location = new System.Drawing.Point(512, 45);
            this.succeededEmailsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.succeededEmailsLabel.Name = "succeededEmailsLabel";
            this.succeededEmailsLabel.Size = new System.Drawing.Size(417, 35);
            this.succeededEmailsLabel.TabIndex = 11;
            this.succeededEmailsLabel.Text = "succeededEmailsLabel";
            // 
            // failedEmailsLabel
            // 
            this.failedEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedEmailsLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.failedEmailsLabel.Location = new System.Drawing.Point(512, 79);
            this.failedEmailsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.failedEmailsLabel.Name = "failedEmailsLabel";
            this.failedEmailsLabel.Size = new System.Drawing.Size(417, 35);
            this.failedEmailsLabel.TabIndex = 12;
            this.failedEmailsLabel.Text = "failedEmailsLabel";
            // 
            // cancelledEmailsLabel
            // 
            this.cancelledEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelledEmailsLabel.ForeColor = System.Drawing.Color.Gray;
            this.cancelledEmailsLabel.Location = new System.Drawing.Point(512, 113);
            this.cancelledEmailsLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.cancelledEmailsLabel.Name = "cancelledEmailsLabel";
            this.cancelledEmailsLabel.Size = new System.Drawing.Size(417, 35);
            this.cancelledEmailsLabel.TabIndex = 13;
            this.cancelledEmailsLabel.Text = "cancelledEmailsLabel";
            // 
            // messagePanel
            // 
            this.messagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.messagePanel.BackColor = System.Drawing.SystemColors.Window;
            this.messagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messagePanel.Controls.Add(this.messageRichTextBox);
            this.messagePanel.Location = new System.Drawing.Point(120, 157);
            this.messagePanel.Margin = new System.Windows.Forms.Padding(1);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.messagePanel.Size = new System.Drawing.Size(371, 405);
            this.messagePanel.TabIndex = 10;
            // 
            // Story2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 631);
            this.Controls.Add(this.messagePanel);
            this.Controls.Add(this.cancelledEmailsLabel);
            this.Controls.Add(this.failedEmailsLabel);
            this.Controls.Add(this.succeededEmailsLabel);
            this.Controls.Add(this.sendingEmailsLabel);
            this.Controls.Add(this.tagsPanel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.templateComboBox);
            this.Controls.Add(this.templateLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.titleLabel);
            this.MinimumSize = new System.Drawing.Size(1024, 670);
            this.Name = "Story2";
            this.Text = "Send Notification";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Story2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.messagePanel.ResumeLayout(false);
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
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel tagsPanel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label sendingEmailsLabel;
        private System.Windows.Forms.Label cancelledEmailsLabel;
        private System.Windows.Forms.Label failedEmailsLabel;
        private System.Windows.Forms.Label succeededEmailsLabel;
        private System.Windows.Forms.Panel messagePanel;
    }
}

