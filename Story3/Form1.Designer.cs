namespace Story3
{
    partial class TemplateCreator
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
            this.templateRichTextBox = new System.Windows.Forms.RichTextBox();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.customTagButton = new System.Windows.Forms.Button();
            this.templateSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.templateSelectionLabel = new System.Windows.Forms.Label();
            this.customTagComboBox = new System.Windows.Forms.ComboBox();
            this.tagSelectorLabel = new System.Windows.Forms.Label();
            this.templateErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.deleteButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.templateErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // templateRichTextBox
            // 
            this.templateRichTextBox.AcceptsTab = true;
            this.templateRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.templateRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateRichTextBox.Location = new System.Drawing.Point(19, 132);
            this.templateRichTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.templateRichTextBox.Name = "templateRichTextBox";
            this.templateRichTextBox.Size = new System.Drawing.Size(566, 355);
            this.templateRichTextBox.TabIndex = 5;
            this.templateRichTextBox.Text = "";
            // 
            // clearAllButton
            // 
            this.clearAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearAllButton.Location = new System.Drawing.Point(310, 491);
            this.clearAllButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(130, 77);
            this.clearAllButton.TabIndex = 8;
            this.clearAllButton.Text = "&Clear All";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveAsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAsButton.Location = new System.Drawing.Point(166, 491);
            this.saveAsButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(130, 77);
            this.saveAsButton.TabIndex = 7;
            this.saveAsButton.Text = "S&ave As";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // customTagButton
            // 
            this.customTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customTagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTagButton.Location = new System.Drawing.Point(631, 133);
            this.customTagButton.Margin = new System.Windows.Forms.Padding(2);
            this.customTagButton.Name = "customTagButton";
            this.customTagButton.Size = new System.Drawing.Size(208, 61);
            this.customTagButton.TabIndex = 6;
            this.customTagButton.Text = "&Insert New Custom Tag";
            this.customTagButton.UseVisualStyleBackColor = true;
            this.customTagButton.Click += new System.EventHandler(this.customTagButton_Click);
            // 
            // templateSelectorComboBox
            // 
            this.templateSelectorComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateSelectorComboBox.FormattingEnabled = true;
            this.templateSelectorComboBox.Location = new System.Drawing.Point(206, 86);
            this.templateSelectorComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.templateSelectorComboBox.Name = "templateSelectorComboBox";
            this.templateSelectorComboBox.Size = new System.Drawing.Size(379, 39);
            this.templateSelectorComboBox.Sorted = true;
            this.templateSelectorComboBox.TabIndex = 2;
            this.templateSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.templateSelectorComboBox_SelectedIndexChanged);
            // 
            // templateSelectionLabel
            // 
            this.templateSelectionLabel.AutoSize = true;
            this.templateSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateSelectionLabel.Location = new System.Drawing.Point(16, 95);
            this.templateSelectionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.templateSelectionLabel.Name = "templateSelectionLabel";
            this.templateSelectionLabel.Size = new System.Drawing.Size(186, 25);
            this.templateSelectionLabel.TabIndex = 1;
            this.templateSelectionLabel.Text = "T&emplate Selector";
            // 
            // customTagComboBox
            // 
            this.customTagComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customTagComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTagComboBox.FormattingEnabled = true;
            this.customTagComboBox.Location = new System.Drawing.Point(631, 86);
            this.customTagComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.customTagComboBox.Name = "customTagComboBox";
            this.customTagComboBox.Size = new System.Drawing.Size(208, 39);
            this.customTagComboBox.Sorted = true;
            this.customTagComboBox.TabIndex = 4;
            this.customTagComboBox.SelectedIndexChanged += new System.EventHandler(this.customTagComboBox_SelectedIndexChanged);
            // 
            // tagSelectorLabel
            // 
            this.tagSelectorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tagSelectorLabel.AutoSize = true;
            this.tagSelectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagSelectorLabel.Location = new System.Drawing.Point(668, 54);
            this.tagSelectorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tagSelectorLabel.Name = "tagSelectorLabel";
            this.tagSelectorLabel.Size = new System.Drawing.Size(134, 25);
            this.tagSelectorLabel.TabIndex = 3;
            this.tagSelectorLabel.Text = "&Tag Selector";
            // 
            // templateErrorProvider
            // 
            this.templateErrorProvider.ContainerControl = this;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(455, 491);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(130, 77);
            this.deleteButton.TabIndex = 9;
            this.deleteButton.Text = "&Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(19, 491);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(130, 77);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // TemplateCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 581);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.tagSelectorLabel);
            this.Controls.Add(this.customTagComboBox);
            this.Controls.Add(this.templateSelectionLabel);
            this.Controls.Add(this.templateSelectorComboBox);
            this.Controls.Add(this.customTagButton);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.clearAllButton);
            this.Controls.Add(this.templateRichTextBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TemplateCreator";
            this.ShowIcon = false;
            this.Text = "Template Creator";
            this.Load += new System.EventHandler(this.templateCreator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.templateErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox templateRichTextBox;
        private System.Windows.Forms.Button clearAllButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.Button customTagButton;
        private System.Windows.Forms.ComboBox templateSelectorComboBox;
        private System.Windows.Forms.Label templateSelectionLabel;
        private System.Windows.Forms.ComboBox customTagComboBox;
        private System.Windows.Forms.Label tagSelectorLabel;
        private System.Windows.Forms.ErrorProvider templateErrorProvider;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button saveButton;
    }
}

