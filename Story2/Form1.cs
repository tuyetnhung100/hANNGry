/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: 1. Using a template to from a notification message
 *                     2. Sending the notification to subscribers
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TemplateLibrary;

namespace Story2
{
    public partial class Story2 : Form
    {
        private List<Template> templates;
        private List<MessageBlock> messageBlocks;
        private List<MessageBlock> tagBlocks;

        public Story2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load template list from the DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Story2_Load(object sender, EventArgs e)
        {
            templateListComboBox.Items.Clear();
            templateListComboBox.Items.Add("None");
            templateListComboBox.SelectedIndex = 0;
            templates = new List<Template>();
            if (TemplateDB.Load(ref templates))
            {
                templateListComboBox.Items.AddRange(templates.ToArray());
            }
            else
            {
                MessageBox.Show("Loading template list failed!");
            }
        }

        /// <summary>
        /// When changed, applies selected template into both messageRichTextBox and previewRichTextBox.
        /// If selected "None", then clears texts of messageRichTextBox and previewRichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateListComboBox.SelectedIndex == 0)
            {
                messageRichTextBox.Enabled = true;
                InitializeMessageBlocks("");
                InitializeTags();
                messageRichTextBox.Text = string.Empty;
                previewRichTextBox.Text = string.Empty;
            }
            else
            {
                messageRichTextBox.Enabled = false;
                InitializeMessageBlocks(templates[templateListComboBox.SelectedIndex - 1].Message);
                InitializeTags();
                ColorifyText(messageRichTextBox, false);
                ColorifyText(previewRichTextBox, true);
            }
        }

        /// <summary>
        /// When clicked, sends the notification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notification sent successfully!");
        }

        /// <summary>
        /// Clear and reset inputs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            foreach (MessageBlock tagBlock in tagBlocks)
            {
                tagBlock.Input = string.Empty;
            }
            for (int i = 0; i < tagsDataGridView.Rows.Count; i++)
            {
                tagsDataGridView.Rows[i].Cells[1].Value = "";
            }
            ColorifyText(previewRichTextBox, true);
        }

        /// <summary>
        /// Initialize messageBlocks that represent blocks of template content.
        /// </summary>
        /// <param name="message"></param>
        private void InitializeMessageBlocks(string message)
        {
            messageBlocks = new List<MessageBlock>();
            tagBlocks = new List<MessageBlock>();
            // tested by https://regexr.com/4mokk
            string[] blocks = Regex.Split(message, @"(\{\$.*?\})");
            foreach (string block in blocks)
            {
                // tested by https://regexr.com/4mokh
                bool isTag = Regex.IsMatch(block, @"^\{\$.*?\}$");
                if (isTag)
                {
                    MessageBlock messageBlock = new MessageBlock
                    {
                        IsTag = true,
                        Message = block,
                        Input = string.Empty
                    };
                    messageBlocks.Add(messageBlock);
                    tagBlocks.Add(messageBlock);
                }
                else
                {
                    messageBlocks.Add(new MessageBlock
                    {
                        IsTag = false,
                        Message = block,
                        Input = null
                    });
                }
            }
        }

        /// <summary>
        /// Initialize tags from messageBlocks and bind into tagsDataGridView.
        /// </summary>
        private void InitializeTags()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Tag Name");
            table.Columns.Add("Input");
            foreach (MessageBlock tagBlock in tagBlocks)
            {
                table.Rows.Add(tagBlock.Message, "");
            }
            tagsDataGridView.DataSource = table;

            // prevent users to sort data
            foreach (DataGridViewColumn dataGridViewColumn in tagsDataGridView.Columns)
            {
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// Colorify RichTextBox's text.
        /// </summary>
        /// <param name="richTextBox">target RichTextBox</param>
        /// <param name="isPreview">whether the RichTextBox is preview box</param>
        private void ColorifyText(RichTextBox richTextBox, bool isPreview)
        {
            richTextBox.Clear();
            foreach (MessageBlock messageBlock in messageBlocks)
            {
                if (!messageBlock.IsTag)
                {
                    richTextBox.SelectionColor = Color.Black;
                    richTextBox.AppendText(messageBlock.Message);
                }
                else
                {
                    if (isPreview)
                    {
                        bool hasInput = !string.IsNullOrWhiteSpace(messageBlock.Input);
                        richTextBox.SelectionColor = hasInput ? Color.Green : Color.Red;
                        richTextBox.AppendText(hasInput ? messageBlock.Input : messageBlock.Message);
                    }
                    else
                    {
                        richTextBox.SelectionColor = Color.Red;
                        richTextBox.AppendText(messageBlock.Message);
                    }
                }
            }
        }

        /// <summary>
        /// When cell is focused, if the cell is editable (not a label), switch it to the edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool isEditable = e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < tagsDataGridView.Rows.Count;
            if (isEditable)
            {
                tagsDataGridView.ReadOnly = false;
                tagsDataGridView.BeginEdit(true);
            }
            else
            {
                tagsDataGridView.ReadOnly = true;
            }
        }

        /// <summary>
        /// When switch to the edit mode, register KeyUp event in order to update preview simultaneously.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            dataGridViewTextBoxEditingControl.KeyUp += DataGridViewTextBoxEditingControl_KeyUp;
        }

        /// <summary>
        /// Update messageBlock and reflash preview message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewTextBoxEditingControl_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = (DataGridViewTextBoxEditingControl)sender;
            string input = dataGridViewTextBoxEditingControl.Text;
            int index = tagsDataGridView.CurrentCell.RowIndex;
            tagBlocks[index].Input = input;
            ColorifyText(previewRichTextBox, true);
        }
    }
}
