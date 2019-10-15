namespace Story3
{
    partial class templateCreator
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.templateRichTextBox = new System.Windows.Forms.RichTextBox();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.deleteTemplateButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.studentNameTagButton = new System.Windows.Forms.Button();
            this.staffNameTagButton = new System.Windows.Forms.Button();
            this.customTagButton = new System.Windows.Forms.Button();
            this.templateSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.templateSelectionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(9, 7);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(196, 26);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Template Creator";
            // 
            // templateRichTextBox
            // 
            this.templateRichTextBox.Location = new System.Drawing.Point(14, 54);
            this.templateRichTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.templateRichTextBox.Name = "templateRichTextBox";
            this.templateRichTextBox.Size = new System.Drawing.Size(212, 274);
            this.templateRichTextBox.TabIndex = 1;
            this.templateRichTextBox.Text = "";
            // 
            // clearAllButton
            // 
            this.clearAllButton.Location = new System.Drawing.Point(513, 30);
            this.clearAllButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(78, 52);
            this.clearAllButton.TabIndex = 2;
            this.clearAllButton.Text = "Clear All";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // deleteTemplateButton
            // 
            this.deleteTemplateButton.Location = new System.Drawing.Point(513, 155);
            this.deleteTemplateButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteTemplateButton.Name = "deleteTemplateButton";
            this.deleteTemplateButton.Size = new System.Drawing.Size(78, 52);
            this.deleteTemplateButton.TabIndex = 3;
            this.deleteTemplateButton.Text = "Delete Template";
            this.deleteTemplateButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(513, 275);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(78, 52);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // studentNameTagButton
            // 
            this.studentNameTagButton.Location = new System.Drawing.Point(406, 30);
            this.studentNameTagButton.Margin = new System.Windows.Forms.Padding(2);
            this.studentNameTagButton.Name = "studentNameTagButton";
            this.studentNameTagButton.Size = new System.Drawing.Size(78, 52);
            this.studentNameTagButton.TabIndex = 5;
            this.studentNameTagButton.Text = "Insert Student Name Tag";
            this.studentNameTagButton.UseVisualStyleBackColor = true;
            this.studentNameTagButton.Click += new System.EventHandler(this.studentNameTagButton_Click);
            // 
            // staffNameTagButton
            // 
            this.staffNameTagButton.Location = new System.Drawing.Point(406, 155);
            this.staffNameTagButton.Margin = new System.Windows.Forms.Padding(2);
            this.staffNameTagButton.Name = "staffNameTagButton";
            this.staffNameTagButton.Size = new System.Drawing.Size(78, 52);
            this.staffNameTagButton.TabIndex = 6;
            this.staffNameTagButton.Text = "Insert Staff Name Tag";
            this.staffNameTagButton.UseVisualStyleBackColor = true;
            this.staffNameTagButton.Click += new System.EventHandler(this.staffNameTagButton_Click);
            // 
            // customTagButton
            // 
            this.customTagButton.Location = new System.Drawing.Point(406, 275);
            this.customTagButton.Margin = new System.Windows.Forms.Padding(2);
            this.customTagButton.Name = "customTagButton";
            this.customTagButton.Size = new System.Drawing.Size(78, 52);
            this.customTagButton.TabIndex = 7;
            this.customTagButton.Text = "Insert Custom Tag";
            this.customTagButton.UseVisualStyleBackColor = true;
            // 
            // templateSelectorComboBox
            // 
            this.templateSelectorComboBox.FormattingEnabled = true;
            this.templateSelectorComboBox.Location = new System.Drawing.Point(254, 61);
            this.templateSelectorComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.templateSelectorComboBox.Name = "templateSelectorComboBox";
            this.templateSelectorComboBox.Size = new System.Drawing.Size(108, 21);
            this.templateSelectorComboBox.TabIndex = 8;
            // 
            // templateSelectionLabel
            // 
            this.templateSelectionLabel.AutoSize = true;
            this.templateSelectionLabel.Location = new System.Drawing.Point(252, 45);
            this.templateSelectionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.templateSelectionLabel.Name = "templateSelectionLabel";
            this.templateSelectionLabel.Size = new System.Drawing.Size(93, 13);
            this.templateSelectionLabel.TabIndex = 9;
            this.templateSelectionLabel.Text = "Template Selector";
            // 
            // templateCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.templateSelectionLabel);
            this.Controls.Add(this.templateSelectorComboBox);
            this.Controls.Add(this.customTagButton);
            this.Controls.Add(this.staffNameTagButton);
            this.Controls.Add(this.studentNameTagButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.deleteTemplateButton);
            this.Controls.Add(this.clearAllButton);
            this.Controls.Add(this.templateRichTextBox);
            this.Controls.Add(this.titleLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "templateCreator";
            this.Text = "Template Creator";
            this.Load += new System.EventHandler(this.templateCreator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.RichTextBox templateRichTextBox;
        private System.Windows.Forms.Button clearAllButton;
        private System.Windows.Forms.Button deleteTemplateButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button studentNameTagButton;
        private System.Windows.Forms.Button staffNameTagButton;
        private System.Windows.Forms.Button customTagButton;
        private System.Windows.Forms.ComboBox templateSelectorComboBox;
        private System.Windows.Forms.Label templateSelectionLabel;
    }
}

