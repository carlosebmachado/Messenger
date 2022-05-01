using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;

namespace Messenger
{
    public partial class App : Form
    {
        private const string HOSTNAME = "192.168.0.117";

        private ArtalkXmppClient client;
        private Thread clientConnection;

        private readonly LoginUC loginUC;
        private readonly MessengerUC messengerUC;

        public App()
        {
            InitializeComponent();

            loginUC = new LoginUC(Login);
            messengerUC = new MessengerUC(client, Logout);

            SetUC(loginUC);
        }

        private void Login(string username, string password)
        {
            client = new ArtalkXmppClient(HOSTNAME, username, password, 5222, false);
            Connect();
            messengerUC.SetClient(client);
            SetUC(messengerUC);
        }

        private void Logout()
        {
            CloseSession();
        }

        private void SetUC(UserControl userControl)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserControl>(SetUC), new object[] { userControl });
            }
            else
            {
                Controls.Clear();
                userControl.Dock = DockStyle.Fill;
                Controls.Add(userControl);
            }
        }

        private void Connect()
        {
            clientConnection = new Thread(() =>
            {
                try
                {
                    client.Connect("app");
                }
                catch (Exception ex)
                {
                    var res = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.OK)
                    {
                        SetUC(loginUC);
                    }
                    else
                    {
                        Close();
                    }
                }
            });
            clientConnection.Start();
        }

        private void CloseSession()
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }
        }

        private void ApplicationLoad(object sender, EventArgs e)
        {
        }

        private void ApplicationFormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSession();
            //clientConnection.Join();
        }
    }
}