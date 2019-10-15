using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Story3
{
    public partial class templateCreator : Form
    {
        public templateCreator()
        {
            InitializeComponent();
        }

        private void templateCreator_Load(object sender, EventArgs e)
        {

        }

        private void studentNameTagButton_Click(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText = "{$student.name}";
        }

        private void staffNameTagButton_Click(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText = "{$staff.name}";
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            DialogResult clear = MessageBox.Show("You are about to clear the template text box. Are you sure?", "Warning!", MessageBoxButtons.YesNo);
            if (clear == DialogResult.Yes)
            {
                templateRichTextBox.Text = null;
            }
        }
    }
}
