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
            txtLineNumber.Text = lastGoToLine;
        }

        public void SendDataToParent()
        {
            ((Notepad)this.Owner).ReceiveDataFromGoToForm(this.txtLineNumber.Text);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // File dropdown menu shortcut key
            if ((keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.KeyCode) == Keys.L)
            {
                txtLineNumber.Focus();
                txtLineNumber.SelectAll();
                return true;
            }

            // Allow base class to process the key event
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GoToLine()
        {
            if (int.TryParse(txtLineNumber.Text, out int lineNumber) && lineNumber >= 1 && lineNumber <= TotalLines)
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

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            GoToLine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
            toolTip.Show("You can only type a number here.", txtLineNumber, 20, -80, 3000);
        }

        private void txtLineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Prevent non-numeric entries
                DisplayCharErrorToolTip();
                e.Handled = true;
            }
        }

        private void txtLineNumber_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLineNumber.Text) && !int.TryParse(txtLineNumber.Text, out _))
            {
                // Prevent non-numeric entries with Paste
                DisplayCharErrorToolTip();
                txtLineNumber.Text = "";
            }
        }
    }
}
