namespace ClockingSystemReminder.ClockingSystems.WinTid
{
    partial class WinTidSettingsDialog
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
            Cancel_Button = new System.Windows.Forms.Button();
            OK_Button = new System.Windows.Forms.Button();
            startWeekDatePicker = new System.Windows.Forms.DateTimePicker();
            enable24_7DutyBox = new System.Windows.Forms.CheckBox();
            dutyWeeksGroupBox = new System.Windows.Forms.GroupBox();
            weeksLabel = new System.Windows.Forms.Label();
            rotationIntervalBox = new Helpers.CustomNumericUpDown();
            rotationIntervalLabel = new System.Windows.Forms.Label();
            startWeekLabel = new System.Windows.Forms.Label();
            dutyWeeksGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rotationIntervalBox).BeginInit();
            SuspendLayout();
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel_Button.Location = new System.Drawing.Point(5, 110);
            Cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new System.Drawing.Size(90, 25);
            Cancel_Button.TabIndex = 2;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = true;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            OK_Button.Enabled = false;
            OK_Button.Location = new System.Drawing.Point(119, 110);
            OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new System.Drawing.Size(90, 25);
            OK_Button.TabIndex = 3;
            OK_Button.Text = "OK";
            OK_Button.UseVisualStyleBackColor = true;
            OK_Button.Click += OK_Button_Click;
            // 
            // startWeekDatePicker
            // 
            startWeekDatePicker.CustomFormat = "dd/MM/yyyy";
            startWeekDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            startWeekDatePicker.Location = new System.Drawing.Point(75, 25);
            startWeekDatePicker.Name = "startWeekDatePicker";
            startWeekDatePicker.Size = new System.Drawing.Size(118, 23);
            startWeekDatePicker.TabIndex = 1;
            // 
            // enable24_7DutyBox
            // 
            enable24_7DutyBox.AutoSize = true;
            enable24_7DutyBox.Location = new System.Drawing.Point(15, 10);
            enable24_7DutyBox.Name = "enable24_7DutyBox";
            enable24_7DutyBox.Size = new System.Drawing.Size(149, 19);
            enable24_7DutyBox.TabIndex = 0;
            enable24_7DutyBox.Text = "Enable 24/7 duty weeks";
            enable24_7DutyBox.UseVisualStyleBackColor = true;
            enable24_7DutyBox.CheckedChanged += enable24_7DutyBox_CheckedChanged;
            // 
            // dutyWeeksGroupBox
            // 
            dutyWeeksGroupBox.Controls.Add(weeksLabel);
            dutyWeeksGroupBox.Controls.Add(rotationIntervalBox);
            dutyWeeksGroupBox.Controls.Add(rotationIntervalLabel);
            dutyWeeksGroupBox.Controls.Add(startWeekLabel);
            dutyWeeksGroupBox.Controls.Add(startWeekDatePicker);
            dutyWeeksGroupBox.Enabled = false;
            dutyWeeksGroupBox.Location = new System.Drawing.Point(5, 10);
            dutyWeeksGroupBox.Name = "dutyWeeksGroupBox";
            dutyWeeksGroupBox.Size = new System.Drawing.Size(205, 90);
            dutyWeeksGroupBox.TabIndex = 1;
            dutyWeeksGroupBox.TabStop = false;
            // 
            // weeksLabel
            // 
            weeksLabel.AutoSize = true;
            weeksLabel.Location = new System.Drawing.Point(145, 60);
            weeksLabel.Name = "weeksLabel";
            weeksLabel.Size = new System.Drawing.Size(39, 15);
            weeksLabel.TabIndex = 4;
            weeksLabel.Text = "weeks";
            // 
            // rotationIntervalBox
            // 
            rotationIntervalBox.LargeIncrement = new decimal(new int[] { 1, 0, 0, 0 });
            rotationIntervalBox.Location = new System.Drawing.Point(105, 58);
            rotationIntervalBox.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            rotationIntervalBox.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            rotationIntervalBox.Name = "rotationIntervalBox";
            rotationIntervalBox.Size = new System.Drawing.Size(40, 23);
            rotationIntervalBox.TabIndex = 3;
            rotationIntervalBox.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // rotationIntervalLabel
            // 
            rotationIntervalLabel.AutoSize = true;
            rotationIntervalLabel.Location = new System.Drawing.Point(5, 60);
            rotationIntervalLabel.Name = "rotationIntervalLabel";
            rotationIntervalLabel.Size = new System.Drawing.Size(97, 15);
            rotationIntervalLabel.TabIndex = 2;
            rotationIntervalLabel.Text = "Rotation interval:";
            // 
            // startWeekLabel
            // 
            startWeekLabel.AutoSize = true;
            startWeekLabel.Location = new System.Drawing.Point(5, 28);
            startWeekLabel.Name = "startWeekLabel";
            startWeekLabel.Size = new System.Drawing.Size(64, 15);
            startWeekLabel.TabIndex = 0;
            startWeekLabel.Text = "Start week:";
            // 
            // WinTidSettingsForm
            // 
            this.AcceptButton = OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = Cancel_Button;
            this.ClientSize = new System.Drawing.Size(218, 141);
            this.Controls.Add(enable24_7DutyBox);
            this.Controls.Add(dutyWeeksGroupBox);
            this.Controls.Add(Cancel_Button);
            this.Controls.Add(OK_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WinTidSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WinTid Settings";
            dutyWeeksGroupBox.ResumeLayout(false);
            dutyWeeksGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rotationIntervalBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.DateTimePicker startWeekDatePicker;
        private System.Windows.Forms.CheckBox enable24_7DutyBox;
        private System.Windows.Forms.GroupBox dutyWeeksGroupBox;
        private System.Windows.Forms.Label startWeekLabel;
        private Helpers.CustomNumericUpDown rotationIntervalBox;
        private System.Windows.Forms.Label rotationIntervalLabel;
        private System.Windows.Forms.Label weeksLabel;
    }
}
