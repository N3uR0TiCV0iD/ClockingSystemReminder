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
            urlLabel = new System.Windows.Forms.Label();
            jiraURLBox = new System.Windows.Forms.TextBox();
            jiraEmailLabel = new System.Windows.Forms.Label();
            jiraEmailBox = new System.Windows.Forms.TextBox();
            jiraTokenLabel = new System.Windows.Forms.Label();
            tempoTokenLabel = new System.Windows.Forms.Label();
            jiraTokenBox = new System.Windows.Forms.TextBox();
            tempoTokenBox = new System.Windows.Forms.TextBox();
            jiraTokenSetupLabel = new System.Windows.Forms.LinkLabel();
            tempoTokenSetupLabel = new System.Windows.Forms.LinkLabel();
            Cancel_Button = new System.Windows.Forms.Button();
            OK_Button = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            urlLabel.Location = new System.Drawing.Point(5, 10);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new System.Drawing.Size(33, 15);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "URL:";
            // 
            // jiraURLBox
            // 
            jiraURLBox.Location = new System.Drawing.Point(40, 7);
            jiraURLBox.Name = "jiraURLBox";
            jiraURLBox.Size = new System.Drawing.Size(325, 23);
            jiraURLBox.TabIndex = 1;
            jiraURLBox.TextChanged += jiraURLBox_TextChanged;
            // 
            // jiraEmailLabel
            // 
            jiraEmailLabel.AutoSize = true;
            jiraEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            jiraEmailLabel.Location = new System.Drawing.Point(5, 40);
            jiraEmailLabel.Name = "jiraEmailLabel";
            jiraEmailLabel.Size = new System.Drawing.Size(39, 15);
            jiraEmailLabel.TabIndex = 2;
            jiraEmailLabel.Text = "Email:";
            // 
            // jiraEmailBox
            // 
            jiraEmailBox.Location = new System.Drawing.Point(45, 37);
            jiraEmailBox.Name = "jiraEmailBox";
            jiraEmailBox.Size = new System.Drawing.Size(320, 23);
            jiraEmailBox.TabIndex = 3;
            jiraEmailBox.TextChanged += jiraEmailBox_TextChanged;
            // 
            // jiraTokenLabel
            // 
            jiraTokenLabel.AutoSize = true;
            jiraTokenLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            jiraTokenLabel.Location = new System.Drawing.Point(5, 70);
            jiraTokenLabel.Name = "jiraTokenLabel";
            jiraTokenLabel.Size = new System.Drawing.Size(66, 15);
            jiraTokenLabel.TabIndex = 4;
            jiraTokenLabel.Text = "Jira Token:";
            // 
            // tempoTokenLabel
            // 
            tempoTokenLabel.AutoSize = true;
            tempoTokenLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            tempoTokenLabel.Location = new System.Drawing.Point(5, 100);
            tempoTokenLabel.Name = "tempoTokenLabel";
            tempoTokenLabel.Size = new System.Drawing.Size(85, 15);
            tempoTokenLabel.TabIndex = 7;
            tempoTokenLabel.Text = "Tempo Token:";
            // 
            // jiraTokenBox
            // 
            jiraTokenBox.Location = new System.Drawing.Point(70, 67);
            jiraTokenBox.MaxLength = 192;
            jiraTokenBox.Name = "jiraTokenBox";
            jiraTokenBox.Size = new System.Drawing.Size(295, 23);
            jiraTokenBox.TabIndex = 5;
            jiraTokenBox.TextChanged += InputBox_TextChanged;
            // 
            // tempoTokenBox
            // 
            tempoTokenBox.Location = new System.Drawing.Point(90, 97);
            tempoTokenBox.MaxLength = 33;
            tempoTokenBox.Name = "tempoTokenBox";
            tempoTokenBox.Size = new System.Drawing.Size(275, 23);
            tempoTokenBox.TabIndex = 8;
            tempoTokenBox.TextChanged += InputBox_TextChanged;
            // 
            // jiraTokenSetupLabel
            // 
            jiraTokenSetupLabel.AutoSize = true;
            jiraTokenSetupLabel.Location = new System.Drawing.Point(370, 70);
            jiraTokenSetupLabel.Name = "jiraTokenSetupLabel";
            jiraTokenSetupLabel.Size = new System.Drawing.Size(37, 15);
            jiraTokenSetupLabel.TabIndex = 6;
            jiraTokenSetupLabel.TabStop = true;
            jiraTokenSetupLabel.Text = "Setup";
            jiraTokenSetupLabel.LinkClicked += jiraTokenSetupLabel_LinkClicked;
            // 
            // tempoTokenSetupLabel
            // 
            tempoTokenSetupLabel.AutoSize = true;
            tempoTokenSetupLabel.Enabled = false;
            tempoTokenSetupLabel.Location = new System.Drawing.Point(370, 100);
            tempoTokenSetupLabel.Name = "tempoTokenSetupLabel";
            tempoTokenSetupLabel.Size = new System.Drawing.Size(37, 15);
            tempoTokenSetupLabel.TabIndex = 9;
            tempoTokenSetupLabel.TabStop = true;
            tempoTokenSetupLabel.Text = "Setup";
            tempoTokenSetupLabel.LinkClicked += tempoTokenSetupLabel_LinkClicked;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel_Button.Location = new System.Drawing.Point(180, 125);
            Cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new System.Drawing.Size(90, 25);
            Cancel_Button.TabIndex = 10;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = true;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            OK_Button.Enabled = false;
            OK_Button.Location = new System.Drawing.Point(275, 125);
            OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new System.Drawing.Size(90, 25);
            OK_Button.TabIndex = 11;
            OK_Button.Text = "OK";
            OK_Button.UseVisualStyleBackColor = true;
            OK_Button.Click += OK_Button_Click;
            // 
            // JiraSettingsDialog
            // 
            this.AcceptButton = OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = Cancel_Button;
            this.ClientSize = new System.Drawing.Size(414, 156);
            this.Controls.Add(tempoTokenSetupLabel);
            this.Controls.Add(Cancel_Button);
            this.Controls.Add(OK_Button);
            this.Controls.Add(jiraTokenSetupLabel);
            this.Controls.Add(tempoTokenBox);
            this.Controls.Add(jiraTokenBox);
            this.Controls.Add(tempoTokenLabel);
            this.Controls.Add(jiraTokenLabel);
            this.Controls.Add(jiraEmailBox);
            this.Controls.Add(jiraEmailLabel);
            this.Controls.Add(jiraURLBox);
            this.Controls.Add(urlLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "JiraSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Jira Settings";
            ResumeLayout(false);
            PerformLayout();
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