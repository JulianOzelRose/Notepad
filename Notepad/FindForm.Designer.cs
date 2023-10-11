
namespace Notepad
{
    partial class FindForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.findWhatTextBox = new System.Windows.Forms.TextBox();
            this.findNextButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.downRadioButton = new System.Windows.Forms.RadioButton();
            this.upRadioButton = new System.Windows.Forms.RadioButton();
            this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.wrapAroundCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fi&nd what:";
            // 
            // findWhatTextBox
            // 
            this.findWhatTextBox.Location = new System.Drawing.Point(65, 11);
            this.findWhatTextBox.Name = "findWhatTextBox";
            this.findWhatTextBox.Size = new System.Drawing.Size(183, 20);
            this.findWhatTextBox.TabIndex = 1;
            this.findWhatTextBox.TextChanged += new System.EventHandler(this.FindWhatTextBox_TextChanged);
            // 
            // findNextButton
            // 
            this.findNextButton.Enabled = false;
            this.findNextButton.Location = new System.Drawing.Point(258, 9);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(75, 23);
            this.findNextButton.TabIndex = 5;
            this.findNextButton.Text = "&Find Next";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.FindNextButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(258, 38);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.downRadioButton);
            this.groupBox1.Controls.Add(this.upRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(121, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 44);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // downRadioButton
            // 
            this.downRadioButton.AutoSize = true;
            this.downRadioButton.Checked = true;
            this.downRadioButton.Location = new System.Drawing.Point(61, 19);
            this.downRadioButton.Name = "downRadioButton";
            this.downRadioButton.Size = new System.Drawing.Size(53, 17);
            this.downRadioButton.TabIndex = 4;
            this.downRadioButton.TabStop = true;
            this.downRadioButton.Text = "&Down";
            this.downRadioButton.UseVisualStyleBackColor = true;
            // 
            // upRadioButton
            // 
            this.upRadioButton.AutoSize = true;
            this.upRadioButton.Location = new System.Drawing.Point(16, 19);
            this.upRadioButton.Name = "upRadioButton";
            this.upRadioButton.Size = new System.Drawing.Size(39, 17);
            this.upRadioButton.TabIndex = 0;
            this.upRadioButton.Text = "&Up";
            this.upRadioButton.UseVisualStyleBackColor = true;
            // 
            // matchCaseCheckBox
            // 
            this.matchCaseCheckBox.AutoSize = true;
            this.matchCaseCheckBox.Location = new System.Drawing.Point(9, 68);
            this.matchCaseCheckBox.Name = "matchCaseCheckBox";
            this.matchCaseCheckBox.Size = new System.Drawing.Size(82, 17);
            this.matchCaseCheckBox.TabIndex = 2;
            this.matchCaseCheckBox.Text = "Match &case";
            this.matchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // wrapAroundCheckBox
            // 
            this.wrapAroundCheckBox.AutoSize = true;
            this.wrapAroundCheckBox.Location = new System.Drawing.Point(9, 91);
            this.wrapAroundCheckBox.Name = "wrapAroundCheckBox";
            this.wrapAroundCheckBox.Size = new System.Drawing.Size(88, 17);
            this.wrapAroundCheckBox.TabIndex = 3;
            this.wrapAroundCheckBox.Text = "W&rap around";
            this.wrapAroundCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 120);
            this.Controls.Add(this.wrapAroundCheckBox);
            this.Controls.Add(this.matchCaseCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.findWhatTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button findNextButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton downRadioButton;
        private System.Windows.Forms.RadioButton upRadioButton;
        private System.Windows.Forms.CheckBox matchCaseCheckBox;
        private System.Windows.Forms.CheckBox wrapAroundCheckBox;
        public System.Windows.Forms.TextBox findWhatTextBox;
    }
}