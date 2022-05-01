using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;

namespace Messenger
{
    public partial class Application : Form
    {
        private const string HOSTNAME = "192.168.0.117";

        private ArtalkXmppClient Client;
        private Thread clientConnection;

        private LoginUC loginUC;

        public Application()
        {
            InitializeComponent();

            loginUC = new LoginUC(LoginClick);

            SetUC(loginUC);
        }

        private void LoginClick(string username, string password)
        {
            Client = new ArtalkXmppClient(HOSTNAME, username, password, 5222, false);
            Connect();
        }

        private void SetUC(UserControl userControl)
        {
            Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            Controls.Add(userControl);
        }

        private void ApplicationLoad(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CloseSession();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            if (!Client.Connected) return;
            AddMessage("System", Client.Jid.ToString());
            Client.SendMessage("chat@conference.desktop-22frc2k.dlink", textBox1.Text);
            textBox1.Text = "";
        }
        void OnNewMessage(object sender, MessageEventArgs e)
        {
            AddMessage(e.Jid.ToString(), e.Message.Body);
        }

        private void Connect()
        {
            Client.Message += OnNewMessage;
            Client.Tls = false;
            clientConnection = new Thread(() =>
            {
                try
                {
                    Client.Connect("");
                    MessageBox.Show("connected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            clientConnection.Start();
        }

        private void CloseSession()
        {
            if (Client != null && Client.Connected)
            {
                Client.Close();
            }
        }

        private void AddMessage(string user, string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.Text += user + ": " + message + Environment.NewLine; });
            }
            else
            {
                richTextBox1.Text += user + ": " + message + Environment.NewLine;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSession();
            //clientConnection.Join();
            //clientConnection.Interrupt();
            //Close();
        }
    }
}