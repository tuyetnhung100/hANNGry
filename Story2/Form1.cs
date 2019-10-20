/*
 * Programmer(s):      Gong-Hao
 * Date:               10/13/2019
 * What the code does: 1. Using a template to from a notification message
 *                     2. Sending the notification to subscribers
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TemplateLibrary;

namespace Story2
{
    public partial class Story2 : Form
    {
        private List<Template> templates = new List<Template>();
        private List<MessageBlock> messageBlocks = new List<MessageBlock>();
        private List<MessageBlock> tagBlocks = new List<MessageBlock>();

        public Story2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize controls and set default settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Story2_Load(object sender, EventArgs e)
        {
            InitializeTemplateComboBox();
            InitializeTagsDataGridView();
        }

        /// <summary>
        /// Initialize and load items of templateComboBox.
        /// </summary>
        private void InitializeTemplateComboBox()
        {
            // set the default option to be "None"
            templateComboBox.Items.Clear();
            templateComboBox.Items.Add("None");
            templateComboBox.SelectedIndex = 0;

            // load template list
            if (TemplateDB.Load(ref templates))
            {
                templateComboBox.Items.AddRange(templates.ToArray());
            }
            else
            {
                MessageBox.Show("Loading template list failed!");
            }
        }

        /// <summary>
        /// Initialize settings of columns of tagsDataGridView.
        /// </summary>
        private void InitializeTagsDataGridView()
        {
            DataGridViewTextBoxColumn tagNameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tag Name",
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            tagsDataGridView.Columns.Add(tagNameColumn);
            DataGridViewTextBoxColumn inputColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Input",
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            tagsDataGridView.Columns.Add(inputColumn);
        }

        /// <summary>
        /// When changed, applies selected template into both messageRichTextBox and previewRichTextBox.
        /// If selected "None", then clears texts of messageRichTextBox and previewRichTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateComboBox.SelectedIndex == 0)
            {
                // not using a templat - enable messageRichTextBox and rely on only text
                messageRichTextBox.Enabled = true;
                ReloadBlocks(string.Empty);
                ReloadTags();
                messageRichTextBox.Text = string.Empty;
                previewRichTextBox.Text = string.Empty;
            }
            else
            {
                // using a templat - disable messageRichTextBox and rely on tags
                messageRichTextBox.Enabled = false;
                ReloadBlocks(templates[templateComboBox.SelectedIndex - 1].Message);
                ReloadTags();
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
            // todo: send notification by emails
            MessageBox.Show("Notification sent successfully!");
        }

        /// <summary>
        /// Clear and reset inputs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tagsDataGridView.Rows.Count; i++)
            {
                // reset tagBlocks
                tagBlocks[i].Input = string.Empty;
                // reset tagsDataGridView
                tagsDataGridView.Rows[i].Cells[1].Value = string.Empty;
            }

            // reset previewRichTextBox
            ColorifyText(previewRichTextBox, true);
        }

        /// <summary>
        /// Analyze and set messageBlocks that represent blocks of template content.
        /// </summary>
        /// <param name="message">message of template</param>
        private void ReloadBlocks(string message)
        {
            messageBlocks.Clear();
            tagBlocks.Clear();

            // Regex tested by https://regexr.com/4mokk
            // it splits {$foo} into blocks
            string[] blocks = Regex.Split(message, @"(\{\$.*?\})");
            foreach (string block in blocks)
            {
                // Regex tested by https://regexr.com/4mokh
                // it matchs {$foo}
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
                    MessageBlock messageBlock = new MessageBlock
                    {
                        IsTag = false,
                        Message = block,
                        Input = null
                    };
                    messageBlocks.Add(messageBlock);
                }
            }
        }

        /// <summary>
        /// Load tagBlocks into tagsDataGridView.
        /// </summary>
        private void ReloadTags()
        {
            // reset rows
            tagsDataGridView.Rows.Clear();

            // add tagBlock into rows of tagsDataGridView
            foreach (MessageBlock tagBlock in tagBlocks)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewCell tagCell = new DataGridViewTextBoxCell
                {
                    Value = tagBlock.Message
                };
                row.Cells.Add(tagCell);
                DataGridViewCell inputCell = new DataGridViewTextBoxCell
                {
                    Value = string.Empty
                };
                row.Cells.Add(inputCell);
                tagsDataGridView.Rows.Add(row);
            }
        }

        /// <summary>
        /// Colorify RichTextBox's text.
        /// </summary>
        /// <param name="richTextBox">target RichTextBox</param>
        /// <param name="isPreview">whether the RichTextBox is preview box</param>
        private void ColorifyText(RichTextBox richTextBox, bool isPreview)
        {
            // reset text value
            richTextBox.Clear();

            // append all blocks to be text value of RichTextBox
            foreach (MessageBlock messageBlock in messageBlocks)
            {
                if (!messageBlock.IsTag)
                {
                    // set normal text color to be black
                    richTextBox.SelectionColor = Color.Black;
                    richTextBox.AppendText(messageBlock.Message);
                }
                else
                {
                    if (isPreview)
                    {
                        // set tag text of preview box to be red
                        // if there has user input, set text to be green
                        bool hasInput = !string.IsNullOrWhiteSpace(messageBlock.Input);
                        richTextBox.SelectionColor = hasInput ? Color.Green : Color.Red;
                        richTextBox.AppendText(hasInput ? messageBlock.Input : messageBlock.Message);
                    }
                    else
                    {
                        // set tag text of message box to be red
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
                // auto switch to edit mode if it is editable
                tagsDataGridView.ReadOnly = false;
                tagsDataGridView.BeginEdit(true);
            }
            else
            {
                // a label is ReadOnly
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
            DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = e.Control as DataGridViewTextBoxEditingControl;
            dataGridViewTextBoxEditingControl.KeyUp += DataGridViewTextBoxEditingControl_KeyUp;
            // unsubscribe event to prevent triggering KeyUp event multiple times
            tagsDataGridView.EditingControlShowing -= TagsDataGridView_EditingControlShowing;
        }

        /// <summary>
        /// Update messageBlock and reflash preview message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewTextBoxEditingControl_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = sender as DataGridViewTextBoxEditingControl;
            string input = dataGridViewTextBoxEditingControl.Text;
            int index = tagsDataGridView.CurrentCell.RowIndex;
            tagBlocks[index].Input = input;
            ColorifyText(previewRichTextBox, true);
        }
    }
}
