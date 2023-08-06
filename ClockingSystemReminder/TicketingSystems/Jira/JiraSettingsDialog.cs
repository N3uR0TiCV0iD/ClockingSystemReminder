using System;
using System.Windows.Forms;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder.TicketingSystems.Jira
{
    public partial class JiraSettingsDialog : Form
    {
        const string TEMPO_TOKENS_URI = "plugins/servlet/ac/io.tempo.jira/tempo-app#!/configuration/api-integration";

        //Just to avoid reparsing the URL/Email multiple times
        bool validURL;
        bool validEmail;

        public JiraSettingsDialog(JiraSettings settings)
        {
            InitializeComponent();
            this.jiraURLBox.Text = settings.URL;
            this.jiraEmailBox.Text = settings.Email;
            this.jiraTokenBox.Text = settings.JiraToken;
            this.tempoTokenBox.Text = settings.TempoToken;
        }

        public JiraSettings NewSettings
        {
            get
            {
                return new JiraSettings()
                {
                    URL = jiraURLBox.Text,
                    Email = jiraEmailBox.Text,
                    TempoToken = tempoTokenBox.Text,
                    JiraToken = jiraTokenBox.Text
                };
            }
        }

        private void jiraURLBox_TextChanged(object sender, EventArgs e)
        {
            validURL = ValidationUtils.IsValidURL(jiraURLBox.Text, true);
            tempoTokenSetupLabel.Enabled = validURL;
            OKButtonEnableCheck();
            Issue7341Hack();
        }

        private void Issue7341Hack()
        {
            //https://github.com/dotnet/winforms/issues/7341
            tempoTokenSetupLabel.Text = validURL ? "Setup" : "Setup ";
        }

        private void jiraEmailBox_TextChanged(object sender, EventArgs e)
        {
            validEmail = ValidationUtils.IsValidEmail(jiraEmailBox.Text);
            OKButtonEnableCheck();
        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {
            OKButtonEnableCheck();
        }

        private void OKButtonEnableCheck()
        {
            OK_Button.Enabled = validURL && validEmail &&
                                ValidateToken(jiraTokenBox) &&
                                ValidateToken(tempoTokenBox);
        }

        private bool ValidateToken(TextBox tokenTextBox)
        {
            var token = tokenTextBox.Text;
            return !token.Contains(' ') && token.Length == tokenTextBox.MaxLength;
        }

        private void jiraTokenSetupLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.OpenWebBrowser("https://id.atlassian.com/manage-profile/security/api-tokens");
        }

        private void tempoTokenSetupLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = jiraURLBox.Text + TEMPO_TOKENS_URI;
            MessageBox.Show("Please grant the \"Manage worklogs\" scope to the new token.",
                            "New token - Required scope", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Utils.OpenWebBrowser(url);
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
