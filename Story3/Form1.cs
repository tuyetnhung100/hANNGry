﻿using AccountLibrary;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TagLibrary;
using TemplateLibrary;

// This form is for creating templates for the notification sending in Story 2.
// Author: Nic Zern
namespace Story3
{
    public partial class templateCreator : Form
    {
        public static Account LoginedEmployee;

        public templateCreator()
        {
            InitializeComponent();
        }

        // Loads the templateCreator, adds all of the tags to the tag combo box from the db.
        private void templateCreator_Load(object sender, EventArgs e)
        {
            List<Tag> myTagList = new List<Tag>();
            TagDB.Load(ref myTagList);

            foreach (Tag myTag in myTagList)
            {
                customTagComboBox.Items.Add(myTag.Name);
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
            if (!customTagComboBox.Items.Contains("{$" + input + "}") && input != "")
            {
                Tag myTag = new Tag();
                myTag.Name = "{$" + input + "}";
                TagDB.Add(myTag);
                customTagComboBox.Items.Add("{$" + input + "}");
                templateRichTextBox.SelectedText = "{$" + input + "}";
            }
            else if (customTagComboBox.Items.Contains("{$" + input + "}"))
            {
                MessageBox.Show("Custom Tag already exists! Please select the tag from the dropdown box.", "Warning!");
            }
            else
            {
                return;
            }

        }

        // Inserts selected tag into RTB.
        private void customTagComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            templateRichTextBox.SelectedText = customTagComboBox.SelectedItem.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Please name your Template before saving: ", "Save Template", "");
            if (!templateSelectorComboBox.Items.Contains(input) && input != "")
            {
                Template myTemplate = new Template();
                myTemplate.Subject = input;
                myTemplate.Message = templateRichTextBox.Text;
            }
        }
    }
}
