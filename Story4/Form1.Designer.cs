namespace Story4
{
    partial class LogViewer
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
            this.findMessageButton = new System.Windows.Forms.Button();
            this.findTimeMessageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.notificationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // notificationDataGridView
            // 
            this.notificationDataGridView.AllowUserToAddRows = false;
            this.notificationDataGridView.AllowUserToDeleteRows = false;
            this.notificationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notificationDataGridView.Location = new System.Drawing.Point(18, 357);
            this.notificationDataGridView.Name = "notificationDataGridView";
            this.notificationDataGridView.ReadOnly = true;
            this.notificationDataGridView.Size = new System.Drawing.Size(956, 321);
            this.notificationDataGridView.TabIndex = 5;
            this.notificationDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.notificationDataGridView_CellClick);
            // 
            // findButton
            // 
            this.findButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findButton.Location = new System.Drawing.Point(99, 275);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(160, 76);
            this.findButton.TabIndex = 3;
            this.findButton.Text = "&Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // firstDateTimePicker
            // 
            this.firstDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstDateTimePicker.Location = new System.Drawing.Point(265, 166);
            this.firstDateTimePicker.Name = "firstDateTimePicker";
            this.firstDateTimePicker.Size = new System.Drawing.Size(460, 38);
            this.firstDateTimePicker.TabIndex = 1;
            // 
            // secondDateTimePicker
            // 
            this.secondDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondDateTimePicker.Location = new System.Drawing.Point(265, 219);
            this.secondDateTimePicker.Name = "secondDateTimePicker";
            this.secondDateTimePicker.Size = new System.Drawing.Size(460, 38);
            this.secondDateTimePicker.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(373, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Date Range";
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(729, 277);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(160, 74);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "&Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // findMessageButton
            // 
            this.findMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findMessageButton.Location = new System.Drawing.Point(304, 277);
            this.findMessageButton.Name = "findMessageButton";
            this.findMessageButton.Size = new System.Drawing.Size(160, 74);
            this.findMessageButton.TabIndex = 6;
            this.findMessageButton.Text = "Find Message";
            this.findMessageButton.UseVisualStyleBackColor = true;
            this.findMessageButton.Click += new System.EventHandler(this.findMessageButton_Click);
            // 
            // findTimeMessageButton
            // 
            this.findTimeMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findTimeMessageButton.Location = new System.Drawing.Point(510, 277);
            this.findTimeMessageButton.Name = "findTimeMessageButton";
            this.findTimeMessageButton.Size = new System.Drawing.Size(160, 74);
            this.findTimeMessageButton.TabIndex = 7;
            this.findTimeMessageButton.Text = "Find Time && Message";
            this.findTimeMessageButton.UseVisualStyleBackColor = true;
            this.findTimeMessageButton.Click += new System.EventHandler(this.findTimeMessageButton_Click);
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 691);
            this.Controls.Add(this.findTimeMessageButton);
            this.Controls.Add(this.findMessageButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secondDateTimePicker);
            this.Controls.Add(this.firstDateTimePicker);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.notificationDataGridView);
            this.Name = "LogViewer";
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
        private System.Windows.Forms.Button findMessageButton;
        private System.Windows.Forms.Button findTimeMessageButton;
    }
}

