namespace ClockingSystemReminder
{
    partial class SettingsDialog
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
            clockingSystemLabel = new System.Windows.Forms.Label();
            clockingSystemBox = new System.Windows.Forms.ComboBox();
            collaborationSystemBox = new System.Windows.Forms.ComboBox();
            collaborationSystemConfigButton = new System.Windows.Forms.Button();
            collaborationSystemLabel = new System.Windows.Forms.Label();
            ticketingSystemLabel = new System.Windows.Forms.Label();
            ticketingSystemBox = new System.Windows.Forms.ComboBox();
            ticketingSystemConfigButton = new System.Windows.Forms.Button();
            Cancel_Button = new System.Windows.Forms.Button();
            OK_Button = new System.Windows.Forms.Button();
            clockingSystemConfigButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // clockingSystemLabel
            // 
            clockingSystemLabel.AutoSize = true;
            clockingSystemLabel.Location = new System.Drawing.Point(5, 10);
            clockingSystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            clockingSystemLabel.Name = "clockingSystemLabel";
            clockingSystemLabel.Size = new System.Drawing.Size(98, 15);
            clockingSystemLabel.TabIndex = 0;
            clockingSystemLabel.Text = "Clocking System:";
            // 
            // clockingSystemBox
            // 
            clockingSystemBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            clockingSystemBox.FormattingEnabled = true;
            clockingSystemBox.Items.AddRange(new object[] { "WinTid", "Capitech", "[None]" });
            clockingSystemBox.Location = new System.Drawing.Point(130, 7);
            clockingSystemBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            clockingSystemBox.Name = "clockingSystemBox";
            clockingSystemBox.Size = new System.Drawing.Size(140, 23);
            clockingSystemBox.TabIndex = 1;
            clockingSystemBox.SelectedIndexChanged += clockingSystemBox_SelectedIndexChanged;
            // 
            // collaborationSystemBox
            // 
            collaborationSystemBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            collaborationSystemBox.FormattingEnabled = true;
            collaborationSystemBox.Items.AddRange(new object[] { "MSTeams" });
            collaborationSystemBox.Location = new System.Drawing.Point(130, 37);
            collaborationSystemBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            collaborationSystemBox.Name = "collaborationSystemBox";
            collaborationSystemBox.Size = new System.Drawing.Size(140, 23);
            collaborationSystemBox.TabIndex = 4;
            collaborationSystemBox.SelectedIndexChanged += collaborationSystemBox_SelectedIndexChanged;
            // 
            // collaborationSystemConfigButton
            // 
            collaborationSystemConfigButton.Enabled = false;
            collaborationSystemConfigButton.Location = new System.Drawing.Point(275, 36);
            collaborationSystemConfigButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            collaborationSystemConfigButton.Name = "collaborationSystemConfigButton";
            collaborationSystemConfigButton.Size = new System.Drawing.Size(90, 25);
            collaborationSystemConfigButton.TabIndex = 5;
            collaborationSystemConfigButton.Text = "Configure";
            collaborationSystemConfigButton.UseVisualStyleBackColor = true;
            collaborationSystemConfigButton.Click += collaborationSystemConfigButton_Click;
            // 
            // collaborationSystemLabel
            // 
            collaborationSystemLabel.AutoSize = true;
            collaborationSystemLabel.Location = new System.Drawing.Point(5, 40);
            collaborationSystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            collaborationSystemLabel.Name = "collaborationSystemLabel";
            collaborationSystemLabel.Size = new System.Drawing.Size(123, 15);
            collaborationSystemLabel.TabIndex = 3;
            collaborationSystemLabel.Text = "Collaboration System:";
            // 
            // ticketingSystemLabel
            // 
            ticketingSystemLabel.AutoSize = true;
            ticketingSystemLabel.Location = new System.Drawing.Point(5, 70);
            ticketingSystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ticketingSystemLabel.Name = "ticketingSystemLabel";
            ticketingSystemLabel.Size = new System.Drawing.Size(99, 15);
            ticketingSystemLabel.TabIndex = 6;
            ticketingSystemLabel.Text = "Ticketing System:";
            // 
            // ticketingSystemBox
            // 
            ticketingSystemBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ticketingSystemBox.FormattingEnabled = true;
            ticketingSystemBox.Items.AddRange(new object[] { "Jira" });
            ticketingSystemBox.Location = new System.Drawing.Point(130, 67);
            ticketingSystemBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ticketingSystemBox.Name = "ticketingSystemBox";
            ticketingSystemBox.Size = new System.Drawing.Size(140, 23);
            ticketingSystemBox.TabIndex = 7;
            ticketingSystemBox.SelectedIndexChanged += ticketingSystemBox_SelectedIndexChanged;
            // 
            // ticketingSystemConfigButton
            // 
            ticketingSystemConfigButton.Enabled = false;
            ticketingSystemConfigButton.Location = new System.Drawing.Point(275, 66);
            ticketingSystemConfigButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ticketingSystemConfigButton.Name = "ticketingSystemConfigButton";
            ticketingSystemConfigButton.Size = new System.Drawing.Size(90, 25);
            ticketingSystemConfigButton.TabIndex = 8;
            ticketingSystemConfigButton.Text = "Configure";
            ticketingSystemConfigButton.UseVisualStyleBackColor = true;
            ticketingSystemConfigButton.Click += ticketingSystemConfigButton_Click;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel_Button.Location = new System.Drawing.Point(180, 97);
            Cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new System.Drawing.Size(90, 25);
            Cancel_Button.TabIndex = 9;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = true;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            OK_Button.Enabled = false;
            OK_Button.Location = new System.Drawing.Point(275, 97);
            OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new System.Drawing.Size(90, 25);
            OK_Button.TabIndex = 10;
            OK_Button.Text = "OK";
            OK_Button.UseVisualStyleBackColor = true;
            OK_Button.Click += OK_Button_Click;
            // 
            // clockingSystemConfigButton
            // 
            clockingSystemConfigButton.Enabled = false;
            clockingSystemConfigButton.Location = new System.Drawing.Point(275, 5);
            clockingSystemConfigButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            clockingSystemConfigButton.Name = "clockingSystemConfigButton";
            clockingSystemConfigButton.Size = new System.Drawing.Size(90, 25);
            clockingSystemConfigButton.TabIndex = 2;
            clockingSystemConfigButton.Text = "Configure";
            clockingSystemConfigButton.UseVisualStyleBackColor = true;
            clockingSystemConfigButton.Click += clockingSystemConfigButton_Click;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = Cancel_Button;
            this.ClientSize = new System.Drawing.Size(370, 127);
            this.Controls.Add(clockingSystemConfigButton);
            this.Controls.Add(Cancel_Button);
            this.Controls.Add(OK_Button);
            this.Controls.Add(ticketingSystemBox);
            this.Controls.Add(ticketingSystemConfigButton);
            this.Controls.Add(ticketingSystemLabel);
            this.Controls.Add(collaborationSystemBox);
            this.Controls.Add(collaborationSystemConfigButton);
            this.Controls.Add(collaborationSystemLabel);
            this.Controls.Add(clockingSystemBox);
            this.Controls.Add(clockingSystemLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label clockingSystemLabel;
        private System.Windows.Forms.ComboBox clockingSystemBox;
        private System.Windows.Forms.ComboBox collaborationSystemBox;
        private System.Windows.Forms.Button collaborationSystemConfigButton;
        private System.Windows.Forms.Label collaborationSystemLabel;
        private System.Windows.Forms.Label ticketingSystemLabel;
        private System.Windows.Forms.ComboBox ticketingSystemBox;
        private System.Windows.Forms.Button ticketingSystemConfigButton;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Button clockingSystemConfigButton;
    }
}