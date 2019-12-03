using System;
using System.Windows.Forms;

namespace Story4
{
    public partial class Form2 : Form
    {
        public Form2(string dataText)
        {
            InitializeComponent();
            messageRichTextBox.Text = dataText;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
