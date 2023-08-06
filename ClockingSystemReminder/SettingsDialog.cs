using System;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.ClockingSystems;
using ClockingSystemReminder.CollaborationSystems;
using ClockingSystemReminder.TicketingSystems;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    public partial class SettingsDialog : Form
    {
        public ClockingSystem ClockingSystem { get; private set; }
        public ITicketingSystem TicketingSystem { get; private set; }
        public ICollaborationSystem CollaborationSystem { get; private set; }

        bool loading;

        public SettingsDialog(ClockingSystem clockingSystem,
                              ITicketingSystem ticketingSystem,
                              ICollaborationSystem collaborationSystem)
        {
            this.loading = true;
            InitializeComponent();

            if (clockingSystem != null)
            {
                this.ClockingSystem = clockingSystem;
                clockingSystemBox.SelectedItem = clockingSystem.FriendlySystemName;
            }

            if (ticketingSystem != null)
            {
                this.TicketingSystem = ticketingSystem;
                ticketingSystemConfigButton.Enabled = true;
                ticketingSystemBox.SelectedItem = ticketingSystem.GetType().Name;
            }

            if (collaborationSystem != null)
            {
                this.CollaborationSystem = collaborationSystem;
                collaborationSystemConfigButton.Enabled = true;
                collaborationSystemBox.SelectedItem = collaborationSystem.GetType().Name;
            }

            if (ClockingManager.ClockedIn)
            {
                this.clockingSystemBox.Enabled = false;
            }
            OKButtonEnableCheck();
            this.loading = false;
        }

        private void clockingSystemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClockingSystem = ClockingSystemResolver.Resolve(clockingSystemBox.Text);
            OKButtonEnableCheck();
        }

        private void collaborationSystemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                this.CollaborationSystem = CollaborationSystemResolver.Load(collaborationSystemBox.Text);
                collaborationSystemConfigButton.Enabled = true;
                OKButtonEnableCheck();
            }
        }

        private void collaborationSystemConfigButton_Click(object sender, EventArgs e)
        {
            this.CollaborationSystem.OpenSettings();
            OKButtonEnableCheck();
        }

        private void ticketingSystemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                this.TicketingSystem = TicketingSystemResolver.Load(ticketingSystemBox.Text);
                ticketingSystemConfigButton.Enabled = true;
                OKButtonEnableCheck();
            }
        }

        private void ticketingSystemConfigButton_Click(object sender, EventArgs e)
        {
            this.TicketingSystem.OpenSettings();
            OKButtonEnableCheck();
        }

        private void OKButtonEnableCheck()
        {
            OK_Button.Enabled = this.ClockingSystem != null &&
                                this.TicketingSystem != null && this.TicketingSystem.IsConfigured() &&
                                this.CollaborationSystem != null && this.CollaborationSystem.IsConfigured();
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
