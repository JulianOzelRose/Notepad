using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class GoToForm : Form
    {
        private int totalLines = 1;
        public int LineNumber { get; private set; }
        public int TotalLines
        {
            get { return Math.Max(1, totalLines); }
            set { totalLines = value; }
        }

        public GoToForm(string lastGoToLine)
        {
            InitializeComponent();
            this.KeyPreview = true;

            // Reload last valid entry
            goToTextBox.Text = lastGoToLine;
        }

        public void SendDataToParent()
        {
            ((Notepad)this.Owner).ReceiveDataFromGoToForm(this.goToTextBox.Text);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // File dropdown menu shortcut key
            if ((keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.KeyCode) == Keys.L)
            {
                goToTextBox.Focus();
                goToTextBox.SelectAll();
                return true;
            }

            // Allow base class to process the key event
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GoToLine()
        {
            if (int.TryParse(goToTextBox.Text, out int lineNumber) && lineNumber >= 1 && lineNumber <= TotalLines)
            {
                // Set property if valid number entered
                LineNumber = lineNumber;

                this.Close();
            }
            else
            {
                MessageBox.Show("The line number is beyond the total number of lines", "Notepad - Goto line", MessageBoxButtons.OK);
            }

            SendDataToParent();
        }

        private void GoToButton_Click(object sender, EventArgs e)
        {
            GoToLine();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GoToForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                GoToLine();
            }
        }

        private void DisplayCharErrorToolTip()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.IsBalloon = true;
            toolTip.ToolTipTitle = "Unacceptable Character";
            toolTip.Show("You can only type a number here.", goToTextBox, 20, -80, 3000);
        }

        private void GoToTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Prevent non-numeric entries
                DisplayCharErrorToolTip();
                e.Handled = true;
            }
        }

        private void GoToTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(goToTextBox.Text) && !int.TryParse(goToTextBox.Text, out _))
            {
                // Prevent non-numeric entries with Paste
                DisplayCharErrorToolTip();
                goToTextBox.Text = "";
            }
        }
    }
}
