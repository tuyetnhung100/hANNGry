using AccountLibrary;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagLibrary;
using TemplateLibrary;

// This form is for creating templates for the notification sending in Story 2.
// Author: Nic Zern
namespace Story3
{
    public partial class TemplateCreator : Form
    {
        public static Account LoginedEmployee;
        
        private List<Tag> tags = new List<Tag>();
        private List<Template> templates = new List<Template>();
        private const string DatabaseError = "Database Error";

        
        public TemplateCreator()
        {
            InitializeComponent();
        }

        // Loads the templateCreator, adds all of the tags to the tag combo box from the db.
        private void templateCreator_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(this.Width, this.Height);
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //this.AutoSize = true;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            templateSelectorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            customTagComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            TemplateDB.Load(ref templates);
            TagDB.Load(ref tags);

            foreach (Tag myTag in tags)
            {
                customTagComboBox.Items.Add(myTag.Name);
            }

            reloadTemplateList();

            if (!TemplateDB.Load(ref templates))
            {
                MessageBox.Show("Unable to retrieve templates from the database. Now closing program.", "Error!");
                this.Close();
            }
        }

        // Updates the currently selected template with whatever is in the RTB.
        private void updateTemplate()
        {
            Template myTemplate = new Template();
            myTemplate.Subject = templateSelectorComboBox.SelectedItem.ToString();
            myTemplate.Message = templateRichTextBox.Text;
            myTemplate.CreatedAccountId = LoginedEmployee.AccountId;
            myTemplate.CreatedDate = DateTime.Now;
            TemplateDB.Update(myTemplate);
            reloadTemplateList();
        }

        // Reloads the templates and sets the index to the first item.
        private void reloadTemplateList()
        {
            templateSelectorComboBox.Items.Clear();

            Template blank = new Template();
            blank.Subject = "";
            blank.Message = "";

            templateSelectorComboBox.Items.Add(blank);

            TemplateDB.Load(ref templates);
            foreach (Template template in templates)
            {
                templateSelectorComboBox.Items.Add(template);
            }

            templateSelectorComboBox.SelectedIndex = 0;
        }

        // Clears the rich text box and reloads the template list.
        private void clearAllButton_Click(object sender, EventArgs e)
        {
            DialogResult clear = MessageBox.Show("You are about to clear the template text box. Are you sure?", "Warning!", MessageBoxButtons.YesNo);
            if (clear == DialogResult.Yes)
            {
                reloadTemplateList();
            }
        }

        // Prompts the user for a tag name, then inserts the tag into the RTB.
        // If the tag is blank, it closes out.
        // If the tag already exists, warns user to select from the drop down.
        private void customTagButton_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Please enter the name of the tag you would like to create: ", "New Tag", "");

            // If the custom tag box doesn't contain the tag and the input isn't blank, it will create a new tag and insert it into the DB.
            if (!customTagComboBox.Items.Contains(input.ToLower()) && input != "")
            {
                Tag myTag = new Tag();
                myTag.Name = input.ToLower();
                TagDB.Add(myTag);
                customTagComboBox.Items.Add(input.ToLower());
                templateRichTextBox.SelectedText = "{$" + input.ToLower() + "}";
            }

            // If the combo box contains the tag, it will alert the user and insert the tag into the text box.
            else if (customTagComboBox.Items.Contains(input.ToLower()))
            {
                MessageBox.Show("Custom Tag already exists, inserting into the RichTextBox.", "attention");
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
            templateRichTextBox.SelectedText = "{$" + customTagComboBox.SelectedItem.ToString() + "}";
            templateRichTextBox.Focus();
        }

        // Saves the template to the database with the subject being the name for the template.
        // If the template has the same subject as one already in the DB, prompts user to make sure they want to overwrite.
        // Then, updates the template in the database.
        private void saveAsButton_Click(object sender, EventArgs e)
        {
            using (Form3 form3 = new Form3())
            {
                if (form3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    String input = form3.SelectedText;
                    int index = templateSelectorComboBox.FindStringExact(input);

                    // If the template RTB is blank, it will not save the template.
                    if (templateRichTextBox.Text == "")
                    {
                        templateErrorProvider.SetError(templateRichTextBox, "Cannot save a blank template! Please try again.");
                        return;
                    }

                    // if index is anything but -1 (that means it's found a template with that name), then it selects that template and saves over the current text.
                    else if (index != -1)
                    {
                        templateRichTextBox.SelectAll();
                        templateRichTextBox.Cut();
                        templateSelectorComboBox.SelectedIndex = index;
                        templateRichTextBox.SelectAll();
                        templateRichTextBox.Paste();
                        Clipboard.Clear();
                        updateTemplate();
                        return;
                    }

                    // If the previous statements return false, then it saves the template as a new template.
                    else if (input != "" && input !=null)
                    {
                        Template myTemplate = new Template();
                        myTemplate.Subject = input;
                        myTemplate.Message = templateRichTextBox.Text;
                        myTemplate.CreatedAccountId = LoginedEmployee.AccountId;
                        myTemplate.CreatedDate = DateTime.Now;
                        TemplateDB.Add(myTemplate);
                        reloadTemplateList();
                    }

                    else
                    {
                        return;
                    }
                    templateErrorProvider.Clear();
                }
            }
        }

        // Loads a template from the DB when selected, and inserts the message column into the RTB.
        private void templateSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Template template = (Template)templateSelectorComboBox.SelectedItem;
            templateRichTextBox.Text = template.Message;
        }

        // Determines whether or not the user wants to delete the selected template, and does so if they click yes.
        private void deleteButton_Click(object sender, EventArgs e)
        {
            String currentItem = templateSelectorComboBox.SelectedItem.ToString();
            Template template = (Template)templateSelectorComboBox.SelectedItem;

            // If the subject of the template is equal to the currently selected item, then it will delete the template from the DB if the user clicks yes.
            if (template.Subject == currentItem)
            {
                DialogResult delete = MessageBox.Show("The selected template is about to be deleted!\n" +
                    "Are you sure you want to delete the template?\nTHE TEMPLATE CANNOT BE RECOVERED.",
                    "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (delete == DialogResult.Yes)
                {
                    TemplateDB.Delete(template);
                    reloadTemplateList();
                }
                else
                {
                    return;
                }
                reloadTemplateList();
            }
        }

        // Saves the template as the currently selected template in the combo box. If there's no template selected (if blank is selected, for instance)
        // then it clicks the save as button and runs through that process.
        private void saveButton_Click(object sender, EventArgs e)
        {
            // If the text for the box either doesn't exist, or is blank, it will click the saveAs button.
            if (templateSelectorComboBox.SelectedItem == null || templateSelectorComboBox.SelectedItem.ToString() == "")
            {
                saveAsButton.PerformClick();
            }

            // If the template RTB is blank, it will not allow the user to save.
            else if (templateRichTextBox.Text == "")
            {
                templateErrorProvider.SetError(templateRichTextBox, "Cannot save a blank template! Please try again.");
                return;
            }

            // If the two previous statements are false, it will update the selected template.
            else
            {
                updateTemplate();
                reloadTemplateList();
            }
        }
    }
}
