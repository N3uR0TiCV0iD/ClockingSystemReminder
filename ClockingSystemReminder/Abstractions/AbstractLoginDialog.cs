using System;
using System.Windows.Forms;

namespace ClockingSystemReminder.Abstractions
{
    public class AbstractLoginDialog : Form
    {
        //Effectively make it abstract. The class itself can't be abstract, otherwise the designer complains :(
        protected AbstractLoginDialog()
        {
        }

        public string Username { get; protected set; }
        public string Password { get; protected set; }
    }
}
