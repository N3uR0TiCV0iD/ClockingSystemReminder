namespace ClockingSystemReminder.Abstractions
{
    partial class LoginDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
            this.OK_Button = new System.Windows.Forms.Button();
            this.usernameBox = new ClockingSystemReminder.Helpers.PlaceholderTextBox();
            this.passwordBox = new ClockingSystemReminder.Helpers.PlaceholderTextBox();
            this.SuspendLayout();
            // 
            // OK_Button
            // 
            this.OK_Button.Enabled = false;
            this.OK_Button.Location = new System.Drawing.Point(12, 81);
            this.OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(141, 27);
            this.OK_Button.TabIndex = 2;
            this.OK_Button.Text = "Login";
            this.OK_Button.UseVisualStyleBackColor = true;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.usernameBox.Location = new System.Drawing.Point(12, 12);
            this.usernameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.PlaceholderColor = System.Drawing.Color.Gray;
            this.usernameBox.PlaceholderText = "Username";
            this.usernameBox.Size = new System.Drawing.Size(140, 23);
            this.usernameBox.TabIndex = 0;
            this.usernameBox.TextChanged += new System.EventHandler(this.usernameBox_TextChanged);
            // 
            // passwordBox
            // 
            this.passwordBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.passwordBox.Location = new System.Drawing.Point(12, 46);
            this.passwordBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PlaceholderColor = System.Drawing.Color.Gray;
            this.passwordBox.PlaceholderText = "Password";
            this.passwordBox.Size = new System.Drawing.Size(140, 23);
            this.passwordBox.TabIndex = 1;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // LoginDialog
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 117);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.OK_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "${System} Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OK_Button;
        private ClockingSystemReminder.Helpers.PlaceholderTextBox usernameBox;
        private ClockingSystemReminder.Helpers.PlaceholderTextBox passwordBox;
    }
}