using System;
using System.Windows.Forms;

namespace Story3
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
            SelectedText = subjectTextBox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
