namespace ClockingSystemReminder
{
    partial class TicketRegistrationDialog
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
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.ticketGroupTabs = new System.Windows.Forms.TabControl();
            this.favoriteTicketsTab = new System.Windows.Forms.TabPage();
            this.favoriteTicketsBox = new System.Windows.Forms.ListBox();
            this.recentTicketsTab = new System.Windows.Forms.TabPage();
            this.recentTicketsBox = new System.Windows.Forms.ListBox();
            this.assignedTicketsTab = new System.Windows.Forms.TabPage();
            this.assignedTicketsBox = new System.Windows.Forms.ListBox();
            this.searchTab = new System.Windows.Forms.TabPage();
            this.searchResultsBox = new System.Windows.Forms.ListBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.durationLabel = new System.Windows.Forms.Label();
            this.minutesBox = new ClockingSystemReminder.Helpers.CustomDomainUpDown();
            this.hoursBox = new ClockingSystemReminder.Helpers.CustomNumericUpDown();
            this.timeTrackBar = new System.Windows.Forms.TrackBar();
            this.OK_Button = new System.Windows.Forms.Button();
            this.durationPanel = new System.Windows.Forms.Panel();
            this.percentageLabel = new System.Windows.Forms.Label();
            this.ticketGroupTabs.SuspendLayout();
            this.favoriteTicketsTab.SuspendLayout();
            this.recentTicketsTab.SuspendLayout();
            this.assignedTicketsTab.SuspendLayout();
            this.searchTab.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTrackBar)).BeginInit();
            this.durationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(10, 250);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(325, 23);
            this.descriptionBox.TabIndex = 2;
            this.descriptionBox.TextChanged += new System.EventHandler(this.descriptionBox_TextChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(8, 230);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(67, 15);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "Description";
            // 
            // ticketGroupTabs
            // 
            this.ticketGroupTabs.Controls.Add(this.favoriteTicketsTab);
            this.ticketGroupTabs.Controls.Add(this.recentTicketsTab);
            this.ticketGroupTabs.Controls.Add(this.assignedTicketsTab);
            this.ticketGroupTabs.Controls.Add(this.searchTab);
            this.ticketGroupTabs.Location = new System.Drawing.Point(10, 10);
            this.ticketGroupTabs.Name = "ticketGroupTabs";
            this.ticketGroupTabs.SelectedIndex = 0;
            this.ticketGroupTabs.Size = new System.Drawing.Size(325, 215);
            this.ticketGroupTabs.TabIndex = 0;
            // 
            // favoriteTicketsTab
            // 
            this.favoriteTicketsTab.Controls.Add(this.favoriteTicketsBox);
            this.favoriteTicketsTab.Location = new System.Drawing.Point(4, 24);
            this.favoriteTicketsTab.Name = "favoriteTicketsTab";
            this.favoriteTicketsTab.Padding = new System.Windows.Forms.Padding(3);
            this.favoriteTicketsTab.Size = new System.Drawing.Size(317, 187);
            this.favoriteTicketsTab.TabIndex = 0;
            this.favoriteTicketsTab.Text = "Favorites";
            this.favoriteTicketsTab.UseVisualStyleBackColor = true;
            // 
            // favoriteTicketsBox
            // 
            this.favoriteTicketsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favoriteTicketsBox.FormattingEnabled = true;
            this.favoriteTicketsBox.ItemHeight = 15;
            this.favoriteTicketsBox.Location = new System.Drawing.Point(3, 3);
            this.favoriteTicketsBox.Name = "favoriteTicketsBox";
            this.favoriteTicketsBox.Size = new System.Drawing.Size(311, 181);
            this.favoriteTicketsBox.TabIndex = 0;
            this.favoriteTicketsBox.SelectedIndexChanged += new System.EventHandler(this.favoriteTicketsBox_SelectedIndexChanged);
            this.favoriteTicketsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TicketsBox_MouseDoubleClick);
            // 
            // recentTicketsTab
            // 
            this.recentTicketsTab.Controls.Add(this.recentTicketsBox);
            this.recentTicketsTab.Location = new System.Drawing.Point(4, 24);
            this.recentTicketsTab.Name = "recentTicketsTab";
            this.recentTicketsTab.Padding = new System.Windows.Forms.Padding(3);
            this.recentTicketsTab.Size = new System.Drawing.Size(317, 187);
            this.recentTicketsTab.TabIndex = 0;
            this.recentTicketsTab.Text = "Recent";
            this.recentTicketsTab.UseVisualStyleBackColor = true;
            // 
            // recentTicketsBox
            // 
            this.recentTicketsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentTicketsBox.FormattingEnabled = true;
            this.recentTicketsBox.ItemHeight = 15;
            this.recentTicketsBox.Location = new System.Drawing.Point(3, 3);
            this.recentTicketsBox.Name = "recentTicketsBox";
            this.recentTicketsBox.Size = new System.Drawing.Size(311, 181);
            this.recentTicketsBox.TabIndex = 0;
            this.recentTicketsBox.SelectedIndexChanged += new System.EventHandler(this.recentTicketsBox_SelectedIndexChanged);
            this.recentTicketsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TicketsBox_MouseDoubleClick);
            // 
            // assignedTicketsTab
            // 
            this.assignedTicketsTab.Controls.Add(this.assignedTicketsBox);
            this.assignedTicketsTab.Location = new System.Drawing.Point(4, 24);
            this.assignedTicketsTab.Name = "assignedTicketsTab";
            this.assignedTicketsTab.Padding = new System.Windows.Forms.Padding(3);
            this.assignedTicketsTab.Size = new System.Drawing.Size(317, 187);
            this.assignedTicketsTab.TabIndex = 1;
            this.assignedTicketsTab.Text = "Assigned";
            this.assignedTicketsTab.UseVisualStyleBackColor = true;
            // 
            // assignedTicketsBox
            // 
            this.assignedTicketsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assignedTicketsBox.FormattingEnabled = true;
            this.assignedTicketsBox.ItemHeight = 15;
            this.assignedTicketsBox.Location = new System.Drawing.Point(3, 3);
            this.assignedTicketsBox.Name = "assignedTicketsBox";
            this.assignedTicketsBox.Size = new System.Drawing.Size(311, 181);
            this.assignedTicketsBox.TabIndex = 0;
            this.assignedTicketsBox.SelectedIndexChanged += new System.EventHandler(this.assignedTicketsBox_SelectedIndexChanged);
            this.assignedTicketsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TicketsBox_MouseDoubleClick);
            // 
            // searchTab
            // 
            this.searchTab.Controls.Add(this.searchResultsBox);
            this.searchTab.Controls.Add(this.searchPanel);
            this.searchTab.Location = new System.Drawing.Point(4, 24);
            this.searchTab.Name = "searchTab";
            this.searchTab.Size = new System.Drawing.Size(317, 187);
            this.searchTab.TabIndex = 2;
            this.searchTab.Text = "Search";
            this.searchTab.UseVisualStyleBackColor = true;
            // 
            // searchResultsBox
            // 
            this.searchResultsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultsBox.FormattingEnabled = true;
            this.searchResultsBox.ItemHeight = 15;
            this.searchResultsBox.Location = new System.Drawing.Point(0, 23);
            this.searchResultsBox.Name = "searchResultsBox";
            this.searchResultsBox.Size = new System.Drawing.Size(317, 164);
            this.searchResultsBox.TabIndex = 1;
            this.searchResultsBox.SelectedIndexChanged += new System.EventHandler(this.searchResultsBox_SelectedIndexChanged);
            this.searchResultsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TicketsBox_MouseDoubleClick);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchPanel.Controls.Add(this.searchBox);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(317, 23);
            this.searchPanel.TabIndex = 3;
            // 
            // searchBox
            // 
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(313, 23);
            this.searchBox.TabIndex = 0;
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(0, 0);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(53, 15);
            this.durationLabel.TabIndex = 3;
            this.durationLabel.Text = "Duration";
            // 
            // minutesBox
            // 
            this.minutesBox.Items.Add("45");
            this.minutesBox.Items.Add("30");
            this.minutesBox.Items.Add("15");
            this.minutesBox.Items.Add("00");
            this.minutesBox.Location = new System.Drawing.Point(45, 20);
            this.minutesBox.Name = "minutesBox";
            this.minutesBox.ReadOnly = true;
            this.minutesBox.Size = new System.Drawing.Size(40, 23);
            this.minutesBox.TabIndex = 1;
            this.minutesBox.Text = "00";
            this.minutesBox.Wrap = true;
            this.minutesBox.SelectedItemChanged += new System.EventHandler(this.minutesBox_SelectedItemChanged);
            // 
            // hoursBox
            // 
            this.hoursBox.LargeIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hoursBox.Location = new System.Drawing.Point(0, 20);
            this.hoursBox.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hoursBox.Name = "hoursBox";
            this.hoursBox.Size = new System.Drawing.Size(40, 23);
            this.hoursBox.TabIndex = 0;
            this.hoursBox.ValueChanged += new System.EventHandler(this.hoursBox_ValueChanged);
            // 
            // timeTrackBar
            // 
            this.timeTrackBar.Location = new System.Drawing.Point(90, 20);
            this.timeTrackBar.Name = "timeTrackBar";
            this.timeTrackBar.Size = new System.Drawing.Size(125, 45);
            this.timeTrackBar.TabIndex = 2;
            this.timeTrackBar.Scroll += new System.EventHandler(this.timeTrackBar_Scroll);
            // 
            // OK_Button
            // 
            this.OK_Button.Enabled = false;
            this.OK_Button.Location = new System.Drawing.Point(260, 297);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(75, 23);
            this.OK_Button.TabIndex = 5;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = true;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // durationPanel
            // 
            this.durationPanel.Controls.Add(this.durationLabel);
            this.durationPanel.Controls.Add(this.timeTrackBar);
            this.durationPanel.Controls.Add(this.hoursBox);
            this.durationPanel.Controls.Add(this.minutesBox);
            this.durationPanel.Controls.Add(this.percentageLabel);
            this.durationPanel.Location = new System.Drawing.Point(10, 277);
            this.durationPanel.Name = "durationPanel";
            this.durationPanel.Size = new System.Drawing.Size(245, 50);
            this.durationPanel.TabIndex = 4;
            // 
            // percentageLabel
            // 
            this.percentageLabel.Location = new System.Drawing.Point(210, 24);
            this.percentageLabel.Name = "percentageLabel";
            this.percentageLabel.Size = new System.Drawing.Size(35, 15);
            this.percentageLabel.TabIndex = 3;
            this.percentageLabel.Text = "0%";
            this.percentageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TicketRegistrationDialog
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 337);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.ticketGroupTabs);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.durationPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TicketRegistrationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ticket Registration";
            this.ticketGroupTabs.ResumeLayout(false);
            this.favoriteTicketsTab.ResumeLayout(false);
            this.recentTicketsTab.ResumeLayout(false);
            this.assignedTicketsTab.ResumeLayout(false);
            this.searchTab.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTrackBar)).EndInit();
            this.durationPanel.ResumeLayout(false);
            this.durationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TabControl ticketGroupTabs;
        private System.Windows.Forms.TabPage favoriteTicketsTab;
        private System.Windows.Forms.TabPage assignedTicketsTab;
        private System.Windows.Forms.TabPage searchTab;
        private System.Windows.Forms.ListBox favoriteTicketsBox;
        private System.Windows.Forms.ListBox recentTicketsBox;
        private System.Windows.Forms.ListBox assignedTicketsBox;
        private System.Windows.Forms.ListBox searchResultsBox;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label durationLabel;
        private ClockingSystemReminder.Helpers.CustomDomainUpDown minutesBox;
        private ClockingSystemReminder.Helpers.CustomNumericUpDown hoursBox;
        private System.Windows.Forms.TrackBar timeTrackBar;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Panel durationPanel;
        private System.Windows.Forms.Label percentageLabel;
        private System.Windows.Forms.TabPage recentTicketsTab;
    }
}