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
            this.clockingSystemLabel = new System.Windows.Forms.Label();
            this.clockingSystemBox = new System.Windows.Forms.ComboBox();
            this.collaborationSystemBox = new System.Windows.Forms.ComboBox();
            this.collaborationSystemConfigButton = new System.Windows.Forms.Button();
            this.collaborationSystemLabel = new System.Windows.Forms.Label();
            this.ticketingSystemLabel = new System.Windows.Forms.Label();
            this.ticketingSystemBox = new System.Windows.Forms.ComboBox();
            this.ticketingSystemConfigButton = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clockingSystemLabel
            // 
            this.clockingSystemLabel.AutoSize = true;
            this.clockingSystemLabel.Location = new System.Drawing.Point(5, 10);
            this.clockingSystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clockingSystemLabel.Name = "clockingSystemLabel";
            this.clockingSystemLabel.Size = new System.Drawing.Size(98, 15);
            this.clockingSystemLabel.TabIndex = 0;
            this.clockingSystemLabel.Text = "Clocking System:";
            // 
            // clockingSystemBox
            // 
            this.clockingSystemBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clockingSystemBox.FormattingEnabled = true;
            this.clockingSystemBox.Items.AddRange(new object[] {
            "WinTid",
            "Capitech",
            "[None]"});
            this.clockingSystemBox.Location = new System.Drawing.Point(130, 7);
            this.clockingSystemBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.clockingSystemBox.Name = "clockingSystemBox";
            this.clockingSystemBox.Size = new System.Drawing.Size(140, 23);
            this.clockingSystemBox.TabIndex = 1;
            this.clockingSystemBox.SelectedIndexChanged += new System.EventHandler(this.clockingSystemBox_SelectedIndexChanged);
            // 
            // collaborationSystemBox
            // 
            this.collaborationSystemBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.collaborationSystemBox.FormattingEnabled = true;
            this.collaborationSystemBox.Items.AddRange(new object[] {
            "MSTeams"});
            this.collaborationSystemBox.Location = new System.Drawing.Point(130, 37);
            this.collaborationSystemBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.collaborationSystemBox.Name = "collaborationSystemBox";
            this.collaborationSystemBox.Size = new System.Drawing.Size(140, 23);
            this.collaborationSystemBox.TabIndex = 3;
            this.collaborationSystemBox.SelectedIndexChanged += new System.EventHandler(this.collaborationSystemBox_SelectedIndexChanged);
            // 
            // collaborationSystemConfigButton
            // 
            this.collaborationSystemConfigButton.Enabled = false;
            this.collaborationSystemConfigButton.Location = new System.Drawing.Point(275, 36);
            this.collaborationSystemConfigButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.collaborationSystemConfigButton.Name = "collaborationSystemConfigButton";
            this.collaborationSystemConfigButton.Size = new System.Drawing.Size(90, 25);
            this.collaborationSystemConfigButton.TabIndex = 4;
            this.collaborationSystemConfigButton.Text = "Configure";
            this.collaborationSystemConfigButton.UseVisualStyleBackColor = true;
            this.collaborationSystemConfigButton.Click += new System.EventHandler(this.collaborationSystemConfigButton_Click);
            // 
            // collaborationSystemLabel
            // 
            this.collaborationSystemLabel.AutoSize = true;
            this.collaborationSystemLabel.Location = new System.Drawing.Point(5, 40);
            this.collaborationSystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.collaborationSystemLabel.Name = "collaborationSystemLabel";
            this.collaborationSystemLabel.Size = new System.Drawing.Size(123, 15);
            this.collaborationSystemLabel.TabIndex = 2;
            this.collaborationSystemLabel.Text = "Collaboration System:";
            // 
            // ticketingSystemLabel
            // 
            this.ticketingSystemLabel.AutoSize = true;
            this.ticketingSystemLabel.Location = new System.Drawing.Point(5, 70);
            this.ticketingSystemLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ticketingSystemLabel.Name = "ticketingSystemLabel";
            this.ticketingSystemLabel.Size = new System.Drawing.Size(99, 15);
            this.ticketingSystemLabel.TabIndex = 5;
            this.ticketingSystemLabel.Text = "Ticketing System:";
            // 
            // ticketingSystemBox
            // 
            this.ticketingSystemBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ticketingSystemBox.FormattingEnabled = true;
            this.ticketingSystemBox.Items.AddRange(new object[] {
            "Jira"});
            this.ticketingSystemBox.Location = new System.Drawing.Point(130, 67);
            this.ticketingSystemBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ticketingSystemBox.Name = "ticketingSystemBox";
            this.ticketingSystemBox.Size = new System.Drawing.Size(140, 23);
            this.ticketingSystemBox.TabIndex = 6;
            this.ticketingSystemBox.SelectedIndexChanged += new System.EventHandler(this.ticketingSystemBox_SelectedIndexChanged);
            // 
            // ticketingSystemConfigButton
            // 
            this.ticketingSystemConfigButton.Enabled = false;
            this.ticketingSystemConfigButton.Location = new System.Drawing.Point(275, 66);
            this.ticketingSystemConfigButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ticketingSystemConfigButton.Name = "ticketingSystemConfigButton";
            this.ticketingSystemConfigButton.Size = new System.Drawing.Size(90, 25);
            this.ticketingSystemConfigButton.TabIndex = 7;
            this.ticketingSystemConfigButton.Text = "Configure";
            this.ticketingSystemConfigButton.UseVisualStyleBackColor = true;
            this.ticketingSystemConfigButton.Click += new System.EventHandler(this.ticketingSystemConfigButton_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(180, 97);
            this.Cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(90, 25);
            this.Cancel_Button.TabIndex = 8;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK_Button.Enabled = false;
            this.OK_Button.Location = new System.Drawing.Point(275, 97);
            this.OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(90, 25);
            this.OK_Button.TabIndex = 9;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = true;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(370, 127);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.ticketingSystemBox);
            this.Controls.Add(this.ticketingSystemConfigButton);
            this.Controls.Add(this.ticketingSystemLabel);
            this.Controls.Add(this.collaborationSystemBox);
            this.Controls.Add(this.collaborationSystemConfigButton);
            this.Controls.Add(this.collaborationSystemLabel);
            this.Controls.Add(this.clockingSystemBox);
            this.Controls.Add(this.clockingSystemLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}