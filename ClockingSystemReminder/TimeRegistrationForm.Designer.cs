namespace ClockingSystemReminder
{
    partial class TimeRegistrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.meetingsBox = new System.Windows.Forms.GroupBox();
            this.meetingsView = new System.Windows.Forms.DataGridView();
            this.meetingsViewTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meetingsViewDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meetingsViewTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meetingsViewTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meetingsViewDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new System.Windows.Forms.Button();
            this.callsBox = new System.Windows.Forms.GroupBox();
            this.callsView = new System.Windows.Forms.DataGridView();
            this.callsViewTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callsViewDurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callsViewParticipantsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callsViewTicketColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callsViewDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manualRegistrationsBox = new System.Windows.Forms.GroupBox();
            this.manualRegistrationsView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.meetingsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meetingsView)).BeginInit();
            this.callsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.callsView)).BeginInit();
            this.manualRegistrationsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manualRegistrationsView)).BeginInit();
            this.SuspendLayout();
            // 
            // meetingsBox
            // 
            this.meetingsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.meetingsBox.Controls.Add(this.meetingsView);
            this.meetingsBox.Location = new System.Drawing.Point(10, 5);
            this.meetingsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.meetingsBox.Name = "meetingsBox";
            this.meetingsBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.meetingsBox.Size = new System.Drawing.Size(902, 218);
            this.meetingsBox.TabIndex = 0;
            this.meetingsBox.TabStop = false;
            this.meetingsBox.Text = "Meetings";
            // 
            // meetingsView
            // 
            this.meetingsView.AllowUserToAddRows = false;
            this.meetingsView.AllowUserToDeleteRows = false;
            this.meetingsView.AllowUserToResizeColumns = false;
            this.meetingsView.AllowUserToResizeRows = false;
            this.meetingsView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.meetingsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.meetingsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.meetingsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.meetingsViewTime,
            this.meetingsViewDuration,
            this.meetingsViewTitle,
            this.meetingsViewTicket,
            this.meetingsViewDescription});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.meetingsView.DefaultCellStyle = dataGridViewCellStyle2;
            this.meetingsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meetingsView.EnableHeadersVisualStyles = false;
            this.meetingsView.Location = new System.Drawing.Point(4, 19);
            this.meetingsView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.meetingsView.MultiSelect = false;
            this.meetingsView.Name = "meetingsView";
            this.meetingsView.ReadOnly = true;
            this.meetingsView.RowHeadersVisible = false;
            this.meetingsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.meetingsView.Size = new System.Drawing.Size(894, 196);
            this.meetingsView.TabIndex = 0;
            this.meetingsView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.meetingsView_CellDoubleClick);
            this.meetingsView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.meetingsView_CellMouseClick);
            // 
            // meetingsViewTime
            // 
            this.meetingsViewTime.HeaderText = "Time";
            this.meetingsViewTime.Name = "meetingsViewTime";
            this.meetingsViewTime.ReadOnly = true;
            this.meetingsViewTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.meetingsViewTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.meetingsViewTime.Width = 40;
            // 
            // meetingsViewDuration
            // 
            this.meetingsViewDuration.HeaderText = "Duration";
            this.meetingsViewDuration.Name = "meetingsViewDuration";
            this.meetingsViewDuration.ReadOnly = true;
            this.meetingsViewDuration.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.meetingsViewDuration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.meetingsViewDuration.Width = 50;
            // 
            // meetingsViewTitle
            // 
            this.meetingsViewTitle.HeaderText = "Title";
            this.meetingsViewTitle.Name = "meetingsViewTitle";
            this.meetingsViewTitle.ReadOnly = true;
            this.meetingsViewTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.meetingsViewTitle.Width = 300;
            // 
            // meetingsViewTicket
            // 
            this.meetingsViewTicket.HeaderText = "Ticket";
            this.meetingsViewTicket.Name = "meetingsViewTicket";
            this.meetingsViewTicket.ReadOnly = true;
            this.meetingsViewTicket.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.meetingsViewTicket.Width = 300;
            // 
            // meetingsViewDescription
            // 
            this.meetingsViewDescription.HeaderText = "Description";
            this.meetingsViewDescription.Name = "meetingsViewDescription";
            this.meetingsViewDescription.ReadOnly = true;
            this.meetingsViewDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.meetingsViewDescription.Width = 200;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(14, 710);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(896, 27);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // callsBox
            // 
            this.callsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.callsBox.Controls.Add(this.callsView);
            this.callsBox.Location = new System.Drawing.Point(10, 230);
            this.callsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.callsBox.Name = "callsBox";
            this.callsBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.callsBox.Size = new System.Drawing.Size(902, 237);
            this.callsBox.TabIndex = 1;
            this.callsBox.TabStop = false;
            this.callsBox.Text = "Calls";
            // 
            // callsView
            // 
            this.callsView.AllowUserToAddRows = false;
            this.callsView.AllowUserToDeleteRows = false;
            this.callsView.AllowUserToResizeColumns = false;
            this.callsView.AllowUserToResizeRows = false;
            this.callsView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.callsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.callsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.callsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.callsViewTimeColumn,
            this.callsViewDurationColumn,
            this.callsViewParticipantsColumn,
            this.callsViewTicketColumn,
            this.callsViewDescriptionColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.callsView.DefaultCellStyle = dataGridViewCellStyle4;
            this.callsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.callsView.EnableHeadersVisualStyles = false;
            this.callsView.Location = new System.Drawing.Point(4, 19);
            this.callsView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.callsView.MultiSelect = false;
            this.callsView.Name = "callsView";
            this.callsView.ReadOnly = true;
            this.callsView.RowHeadersVisible = false;
            this.callsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.callsView.Size = new System.Drawing.Size(894, 215);
            this.callsView.TabIndex = 0;
            this.callsView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.callsView_CellDoubleClick);
            // 
            // callsViewTimeColumn
            // 
            this.callsViewTimeColumn.HeaderText = "Time";
            this.callsViewTimeColumn.Name = "callsViewTimeColumn";
            this.callsViewTimeColumn.ReadOnly = true;
            this.callsViewTimeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.callsViewTimeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.callsViewTimeColumn.Width = 40;
            // 
            // callsViewDurationColumn
            // 
            this.callsViewDurationColumn.HeaderText = "Duration";
            this.callsViewDurationColumn.Name = "callsViewDurationColumn";
            this.callsViewDurationColumn.ReadOnly = true;
            this.callsViewDurationColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.callsViewDurationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.callsViewDurationColumn.Width = 50;
            // 
            // callsViewParticipantsColumn
            // 
            this.callsViewParticipantsColumn.HeaderText = "Participants";
            this.callsViewParticipantsColumn.Name = "callsViewParticipantsColumn";
            this.callsViewParticipantsColumn.ReadOnly = true;
            this.callsViewParticipantsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.callsViewParticipantsColumn.Width = 300;
            // 
            // callsViewTicketColumn
            // 
            this.callsViewTicketColumn.HeaderText = "Ticket";
            this.callsViewTicketColumn.Name = "callsViewTicketColumn";
            this.callsViewTicketColumn.ReadOnly = true;
            this.callsViewTicketColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.callsViewTicketColumn.Width = 300;
            // 
            // callsViewDescriptionColumn
            // 
            this.callsViewDescriptionColumn.HeaderText = "Description";
            this.callsViewDescriptionColumn.Name = "callsViewDescriptionColumn";
            this.callsViewDescriptionColumn.ReadOnly = true;
            this.callsViewDescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.callsViewDescriptionColumn.Width = 200;
            // 
            // manualRegistrationsBox
            // 
            this.manualRegistrationsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.manualRegistrationsBox.Controls.Add(this.manualRegistrationsView);
            this.manualRegistrationsBox.Location = new System.Drawing.Point(10, 475);
            this.manualRegistrationsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.manualRegistrationsBox.Name = "manualRegistrationsBox";
            this.manualRegistrationsBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.manualRegistrationsBox.Size = new System.Drawing.Size(561, 224);
            this.manualRegistrationsBox.TabIndex = 2;
            this.manualRegistrationsBox.TabStop = false;
            this.manualRegistrationsBox.Text = "Manual Registrations";
            // 
            // manualRegistrationsView
            // 
            this.manualRegistrationsView.AllowUserToDeleteRows = false;
            this.manualRegistrationsView.AllowUserToResizeColumns = false;
            this.manualRegistrationsView.AllowUserToResizeRows = false;
            this.manualRegistrationsView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.manualRegistrationsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.manualRegistrationsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.manualRegistrationsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.manualRegistrationsView.DefaultCellStyle = dataGridViewCellStyle6;
            this.manualRegistrationsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manualRegistrationsView.EnableHeadersVisualStyles = false;
            this.manualRegistrationsView.Location = new System.Drawing.Point(4, 19);
            this.manualRegistrationsView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.manualRegistrationsView.MultiSelect = false;
            this.manualRegistrationsView.Name = "manualRegistrationsView";
            this.manualRegistrationsView.ReadOnly = true;
            this.manualRegistrationsView.RowHeadersVisible = false;
            this.manualRegistrationsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.manualRegistrationsView.Size = new System.Drawing.Size(553, 202);
            this.manualRegistrationsView.TabIndex = 0;
            this.manualRegistrationsView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.manualRegistrationsView_CellDoubleClick);
            this.manualRegistrationsView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.manualRegistrationsView_KeyDown);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Duration";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 50;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Ticket";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 300;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Description";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 200;
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // TimeRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 748);
            this.Controls.Add(this.manualRegistrationsBox);
            this.Controls.Add(this.callsBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.meetingsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "TimeRegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Registration";
            this.Load += new System.EventHandler(this.HourRegistration_Load);
            this.meetingsBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meetingsView)).EndInit();
            this.callsBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.callsView)).EndInit();
            this.manualRegistrationsBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.manualRegistrationsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox meetingsBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox callsBox;
        private System.Windows.Forms.GroupBox manualRegistrationsBox;
        private System.Windows.Forms.DataGridView meetingsView;
        private System.Windows.Forms.DataGridView callsView;
        private System.Windows.Forms.DataGridView manualRegistrationsView;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetingsViewTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetingsViewDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetingsViewTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetingsViewTicket;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetingsViewDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn callsViewTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callsViewDurationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callsViewParticipantsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callsViewTicketColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callsViewDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
    }
}