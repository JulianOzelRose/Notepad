
namespace Notepad
{
    partial class AboutForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.lblNotepad = new System.Windows.Forms.Label();
            this.lblAbout1 = new System.Windows.Forms.Label();
            this.lblAbout2 = new System.Windows.Forms.Label();
            this.llbGitHub = new System.Windows.Forms.LinkLabel();
            this.picNotepad = new System.Windows.Forms.PictureBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picNotepad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(355, 321);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblNotepad
            // 
            this.lblNotepad.AutoSize = true;
            this.lblNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotepad.ForeColor = System.Drawing.Color.Blue;
            this.lblNotepad.Location = new System.Drawing.Point(147, 24);
            this.lblNotepad.Name = "lblNotepad";
            this.lblNotepad.Size = new System.Drawing.Size(207, 55);
            this.lblNotepad.TabIndex = 2;
            this.lblNotepad.Text = "Notepad";
            // 
            // lblAbout1
            // 
            this.lblAbout1.AutoSize = true;
            this.lblAbout1.Location = new System.Drawing.Point(52, 148);
            this.lblAbout1.Name = "lblAbout1";
            this.lblAbout1.Size = new System.Drawing.Size(245, 13);
            this.lblAbout1.TabIndex = 3;
            this.lblAbout1.Text = "This Notepad clone was written by Julian O. Rose.";
            // 
            // lblAbout2
            // 
            this.lblAbout2.AutoSize = true;
            this.lblAbout2.Location = new System.Drawing.Point(52, 170);
            this.lblAbout2.Name = "lblAbout2";
            this.lblAbout2.Size = new System.Drawing.Size(197, 13);
            this.lblAbout2.TabIndex = 4;
            this.lblAbout2.Text = "For more projects, check out my GitHub:";
            // 
            // llbGitHub
            // 
            this.llbGitHub.AutoSize = true;
            this.llbGitHub.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbGitHub.Location = new System.Drawing.Point(52, 205);
            this.llbGitHub.Name = "llbGitHub";
            this.llbGitHub.Size = new System.Drawing.Size(211, 16);
            this.llbGitHub.TabIndex = 5;
            this.llbGitHub.TabStop = true;
            this.llbGitHub.Text = "https://github.com/JulianOzelRose";
            this.llbGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbGitHub_LinkClicked);
            // 
            // picNotepad
            // 
            this.picNotepad.Image = global::Notepad.Properties.Resources.Notepad_logo;
            this.picNotepad.Location = new System.Drawing.Point(83, 13);
            this.picNotepad.Name = "picNotepad";
            this.picNotepad.Size = new System.Drawing.Size(56, 69);
            this.picNotepad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNotepad.TabIndex = 6;
            this.picNotepad.TabStop = false;
            // 
            // lblSeparator
            // 
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSeparator.Location = new System.Drawing.Point(10, 105);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(420, 1);
            this.lblSeparator.TabIndex = 7;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 356);
            this.Controls.Add(this.lblSeparator);
            this.Controls.Add(this.picNotepad);
            this.Controls.Add(this.llbGitHub);
            this.Controls.Add(this.lblAbout2);
            this.Controls.Add(this.lblAbout1);
            this.Controls.Add(this.lblNotepad);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Notepad";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picNotepad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblNotepad;
        private System.Windows.Forms.Label lblAbout1;
        private System.Windows.Forms.Label lblAbout2;
        private System.Windows.Forms.LinkLabel llbGitHub;
        private System.Windows.Forms.PictureBox picNotepad;
        private System.Windows.Forms.Label lblSeparator;
    }
}