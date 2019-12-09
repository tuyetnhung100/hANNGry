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
    }
}
