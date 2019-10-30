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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.sendingEmailsLabel = new System.Windows.Forms.Label();
            this.succeededEmailsLabel = new System.Windows.Forms.Label();
            this.failedEmailsLabel = new System.Windows.Forms.Label();
            this.cancelledEmailsLabel = new System.Windows.Forms.Label();
            this.tagsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendButton.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendButton.Location = new System.Drawing.Point(236, 1012);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(295, 105);
            this.sendButton.TabIndex = 7;
            this.sendButton.Text = "&Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.messageRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.messageRichTextBox.Enabled = false;
            this.messageRichTextBox.Font = new System.Drawing.Font("Arial Narrow", 13.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageRichTextBox.Location = new System.Drawing.Point(236, 260);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.Size = new System.Drawing.Size(600, 746);
            this.messageRichTextBox.TabIndex = 6;
            this.messageRichTextBox.Text = "";
            // 
            // templateComboBox
            // 
            this.templateComboBox.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(236, 100);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(601, 51);
            this.templateComboBox.TabIndex = 2;
            this.templateComboBox.SelectedIndexChanged += new System.EventHandler(this.TemplateComboBox_SelectedIndexChanged);
            // 
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateLabel.Location = new System.Drawing.Point(25, 104);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(146, 43);
            this.templateLabel.TabIndex = 1;
            this.templateLabel.Text = "&Template";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(25, 260);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(142, 43);
            this.messageLabel.TabIndex = 5;
            this.messageLabel.Text = "&Message";
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectLabel.Location = new System.Drawing.Point(25, 182);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(120, 43);
            this.subjectLabel.TabIndex = 3;
            this.subjectLabel.Text = "S&ubject";
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectTextBox.Location = new System.Drawing.Point(236, 178);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(601, 50);
            this.subjectTextBox.TabIndex = 4;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(7, 19);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(571, 85);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Send Notification";
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(542, 1012);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(295, 105);
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
            this.tagsPanel.Controls.Add(this.textBox2);
            this.tagsPanel.Controls.Add(this.label2);
            this.tagsPanel.Controls.Add(this.textBox1);
            this.tagsPanel.Controls.Add(this.label1);
            this.tagsPanel.Location = new System.Drawing.Point(843, 100);
            this.tagsPanel.Name = "tagsPanel";
            this.tagsPanel.Size = new System.Drawing.Size(800, 906);
            this.tagsPanel.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(231, 212);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(550, 50);
            this.textBox2.TabIndex = 3;
            this.textBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 43);
            this.label2.TabIndex = 2;
            this.label2.Text = "&2. tag2";
            this.label2.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(231, 156);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(550, 50);
            this.textBox1.TabIndex = 1;
            this.textBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "&1. tag1";
            this.label1.Visible = false;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // sendingEmailsLabel
            // 
            this.sendingEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendingEmailsLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.sendingEmailsLabel.Location = new System.Drawing.Point(862, 16);
            this.sendingEmailsLabel.Name = "sendingEmailsLabel";
            this.sendingEmailsLabel.Size = new System.Drawing.Size(904, 58);
            this.sendingEmailsLabel.TabIndex = 10;
            this.sendingEmailsLabel.Text = "sendingEmailsLabel";
            // 
            // succeededEmailsLabel
            // 
            this.succeededEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.succeededEmailsLabel.ForeColor = System.Drawing.Color.ForestGreen;
            this.succeededEmailsLabel.Location = new System.Drawing.Point(862, 74);
            this.succeededEmailsLabel.Name = "succeededEmailsLabel";
            this.succeededEmailsLabel.Size = new System.Drawing.Size(904, 58);
            this.succeededEmailsLabel.TabIndex = 11;
            this.succeededEmailsLabel.Text = "succeededEmailsLabel";
            // 
            // failedEmailsLabel
            // 
            this.failedEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedEmailsLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.failedEmailsLabel.Location = new System.Drawing.Point(862, 132);
            this.failedEmailsLabel.Name = "failedEmailsLabel";
            this.failedEmailsLabel.Size = new System.Drawing.Size(904, 58);
            this.failedEmailsLabel.TabIndex = 12;
            this.failedEmailsLabel.Text = "failedEmailsLabel";
            // 
            // cancelledEmailsLabel
            // 
            this.cancelledEmailsLabel.Font = new System.Drawing.Font("Arial Narrow", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelledEmailsLabel.ForeColor = System.Drawing.Color.Gray;
            this.cancelledEmailsLabel.Location = new System.Drawing.Point(862, 190);
            this.cancelledEmailsLabel.Name = "cancelledEmailsLabel";
            this.cancelledEmailsLabel.Size = new System.Drawing.Size(904, 58);
            this.cancelledEmailsLabel.TabIndex = 13;
            this.cancelledEmailsLabel.Text = "cancelledEmailsLabel";
            // 
            // Story2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2185, 1129);
            this.Controls.Add(this.cancelledEmailsLabel);
            this.Controls.Add(this.failedEmailsLabel);
            this.Controls.Add(this.succeededEmailsLabel);
            this.Controls.Add(this.sendingEmailsLabel);
            this.Controls.Add(this.tagsPanel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageRichTextBox);
            this.Controls.Add(this.templateComboBox);
            this.Controls.Add(this.templateLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.titleLabel);
            this.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.Name = "Story2";
            this.Text = "Send Notification";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Story2_Load);
            this.tagsPanel.ResumeLayout(false);
            this.tagsPanel.PerformLayout();
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
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel tagsPanel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label sendingEmailsLabel;
        private System.Windows.Forms.Label cancelledEmailsLabel;
        private System.Windows.Forms.Label failedEmailsLabel;
        private System.Windows.Forms.Label succeededEmailsLabel;
    }
}

