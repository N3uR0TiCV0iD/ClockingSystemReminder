namespace ClockingSystemReminder.TicketingSystems.Jira
{
    partial class JiraSettingsDialog
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
            this.urlLabel = new System.Windows.Forms.Label();
            this.jiraURLBox = new System.Windows.Forms.TextBox();
            this.jiraEmailLabel = new System.Windows.Forms.Label();
            this.jiraEmailBox = new System.Windows.Forms.TextBox();
            this.jiraTokenLabel = new System.Windows.Forms.Label();
            this.tempoTokenLabel = new System.Windows.Forms.Label();
            this.jiraTokenBox = new System.Windows.Forms.TextBox();
            this.tempoTokenBox = new System.Windows.Forms.TextBox();
            this.jiraTokenSetupLabel = new System.Windows.Forms.LinkLabel();
            this.tempoTokenSetupLabel = new System.Windows.Forms.LinkLabel();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.urlLabel.Location = new System.Drawing.Point(5, 10);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(33, 15);
            this.urlLabel.TabIndex = 0;
            this.urlLabel.Text = "URL:";
            // 
            // jiraURLBox
            // 
            this.jiraURLBox.Location = new System.Drawing.Point(40, 7);
            this.jiraURLBox.Name = "jiraURLBox";
            this.jiraURLBox.Size = new System.Drawing.Size(325, 23);
            this.jiraURLBox.TabIndex = 1;
            this.jiraURLBox.TextChanged += new System.EventHandler(this.jiraURLBox_TextChanged);
            // 
            // jiraEmailLabel
            // 
            this.jiraEmailLabel.AutoSize = true;
            this.jiraEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.jiraEmailLabel.Location = new System.Drawing.Point(5, 40);
            this.jiraEmailLabel.Name = "jiraEmailLabel";
            this.jiraEmailLabel.Size = new System.Drawing.Size(39, 15);
            this.jiraEmailLabel.TabIndex = 2;
            this.jiraEmailLabel.Text = "Email:";
            // 
            // jiraEmailBox
            // 
            this.jiraEmailBox.Location = new System.Drawing.Point(45, 37);
            this.jiraEmailBox.Name = "jiraEmailBox";
            this.jiraEmailBox.Size = new System.Drawing.Size(320, 23);
            this.jiraEmailBox.TabIndex = 3;
            this.jiraEmailBox.TextChanged += new System.EventHandler(this.jiraEmailBox_TextChanged);
            // 
            // jiraTokenLabel
            // 
            this.jiraTokenLabel.AutoSize = true;
            this.jiraTokenLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.jiraTokenLabel.Location = new System.Drawing.Point(5, 70);
            this.jiraTokenLabel.Name = "jiraTokenLabel";
            this.jiraTokenLabel.Size = new System.Drawing.Size(66, 15);
            this.jiraTokenLabel.TabIndex = 4;
            this.jiraTokenLabel.Text = "Jira Token:";
            // 
            // tempoTokenLabel
            // 
            this.tempoTokenLabel.AutoSize = true;
            this.tempoTokenLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tempoTokenLabel.Location = new System.Drawing.Point(5, 100);
            this.tempoTokenLabel.Name = "tempoTokenLabel";
            this.tempoTokenLabel.Size = new System.Drawing.Size(85, 15);
            this.tempoTokenLabel.TabIndex = 7;
            this.tempoTokenLabel.Text = "Tempo Token:";
            // 
            // jiraTokenBox
            // 
            this.jiraTokenBox.Location = new System.Drawing.Point(70, 67);
            this.jiraTokenBox.MaxLength = 192;
            this.jiraTokenBox.Name = "jiraTokenBox";
            this.jiraTokenBox.Size = new System.Drawing.Size(295, 23);
            this.jiraTokenBox.TabIndex = 5;
            this.jiraTokenBox.TextChanged += new System.EventHandler(this.InputBox_TextChanged);
            // 
            // tempoTokenBox
            // 
            this.tempoTokenBox.Location = new System.Drawing.Point(90, 97);
            this.tempoTokenBox.MaxLength = 30;
            this.tempoTokenBox.Name = "tempoTokenBox";
            this.tempoTokenBox.Size = new System.Drawing.Size(275, 23);
            this.tempoTokenBox.TabIndex = 8;
            this.tempoTokenBox.TextChanged += new System.EventHandler(this.InputBox_TextChanged);
            // 
            // jiraTokenSetupLabel
            // 
            this.jiraTokenSetupLabel.AutoSize = true;
            this.jiraTokenSetupLabel.Location = new System.Drawing.Point(370, 70);
            this.jiraTokenSetupLabel.Name = "jiraTokenSetupLabel";
            this.jiraTokenSetupLabel.Size = new System.Drawing.Size(37, 15);
            this.jiraTokenSetupLabel.TabIndex = 6;
            this.jiraTokenSetupLabel.TabStop = true;
            this.jiraTokenSetupLabel.Text = "Setup";
            this.jiraTokenSetupLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.jiraTokenSetupLabel_LinkClicked);
            // 
            // tempoTokenSetupLabel
            // 
            this.tempoTokenSetupLabel.AutoSize = true;
            this.tempoTokenSetupLabel.Enabled = false;
            this.tempoTokenSetupLabel.Location = new System.Drawing.Point(370, 100);
            this.tempoTokenSetupLabel.Name = "tempoTokenSetupLabel";
            this.tempoTokenSetupLabel.Size = new System.Drawing.Size(37, 15);
            this.tempoTokenSetupLabel.TabIndex = 9;
            this.tempoTokenSetupLabel.TabStop = true;
            this.tempoTokenSetupLabel.Text = "Setup";
            this.tempoTokenSetupLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tempoTokenSetupLabel_LinkClicked);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(180, 125);
            this.Cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(90, 25);
            this.Cancel_Button.TabIndex = 10;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK_Button.Enabled = false;
            this.OK_Button.Location = new System.Drawing.Point(275, 125);
            this.OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(90, 25);
            this.OK_Button.TabIndex = 11;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = true;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // JiraSettingsDialog
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(414, 156);
            this.Controls.Add(this.tempoTokenSetupLabel);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.jiraTokenSetupLabel);
            this.Controls.Add(this.tempoTokenBox);
            this.Controls.Add(this.jiraTokenBox);
            this.Controls.Add(this.tempoTokenLabel);
            this.Controls.Add(this.jiraTokenLabel);
            this.Controls.Add(this.jiraEmailBox);
            this.Controls.Add(this.jiraEmailLabel);
            this.Controls.Add(this.jiraURLBox);
            this.Controls.Add(this.urlLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "JiraSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Jira Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.TextBox jiraURLBox;
        private System.Windows.Forms.Label jiraEmailLabel;
        private System.Windows.Forms.TextBox jiraEmailBox;
        private System.Windows.Forms.Label jiraTokenLabel;
        private System.Windows.Forms.Label tempoTokenLabel;
        private System.Windows.Forms.TextBox jiraTokenBox;
        private System.Windows.Forms.TextBox tempoTokenBox;
        private System.Windows.Forms.LinkLabel jiraTokenSetupLabel;
        private System.Windows.Forms.LinkLabel tempoTokenSetupLabel;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button OK_Button;
    }
}