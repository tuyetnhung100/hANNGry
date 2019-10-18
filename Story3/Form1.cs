using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

// This form is for creating templates for the notification sending in Story 2.
// Author: Nic Zern
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

        // Inserts the tag for the student name.
        private void studentNameTagButton_Click(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText = "{$student.name}";
        }

        // Inserts the tag for the staff name.
        private void staffNameTagButton_Click(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText = "{$staff.name}";
        }

        // Clears the rich text box.
        private void clearAllButton_Click(object sender, EventArgs e)
        {
            DialogResult clear = MessageBox.Show("You are about to clear the template text box. Are you sure?", "Warning!", MessageBoxButtons.YesNo);
            if (clear == DialogResult.Yes)
            {
                templateRichTextBox.Text = null;
            }
        }

        // Promts the user for a tag name, then inserts the tag into the RTB.
        private void customTagButton_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Please enter the name of the tag you would like to create: ", "New Tag", "");
            customTagComboBox.Items.Add("{$" + input + "}");
            templateRichTextBox.SelectedText = "{$" + input + "}";
        }

        // Inserts selected tag into RTB.
        private void customTagComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText = customTagComboBox.SelectedItem.ToString();
        }
    }
}
