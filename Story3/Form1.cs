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
using TagLibrary;
using TemplateLibrary;

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

        // Loads the templateCreator, adds all of the tags to the tag combo box from the db.
        private void templateCreator_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(this.Width, this.Height);
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            List<Tag> myTagList = new List<Tag>();
            TagDB.Load(ref myTagList);
            List<Template> myTemplateList = new List<Template>();
            TemplateDB.Load(ref myTemplateList);

            foreach(Tag myTag in myTagList)
            {
                customTagComboBox.Items.Add(myTag.Name);
            }
            
            foreach(Template template in myTemplateList)
            {
                templateSelectorComboBox.Items.Add(template.Subject);
            }
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
        // If the tag is blank, it closes out.
        // If the tag already exists, warns user to select from the drop down.
        private void customTagButton_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Please enter the name of the tag you would like to create: ", "New Tag", "");
            if (!customTagComboBox.Items.Contains(input.ToLower()) && input != "")
            {
                Tag myTag = new Tag();
                myTag.Name = input.ToLower();
                TagDB.Add(myTag);
                customTagComboBox.Items.Add(input.ToLower());
                templateRichTextBox.SelectedText = "{$" + input.ToLower() + "}";
            }
            else if (customTagComboBox.Items.Contains(input.ToLower()))
            {
                MessageBox.Show("Custom Tag already exists, inserting into the RichTexttention");
                templateRichTextBox.SelectedText = "{$" + input.ToLower() + "}";
            }
            else
            {
                return;
            }
        }

        // Inserts selected tag into RTB.
        private void customTagComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText =  "{$" + customTagComboBox.SelectedItem.ToString() + "}";
        }

        // Saves the template to the database with the subject being the name for the template.
        private void saveButton_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Please enter the subject of your template before saving: ", "Save Template", "");
            if (!templateSelectorComboBox.Items.Contains(input.ToLower()) && input != "")
            {
                Template myTemplate = new Template();
                myTemplate.Subject = input;
                myTemplate.Message = templateRichTextBox.Text;
                myTemplate.CreatedAccountId = 1;
                myTemplate.CreatedDate = DateTime.Now;
                TemplateDB.Add(myTemplate);
                templateSelectorComboBox.Items.Add(input.ToLower());
            }
            else if(templateSelectorComboBox.Items.Contains(input.ToLower()))
            {
                MessageBox.Show("Template already exists under that subject! Please select the template from the dropdown box.", "Warning!");
            }
            else
            {
                return;
            }
        }

        private void templateSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
