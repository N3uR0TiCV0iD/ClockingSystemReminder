using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClockingSystemReminder.Helpers
{
    public class PlaceholderTextBox : TextBox
    {
        bool gettingFocus; //Hack to allow the textbox to clear its text when focused
        char passwordChar;
        bool useSystemPWDChar;
        bool ignoreTextChange;
        bool placeholderActive;
        string placeholderText;
        Color placeholderColor = Color.Gray;
        Color foreColor = SystemColors.WindowText;

        public PlaceholderTextBox()
        {
            this.placeholderActive = true;
        }

#if NET5_0_OR_GREATER
        public override string PlaceholderText
#else
        public string PlaceholderText
#endif
        {
            get
            {
                return placeholderText;
            }
            set
            {
                placeholderText = value;
                ShowPlaceholderIfNecessary();
            }
        }

        public Color PlaceholderColor
        {
            get
            {
                return placeholderColor;
            }
            set
            {
                placeholderColor = value;
                if (placeholderActive)
                {
                    base.ForeColor = value;
                }
            }
        }

        public override string Text
        {
            get
            {
                if (placeholderActive)
                {
                    return !gettingFocus ? string.Empty : null;
                }
                return base.Text;
            }
            set
            {
                placeholderActive = string.IsNullOrEmpty(value);
                if (placeholderActive)
                {
                    ShowPlaceholder();
                }
                else
                {
                    base.Text = value;
                }
            }
        }

        public new Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
                if (!placeholderActive)
                {
                    base.ForeColor = value;
                }
            }
        }

        public new bool UseSystemPasswordChar
        {
            get
            {
                return useSystemPWDChar;
            }
            set
            {
                useSystemPWDChar = value;
                if (!placeholderActive)
                {
                    base.UseSystemPasswordChar = value;
                }
            }
        }

        public new char PasswordChar
        {
            get
            {
                return passwordChar;
            }
            set
            {
                passwordChar = value;
                if (!placeholderActive)
                {
                    base.PasswordChar = value;
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (placeholderActive)
            {
                gettingFocus = true;
                ignoreTextChange = true;

                this.SelectionLength = 0;
                base.Text = string.Empty;

                ignoreTextChange = false;
                gettingFocus = false;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!ignoreTextChange)
            {
                placeholderActive = string.IsNullOrEmpty(base.Text);
                if (!placeholderActive)
                {
                    RemovePlaceholder();
                }
                base.OnTextChanged(e);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            ShowPlaceholderIfNecessary();
        }

        private void ShowPlaceholderIfNecessary()
        {
            if (placeholderActive)
            {
                ShowPlaceholder();
            }
        }

        private void ShowPlaceholder()
        {
            base.PasswordChar = '\0';
            base.UseSystemPasswordChar = false;
            base.ForeColor = placeholderColor;
            ignoreTextChange = true;

            base.Text = placeholderText;

            ignoreTextChange = false;
        }

        private void RemovePlaceholder()
        {
            base.ForeColor = foreColor;
            if (useSystemPWDChar)
            {
                if (base.UseSystemPasswordChar != useSystemPWDChar)
                {
                    base.UseSystemPasswordChar = true;
                    this.Select(base.Text.Length, 0);
                }
            }
            else if (base.PasswordChar != passwordChar)
            {
                base.PasswordChar = passwordChar;
                this.Select(base.Text.Length, 0);
            }
        }
    }
}
