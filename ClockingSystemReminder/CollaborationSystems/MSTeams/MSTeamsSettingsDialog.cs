using System;
using System.Windows.Forms;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder.CollaborationSystems.MSTeams
{
    public partial class MSTeamsSettingsDialog : Form
    {
        public string TenantID => tenantIDBox.Text;

        public MSTeamsSettingsDialog(MSTeamsSettings settings)
        {
            InitializeComponent();
            this.tenantIDBox.Text = settings.TenantID;
        }

        private void tenantIDBox_TextChanged(object sender, EventArgs e)
        {
            OK_Button.Enabled = ValidationUtils.IsValidGUID(tenantIDBox.Text);
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
