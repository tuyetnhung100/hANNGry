using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Story4
{
    public partial class Form3 : Form
    {
        public string SelectedText { get; set; }
        public Form3()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedText = messageSearchTextBox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
