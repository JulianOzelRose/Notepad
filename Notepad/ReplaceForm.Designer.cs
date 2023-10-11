
namespace Notepad
{
    partial class ReplaceForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.replaceWithTextBox = new System.Windows.Forms.TextBox();
            this.findWhatTextBoxReplace = new System.Windows.Forms.TextBox();
            this.findNextButton = new System.Windows.Forms.Button();
            this.replaceButton = new System.Windows.Forms.Button();
            this.replaceAllButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.wrapAroundCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fi&nd what:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Re&place with:";
            // 
            // replaceWithTextBox
            // 
            this.replaceWithTextBox.Location = new System.Drawing.Point(90, 44);
            this.replaceWithTextBox.Name = "replaceWithTextBox";
            this.replaceWithTextBox.Size = new System.Drawing.Size(194, 20);
            this.replaceWithTextBox.TabIndex = 1;
            // 
            // findWhatTextBoxReplace
            // 
            this.findWhatTextBoxReplace.Location = new System.Drawing.Point(91, 13);
            this.findWhatTextBoxReplace.Name = "findWhatTextBoxReplace";
            this.findWhatTextBoxReplace.Size = new System.Drawing.Size(194, 20);
            this.findWhatTextBoxReplace.TabIndex = 0;
            this.findWhatTextBoxReplace.TextChanged += new System.EventHandler(this.FindWhatTextBox_TextChanged);
            // 
            // findNextButton
            // 
            this.findNextButton.Enabled = false;
            this.findNextButton.Location = new System.Drawing.Point(291, 13);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(75, 23);
            this.findNextButton.TabIndex = 5;
            this.findNextButton.Text = "&Find Next";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.FindNextButton_Click);
            // 
            // replaceButton
            // 
            this.replaceButton.Enabled = false;
            this.replaceButton.Location = new System.Drawing.Point(291, 42);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(75, 23);
            this.replaceButton.TabIndex = 6;
            this.replaceButton.Text = "&Replace";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.Enabled = false;
            this.replaceAllButton.Location = new System.Drawing.Point(291, 71);
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(75, 23);
            this.replaceAllButton.TabIndex = 7;
            this.replaceAllButton.Text = "Replace &All";
            this.replaceAllButton.UseVisualStyleBackColor = true;
            this.replaceAllButton.Click += new System.EventHandler(this.ReplaceAllButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(291, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // matchCaseCheckBox
            // 
            this.matchCaseCheckBox.AutoSize = true;
            this.matchCaseCheckBox.Location = new System.Drawing.Point(15, 105);
            this.matchCaseCheckBox.Name = "matchCaseCheckBox";
            this.matchCaseCheckBox.Size = new System.Drawing.Size(82, 17);
            this.matchCaseCheckBox.TabIndex = 3;
            this.matchCaseCheckBox.Text = "Match &case";
            this.matchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // wrapAroundCheckBox
            // 
            this.wrapAroundCheckBox.AutoSize = true;
            this.wrapAroundCheckBox.Location = new System.Drawing.Point(15, 128);
            this.wrapAroundCheckBox.Name = "wrapAroundCheckBox";
            this.wrapAroundCheckBox.Size = new System.Drawing.Size(88, 17);
            this.wrapAroundCheckBox.TabIndex = 4;
            this.wrapAroundCheckBox.Text = "Wrap around";
            this.wrapAroundCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 152);
            this.Controls.Add(this.wrapAroundCheckBox);
            this.Controls.Add(this.matchCaseCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.replaceAllButton);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.findWhatTextBoxReplace);
            this.Controls.Add(this.replaceWithTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replace";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReplaceForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox replaceWithTextBox;
        private System.Windows.Forms.TextBox findWhatTextBoxReplace;
        private System.Windows.Forms.Button findNextButton;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button replaceAllButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox matchCaseCheckBox;
        private System.Windows.Forms.CheckBox wrapAroundCheckBox;
    }
}