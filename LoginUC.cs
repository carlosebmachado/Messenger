using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger
{
    public partial class LoginUC : UserControl
    {
        private Delegate LoginClick;

        public LoginUC(Delegate callback)
        {
            InitializeComponent();

            LoginClick = callback;
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Username and password must be informed");
            }
            LoginClick.DynamicInvoke(new object[] { txtUsername, txtPassword });
        }
    }
}
