namespace Messenger
{
    public partial class LoginUC : UserControl
    {
        private readonly Delegate loginDelegate;

        public LoginUC(Delegate loginDelegate)
        {
            InitializeComponent();

            this.loginDelegate = loginDelegate;
        }

        private void ButtonLoginClick(object sender, EventArgs e)
        {
            Login();
        }

        private void TextFieldUsernameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Select();
            }
        }

        private void TextFieldPasswordKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Username must be informed.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password must be informed.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loginDelegate.DynamicInvoke(new object[] { txtUsername.Text, txtPassword.Text });
        }
    }
}
