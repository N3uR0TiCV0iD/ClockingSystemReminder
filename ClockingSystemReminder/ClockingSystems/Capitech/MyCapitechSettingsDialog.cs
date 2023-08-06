using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder.ClockingSystems.Capitech
{
    public partial class MyCapitechSettingsDialog : AbstractLoginDialog
    {
        readonly List<int> loginIDs;

        public MyCapitechSettingsDialog(List<CapitechLoginClient> loginClients)
        {
            InitializeComponent();
            this.loginIDs = new List<int>();
            foreach (var currLoginClient in loginClients)
            {
                this.loginIDs.Add(currLoginClient.ID);
                loginClientsBox.Items.Add(currLoginClient.Name);
            }
            loginClientsBox.SelectedIndex = 0;
        }

        public int ClientID
        {
            get
            {
                return loginIDs[loginClientsBox.SelectedIndex];
            }
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
