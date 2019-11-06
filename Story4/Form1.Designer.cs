namespace Story4
{
    partial class Form1
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
            this.notificationDataGridView = new System.Windows.Forms.DataGridView();
            this.findButton = new System.Windows.Forms.Button();
            this.firstDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.secondDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.notificationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // notificationDataGridView
            // 
            this.notificationDataGridView.AllowUserToAddRows = false;
            this.notificationDataGridView.AllowUserToDeleteRows = false;
            this.notificationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notificationDataGridView.Location = new System.Drawing.Point(27, 435);
            this.notificationDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.notificationDataGridView.Name = "notificationDataGridView";
            this.notificationDataGridView.ReadOnly = true;
            this.notificationDataGridView.Size = new System.Drawing.Size(1434, 504);
            this.notificationDataGridView.TabIndex = 0;
            // 
            // findButton
            // 
            this.findButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findButton.Location = new System.Drawing.Point(152, 336);
            this.findButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(240, 82);
            this.findButton.TabIndex = 1;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // firstDateTimePicker
            // 
            this.firstDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstDateTimePicker.Location = new System.Drawing.Point(398, 176);
            this.firstDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.firstDateTimePicker.Name = "firstDateTimePicker";
            this.firstDateTimePicker.Size = new System.Drawing.Size(688, 53);
            this.firstDateTimePicker.TabIndex = 2;
            // 
            // secondDateTimePicker
            // 
            this.secondDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondDateTimePicker.Location = new System.Drawing.Point(398, 256);
            this.secondDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.secondDateTimePicker.Name = "secondDateTimePicker";
            this.secondDateTimePicker.Size = new System.Drawing.Size(688, 53);
            this.secondDateTimePicker.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(561, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 47);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Date Range";
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(1086, 336);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(240, 82);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1479, 957);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secondDateTimePicker);
            this.Controls.Add(this.firstDateTimePicker);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.notificationDataGridView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Notification Review";
            ((System.ComponentModel.ISupportInitialize)(this.notificationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView notificationDataGridView;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.DateTimePicker firstDateTimePicker;
        private System.Windows.Forms.DateTimePicker secondDateTimePicker;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button clearButton;
    }
}

