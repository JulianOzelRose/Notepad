using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    public partial class ReplaceForm : Form
    {
        public RichTextBox RichTextBoxControl { get; set; }
        public RichTextBox richTextBox;
        private string replaceWithText;
        private string textToFind;

        public ReplaceForm(string lastSearch, string lastReplace, bool matchCase, bool wrapAround)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.ActiveControl = findWhatTextBoxReplace;

            // Reload previous search params
            findWhatTextBoxReplace.Text = lastSearch;
            replaceWithTextBox.Text = lastReplace;
            matchCaseCheckBox.Checked = matchCase;
            wrapAroundCheckBox.Checked = wrapAround;
        }

        public void SendDataToParent()
        {
            ((Notepad)this.Owner).ReceiveDataFromReplaceForm(this.findWhatTextBoxReplace.Text, this.replaceWithTextBox.Text,
                this.matchCaseCheckBox.Checked, this.wrapAroundCheckBox.Checked);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Find textbox shortcut keys
            if ((keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.KeyCode) == Keys.N)
            {
                findWhatTextBoxReplace.Focus();
                findWhatTextBoxReplace.SelectAll();
                return true;
            }

            // Replace with textbox shortcut keys
            if ((keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.KeyCode) == Keys.P)
            {
                replaceWithTextBox.Focus();
                replaceWithTextBox.SelectAll();
                return true;
            }

            // Allow base class to process the key event
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SetParentRichTextBox(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                this.richTextBox = richTextBox;
            }
        }

        private void FindNext()
        {
            textToFind = findWhatTextBoxReplace.Text;

            if (!string.IsNullOrEmpty(textToFind) && richTextBox != null)
            {
                int startPosition = richTextBox.SelectionStart + richTextBox.SelectionLength;
                int textLength = richTextBox.Text.Length;

                RichTextBoxFinds searchOptions = RichTextBoxFinds.None;

                if (matchCaseCheckBox.Checked)
                {
                    searchOptions |= RichTextBoxFinds.MatchCase;
                }

                if (!wrapAroundCheckBox.Checked)
                {
                    // If wrap around is disabled, only search within the current selection
                    startPosition = Math.Max(0, Math.Min(startPosition, textLength));
                    int index = richTextBox.Find(textToFind, startPosition, textLength, searchOptions);

                    if (index >= 0)
                    {
                        // Clear previous highlights
                        richTextBox.Select(0, richTextBox.Text.Length);
                        richTextBox.SelectionBackColor = Color.White;

                        // Select result
                        richTextBox.Select(index, textToFind.Length);

                        // Inform the parent form to update its UI
                        ((Notepad)this.Owner).UpdateUIAfterHighlight();
                    }
                    else
                    {
                        MessageBox.Show($"Cannot find \"{textToFind}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // If wrap around is enabled, search from the start when necessary
                    int index = richTextBox.Find(textToFind, startPosition, textLength, searchOptions);

                    if (index >= 0)
                    {
                        // Clear previous highlights
                        richTextBox.Select(0, richTextBox.Text.Length);
                        richTextBox.SelectionBackColor = Color.White;

                        // Select result
                        richTextBox.Select(index, textToFind.Length);
                        richTextBox.SelectionBackColor = Color.White;

                        // Inform the parent form to update its UI
                        ((Notepad)this.Owner).UpdateUIAfterHighlight();
                    }
                    else
                    {
                        // If no match is found, search from the beginning
                        index = richTextBox.Find(textToFind, 0, textLength, searchOptions);

                        if (index >= 0)
                        {
                            // Clear previous highlights
                            richTextBox.Select(0, richTextBox.Text.Length);
                            richTextBox.SelectionBackColor = Color.White;

                            // Select and highlight the found text
                            richTextBox.Select(index, textToFind.Length);
                            richTextBox.SelectionBackColor = Color.White;

                            richTextBox.SelectionStart = index + textToFind.Length;

                            // Inform the parent form to update its UI
                            ((Notepad)this.Owner).UpdateUIAfterHighlight();
                        }
                        else
                        {
                            MessageBox.Show($"Cannot find \"{textToFind}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("textToFind or richTextBox is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SendDataToParent();
        }

        private void Replace()
        {
            replaceWithText = replaceWithTextBox.Text;
            textToFind = findWhatTextBoxReplace.Text;

            if (!string.IsNullOrEmpty(textToFind) && richTextBox != null)
            {
                int textLength = richTextBox.Text.Length;

                RichTextBoxFinds searchOptions = RichTextBoxFinds.None;

                if (matchCaseCheckBox.Checked)
                {
                    searchOptions |= RichTextBoxFinds.MatchCase;
                }

                int index = richTextBox.Find(textToFind, 0, textLength, searchOptions);

                if (index >= 0)
                {
                    // Clear previous highlights
                    richTextBox.Select(0, richTextBox.Text.Length);
                    richTextBox.SelectionBackColor = Color.White;

                    // Replace the found text
                    richTextBox.Select(index, textToFind.Length);
                    richTextBox.SelectedText = replaceWithText;

                    // Move the starting position past the replaced text
                    int startPosition = index + replaceWithText.Length;

                    // Highlight the next occurrence after replacement
                    index = richTextBox.Find(textToFind, startPosition, textLength, searchOptions);

                    if (index >= 0)
                    {
                        richTextBox.Select(index, textToFind.Length);
                        richTextBox.SelectionBackColor = Color.LightBlue;
                    }
                }
                else
                {
                    MessageBox.Show($"Cannot find \"{textToFind}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("textToFind or richTextBox is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SendDataToParent();
        }

        private void ReplaceAll()
        {
            replaceWithText = replaceWithTextBox.Text;
            textToFind = findWhatTextBoxReplace.Text;

            if (!string.IsNullOrEmpty(textToFind) && richTextBox != null)
            {
                int index = 0;
                int count = 0;
                int textLength = richTextBox.Text.Length;

                RichTextBoxFinds searchOptions = RichTextBoxFinds.None;

                if (matchCaseCheckBox.Checked)
                {
                    searchOptions |= RichTextBoxFinds.MatchCase;
                }

                while (index >= 0)
                {
                    if (!wrapAroundCheckBox.Checked)
                    {
                        // If wrap around is disabled, only search within the current selection
                        int startPosition = index + textToFind.Length;

                        if (startPosition < 0)
                        {
                            startPosition = 0;
                        }

                        startPosition = Math.Min(startPosition, textLength);

                        index = richTextBox.Find(textToFind, startPosition, textLength, searchOptions);
                    }
                    else
                    {
                        // If wrap around is enabled, search from the start when necessary
                        index = richTextBox.Find(textToFind, index, textLength, searchOptions);
                    }

                    if (index >= 0)
                    {
                        // Clear previous highlights
                        richTextBox.Select(0, richTextBox.Text.Length);
                        richTextBox.SelectionBackColor = Color.White;

                        // Replace the found text
                        richTextBox.Select(index, textToFind.Length);
                        richTextBox.SelectedText = replaceWithText;
                        richTextBox.BackColor = Color.White;
                        count++;
                        index += replaceWithText.Length;
                    }
                }
            }
            else
            {
                MessageBox.Show("textToFind or richTextBox is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SendDataToParent();
        }

        private void FindNextButton_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            Replace();
        }

        private void ReplaceAllButton_Click(object sender, EventArgs e)
        {
            ReplaceAll();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReplaceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter && findNextButton.Enabled)
            {
                FindNext();
            }
        }

        private void FindWhatTextBox_TextChanged(object sender, EventArgs e)
        {
            // Update button states
            findNextButton.Enabled = !string.IsNullOrEmpty(findWhatTextBoxReplace.Text);
            replaceButton.Enabled = !string.IsNullOrEmpty(findWhatTextBoxReplace.Text);
            replaceAllButton.Enabled = !string.IsNullOrEmpty(findWhatTextBoxReplace.Text);
        }
    }
}
