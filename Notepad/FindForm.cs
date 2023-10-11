using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FindForm : Form
    {
        public RichTextBox RichTextBoxControl { get; set; }
        public int FoundTextPosition { get; private set; }

        public FindForm(string lastSearchVal, bool lastRadioUpVal, bool lastRadioDownVal, bool lastMatchCaseVal, bool lastWrapAroundVal)
        {
            InitializeComponent();

            this.KeyPreview = true;

            // Reload previous search params
            findWhatTextBox.Text = lastSearchVal;
            downRadioButton.Checked = lastRadioDownVal;
            upRadioButton.Checked = lastRadioUpVal;
            matchCaseCheckBox.Checked = lastMatchCaseVal;
            wrapAroundCheckBox.Checked = lastWrapAroundVal;
        }

        public void SendDataToParent()
        {
            ((Notepad)this.Owner).ReceiveDataFromFindForm(this.findWhatTextBox.Text, this.upRadioButton.Checked, this.downRadioButton.Checked,
                this.matchCaseCheckBox.Checked, this.wrapAroundCheckBox.Checked);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Find textbox shortcut keys
            if (keyData == (Keys.Alt | Keys.N))
            {
                findWhatTextBox.Focus();
                findWhatTextBox.SelectAll();
            }

            // Allow base class to process the key event
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FindNext()
        {
            string textToFind = findWhatTextBox.Text;

            RichTextBoxFinds searchOptions = RichTextBoxFinds.None;

            if (matchCaseCheckBox.Checked)
            {
                searchOptions |= RichTextBoxFinds.MatchCase;
            }

            int startPosition = RichTextBoxControl.SelectionStart;
            int index;

            if (downRadioButton.Checked)
            {
                index = RichTextBoxControl.Find(textToFind, startPosition + 1, searchOptions);
                if (index == -1 && wrapAroundCheckBox.Checked)
                {
                    index = RichTextBoxControl.Find(textToFind, 0, searchOptions);
                }
            }
            else
            {
                index = RichTextBoxControl.Find(textToFind, 0, searchOptions);
                if (index == -1 && wrapAroundCheckBox.Checked)
                {
                    index = RichTextBoxControl.Find(textToFind, RichTextBoxControl.Text.Length, searchOptions);
                }
            }

            if (index >= 0)
            {
                // Clear previous highlights
                RichTextBoxControl.Select(0, RichTextBoxControl.Text.Length);
                RichTextBoxControl.SelectionBackColor = Color.White;

                // Select result
                RichTextBoxControl.Select(index, textToFind.Length);
                RichTextBoxControl.SelectionBackColor = Color.White;

                // Inform the parent form to update its UI
                ((Notepad)this.Owner).UpdateUIAfterHighlight();

                // Select result
                RichTextBoxControl.SelectionStart = index + textToFind.Length;
                FoundTextPosition = index;
            }
            else
            {
                MessageBox.Show($"Cannot find \"{textToFind}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SendDataToParent();
        }

        private void FindWhatTextBox_TextChanged(object sender, EventArgs e)
        {
            // Update Find button state
            findNextButton.Enabled = !string.IsNullOrEmpty(findWhatTextBox.Text);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FindNextButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findWhatTextBox.Text))
            {
                FindNext();
            }
        }

        private void FindForm_KeyDown(object sender, KeyEventArgs e)
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
    }
}
