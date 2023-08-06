using System;
using System.Windows.Forms;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder.Abstractions
{
    public partial class LoginDialog : AbstractLoginDialog
    {
        public LoginDialog(string systemName)
        {
            InitializeComponent();
            this.Text = systemName + " Login";
        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {
            this.Username = usernameBox.Text;
            EnableOKButtonCheck();
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            this.Password = passwordBox.Text;
            EnableOKButtonCheck();
        }

        private void EnableOKButtonCheck()
        {
            OK_Button.Enabled = this.Username != string.Empty && this.Password != string.Empty;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
