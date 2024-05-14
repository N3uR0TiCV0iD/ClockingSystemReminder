
namespace ClockingSystemReminder
{
    partial class ManualRegistrationDialog
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
            OK_Button = new System.Windows.Forms.Button();
            Cancel_Button = new System.Windows.Forms.Button();
            hourBox = new System.Windows.Forms.NumericUpDown();
            separatorLabel = new System.Windows.Forms.Label();
            minuteBox = new System.Windows.Forms.NumericUpDown();
            dateTimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)hourBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minuteBox).BeginInit();
            SuspendLayout();
            // 
            // OK_Button
            // 
            OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            OK_Button.Enabled = false;
            OK_Button.Location = new System.Drawing.Point(100, 76);
            OK_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new System.Drawing.Size(85, 25);
            OK_Button.TabIndex = 5;
            OK_Button.Text = "OK";
            OK_Button.UseVisualStyleBackColor = true;
            OK_Button.Click += OK_Button_Click;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel_Button.Location = new System.Drawing.Point(5, 76);
            Cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new System.Drawing.Size(85, 25);
            Cancel_Button.TabIndex = 4;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = true;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // hourBox
            // 
            hourBox.Location = new System.Drawing.Point(37, 41);
            hourBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            hourBox.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            hourBox.Name = "hourBox";
            hourBox.Size = new System.Drawing.Size(49, 23);
            hourBox.TabIndex = 1;
            hourBox.Value = new decimal(new int[] { 7, 0, 0, 0 });
            hourBox.ValueChanged += hourBox_ValueChanged;
            // 
            // separatorLabel
            // 
            separatorLabel.AutoSize = true;
            separatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            separatorLabel.Location = new System.Drawing.Point(84, 40);
            separatorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            separatorLabel.Name = "separatorLabel";
            separatorLabel.Size = new System.Drawing.Size(15, 20);
            separatorLabel.TabIndex = 2;
            separatorLabel.Text = ":";
            // 
            // minuteBox
            // 
            minuteBox.Location = new System.Drawing.Point(97, 41);
            minuteBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            minuteBox.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            minuteBox.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            minuteBox.Name = "minuteBox";
            minuteBox.Size = new System.Drawing.Size(49, 23);
            minuteBox.TabIndex = 3;
            minuteBox.Value = new decimal(new int[] { 30, 0, 0, 0 });
            minuteBox.ValueChanged += minuteBox_ValueChanged;
            // 
            // dateTimePicker
            // 
            dateTimePicker.CustomFormat = "dd/MM/yyyy";
            dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker.Location = new System.Drawing.Point(37, 12);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new System.Drawing.Size(110, 23);
            dateTimePicker.TabIndex = 0;
            dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
            // 
            // ManualRegistrationDialog
            // 
            this.AcceptButton = OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = Cancel_Button;
            this.ClientSize = new System.Drawing.Size(189, 108);
            this.Controls.Add(dateTimePicker);
            this.Controls.Add(minuteBox);
            this.Controls.Add(hourBox);
            this.Controls.Add(Cancel_Button);
            this.Controls.Add(OK_Button);
            this.Controls.Add(separatorLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "ManualRegistrationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual registration:";
            ((System.ComponentModel.ISupportInitialize)hourBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)minuteBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.NumericUpDown hourBox;
        private System.Windows.Forms.Label separatorLabel;
        private System.Windows.Forms.NumericUpDown minuteBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}
