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
            this.components = new System.ComponentModel.Container();
            this.templateRichTextBox = new System.Windows.Forms.RichTextBox();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.customTagButton = new System.Windows.Forms.Button();
            this.templateSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.templateSelectionLabel = new System.Windows.Forms.Label();
            this.customTagComboBox = new System.Windows.Forms.ComboBox();
            this.tagSelectorLabel = new System.Windows.Forms.Label();
            this.templateErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.templateErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // templateRichTextBox
            // 
            this.templateRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateRichTextBox.Location = new System.Drawing.Point(28, 183);
            this.templateRichTextBox.Name = "templateRichTextBox";
            this.templateRichTextBox.Size = new System.Drawing.Size(847, 644);
            this.templateRichTextBox.TabIndex = 5;
            this.templateRichTextBox.Text = "";
            // 
            // clearAllButton
            // 
            this.clearAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearAllButton.Location = new System.Drawing.Point(538, 864);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(250, 107);
            this.clearAllButton.TabIndex = 8;
            this.clearAllButton.Text = "&Clear All";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(106, 864);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(250, 107);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // customTagButton
            // 
            this.customTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customTagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTagButton.Location = new System.Drawing.Point(968, 184);
            this.customTagButton.Name = "customTagButton";
            this.customTagButton.Size = new System.Drawing.Size(272, 107);
            this.customTagButton.TabIndex = 6;
            this.customTagButton.Text = "&Insert Custom Tag";
            this.customTagButton.UseVisualStyleBackColor = true;
            this.customTagButton.Click += new System.EventHandler(this.customTagButton_Click);
            // 
            // templateSelectorComboBox
            // 
            this.templateSelectorComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateSelectorComboBox.FormattingEnabled = true;
            this.templateSelectorComboBox.Location = new System.Drawing.Point(309, 119);
            this.templateSelectorComboBox.Name = "templateSelectorComboBox";
            this.templateSelectorComboBox.Size = new System.Drawing.Size(418, 55);
            this.templateSelectorComboBox.TabIndex = 2;
            this.templateSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.templateSelectorComboBox_SelectedIndexChanged);
            // 
            // templateSelectionLabel
            // 
            this.templateSelectionLabel.AutoSize = true;
            this.templateSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateSelectionLabel.Location = new System.Drawing.Point(24, 132);
            this.templateSelectionLabel.Name = "templateSelectionLabel";
            this.templateSelectionLabel.Size = new System.Drawing.Size(275, 37);
            this.templateSelectionLabel.TabIndex = 1;
            this.templateSelectionLabel.Text = "T&emplate Selector";
            // 
            // customTagComboBox
            // 
            this.customTagComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customTagComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTagComboBox.FormattingEnabled = true;
            this.customTagComboBox.Location = new System.Drawing.Point(946, 119);
            this.customTagComboBox.Name = "customTagComboBox";
            this.customTagComboBox.Size = new System.Drawing.Size(310, 55);
            this.customTagComboBox.TabIndex = 4;
            this.customTagComboBox.SelectedIndexChanged += new System.EventHandler(this.customTagComboBox_SelectedIndexChanged);
            // 
            // tagSelectorLabel
            // 
            this.tagSelectorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tagSelectorLabel.AutoSize = true;
            this.tagSelectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagSelectorLabel.Location = new System.Drawing.Point(1002, 77);
            this.tagSelectorLabel.Name = "tagSelectorLabel";
            this.tagSelectorLabel.Size = new System.Drawing.Size(198, 37);
            this.tagSelectorLabel.TabIndex = 3;
            this.tagSelectorLabel.Text = "&Tag Selector";
            // 
            // templateErrorProvider
            // 
            this.templateErrorProvider.ContainerControl = this;
            // 
            // templateCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1323, 1004);
            this.Controls.Add(this.tagSelectorLabel);
            this.Controls.Add(this.customTagComboBox);
            this.Controls.Add(this.templateSelectionLabel);
            this.Controls.Add(this.templateSelectorComboBox);
            this.Controls.Add(this.customTagButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.clearAllButton);
            this.Controls.Add(this.templateRichTextBox);
            this.Name = "templateCreator";
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
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button customTagButton;
        private System.Windows.Forms.ComboBox templateSelectorComboBox;
        private System.Windows.Forms.Label templateSelectionLabel;
        private System.Windows.Forms.ComboBox customTagComboBox;
        private System.Windows.Forms.Label tagSelectorLabel;
        private System.Windows.Forms.ErrorProvider templateErrorProvider;
    }
}

