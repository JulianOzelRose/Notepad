
namespace Notepad
{
    partial class GoToForm
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
            this.goToTextBox = new System.Windows.Forms.TextBox();
            this.goToButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Line number:";
            // 
            // goToTextBox
            // 
            this.goToTextBox.Location = new System.Drawing.Point(12, 25);
            this.goToTextBox.Name = "goToTextBox";
            this.goToTextBox.Size = new System.Drawing.Size(224, 20);
            this.goToTextBox.TabIndex = 1;
            this.goToTextBox.TextChanged += new System.EventHandler(this.GoToTextBox_TextChanged);
            this.goToTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GoToTextBox_KeyPress);
            // 
            // goToButton
            // 
            this.goToButton.Location = new System.Drawing.Point(80, 58);
            this.goToButton.Name = "goToButton";
            this.goToButton.Size = new System.Drawing.Size(75, 23);
            this.goToButton.TabIndex = 2;
            this.goToButton.Text = "Go To";
            this.goToButton.UseVisualStyleBackColor = true;
            this.goToButton.Click += new System.EventHandler(this.GoToButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(161, 58);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // GoToForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 88);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.goToButton);
            this.Controls.Add(this.goToTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go To Line";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoToForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox goToTextBox;
        private System.Windows.Forms.Button goToButton;
        private System.Windows.Forms.Button cancelButton;
    }
}