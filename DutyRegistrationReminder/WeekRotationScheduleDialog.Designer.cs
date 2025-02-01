namespace ClockingSystemReminder.ClockingSystems.WinTid
{
    partial class WeekRotationScheduleDialog
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
            startWeekLabel = new System.Windows.Forms.Label();
            rotationIntervalLabel = new System.Windows.Forms.Label();
            weeksLabel = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel_Button.Location = new System.Drawing.Point(5, 78);
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
            OK_Button.Location = new System.Drawing.Point(119, 78);
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
            startWeekDatePicker.Location = new System.Drawing.Point(80, 10);
            startWeekDatePicker.Name = "startWeekDatePicker";
            startWeekDatePicker.Size = new System.Drawing.Size(118, 23);
            startWeekDatePicker.TabIndex = 1;
            // 
            // startWeekLabel
            // 
            startWeekLabel.AutoSize = true;
            startWeekLabel.Location = new System.Drawing.Point(10, 13);
            startWeekLabel.Name = "startWeekLabel";
            startWeekLabel.Size = new System.Drawing.Size(64, 15);
            startWeekLabel.TabIndex = 0;
            startWeekLabel.Text = "Start week:";
            // 
            // rotationIntervalLabel
            // 
            rotationIntervalLabel.AutoSize = true;
            rotationIntervalLabel.Location = new System.Drawing.Point(10, 45);
            rotationIntervalLabel.Name = "rotationIntervalLabel";
            rotationIntervalLabel.Size = new System.Drawing.Size(97, 15);
            rotationIntervalLabel.TabIndex = 2;
            rotationIntervalLabel.Text = "Rotation interval:";
            // 
            // weeksLabel
            // 
            weeksLabel.AutoSize = true;
            weeksLabel.Location = new System.Drawing.Point(150, 45);
            weeksLabel.Name = "weeksLabel";
            weeksLabel.Size = new System.Drawing.Size(39, 15);
            weeksLabel.TabIndex = 4;
            weeksLabel.Text = "weeks";
            // 
            // WeekRotationScheduleDialog
            // 
            this.AcceptButton = OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = Cancel_Button;
            this.ClientSize = new System.Drawing.Size(218, 109);
            this.Controls.Add(weeksLabel);
            this.Controls.Add(rotationIntervalLabel);
            this.Controls.Add(Cancel_Button);
            this.Controls.Add(startWeekLabel);
            this.Controls.Add(startWeekDatePicker);
            this.Controls.Add(OK_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WeekRotationScheduleDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Week Rotation Schedule";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button OK_Button;
        private Helpers.CustomNumericUpDown rotationIntervalBox;
        private System.Windows.Forms.Label weeksLabel;
        private System.Windows.Forms.Label rotationIntervalLabel;
        private System.Windows.Forms.Label startWeekLabel;
        private System.Windows.Forms.DateTimePicker startWeekDatePicker;
    }
}
