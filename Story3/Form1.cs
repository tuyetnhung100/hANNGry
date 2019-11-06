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
using AccountLibrary;

// This form is for creating templates for the notification sending in Story 2.
// Author: Nic Zern
namespace Story3
{
    public partial class templateCreator : Form
    {
        private List<Tag> tags = new List<Tag>();
        private List<Template> templates = new List<Template>();
        private Account employee = new Account();
        private const string DatabaseError = "Database Error";

        public templateCreator()
        {
            InitializeComponent();
        }

        // Borrowed this from Story 2 to load in the account id in place of the 1 I had originally set for testing purposes. 
        // Loads all the employees from the database.
        private void LoadEmployee()
        {
            if (!AccountDB.FakeGetLoginedEmployee(ref employee))
            {
                MessageBox.Show(DatabaseError, "Loading Employee failed!");
            }
        }

        // Loads the templateCreator, adds all of the tags to the tag combo box from the db.
        private void templateCreator_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(this.Width, this.Height);
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            
            TemplateDB.Load(ref templates);
            TagDB.Load(ref tags);


            foreach (Tag myTag in tags)
            {
                customTagComboBox.Items.Add(myTag.Name);
            }

            foreach (Template template in templates)
            {
                templateSelectorComboBox.Items.Add(template);
            }
            templateSelectorComboBox.SelectedIndex = 0;
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
        // If the template has the same subject as one already in the DB, prompts user to make sure they want to overwrite.
        // Then, updates the template in the database.
        private void saveButton_Click(object sender, EventArgs e)
        {
            string currentItem = templateSelectorComboBox.SelectedItem.ToString();
            string input = Interaction.InputBox("Please enter the subject of your template before saving: ", "Save Template", "");
            if (templateRichTextBox.Text == "")
            {
                templateErrorProvider.SetError(templateRichTextBox, "Cannot save a blank template! Please try again.");
                return;
            }
            else if (currentItem != input && input != "")
            {
                Template myTemplate = new Template();
                myTemplate.Subject = input;
                myTemplate.Message = templateRichTextBox.Text;
                myTemplate.CreatedAccountId = 1;
                myTemplate.CreatedDate = DateTime.Now;
                TemplateDB.Add(myTemplate);
                templateSelectorComboBox.Items.Add(myTemplate);
                templateSelectorComboBox.SelectedItem.Equals(myTemplate);
            }
            else if (currentItem == input)
            {
                DialogResult save = MessageBox.Show("Template already exists under that subject! " +
                    "Are you sure you want to overwrite the previous template?", "Warning!", MessageBoxButtons.YesNo);
                if (save == DialogResult.Yes)
                {
                    Template myTemplate = new Template();
                    myTemplate.Subject = input;
                    myTemplate.Message = templateRichTextBox.Text;
                    myTemplate.CreatedAccountId = employee.AccountId;
                    myTemplate.CreatedDate = DateTime.Now;
                    TemplateDB.Update(myTemplate);
                    templateSelectorComboBox.SelectedItem.Equals(myTemplate);
                }
            }
            else
            {
                return;
            }

            foreach (Template template in templates)
            {
                templateSelectorComboBox.Items.Add(template);
            }
            templateSelectorComboBox.Items.Clear();
            TemplateDB.Load(ref templates);
            foreach (Template template in templates)
            {
                templateSelectorComboBox.Items.Add(template);
            }
        }

        // Loads a template from the DB when selected, and inserts the message column into the RTB.
        private void templateSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Template template = (Template)templateSelectorComboBox.SelectedItem;
            templateRichTextBox.Text = template.Message;
        }
    }
}
