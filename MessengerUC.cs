using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;

namespace Messenger
{
    public partial class MessengerUC : UserControl
    {
        private ArtalkXmppClient client;

        private readonly Delegate exit;

        public MessengerUC(ArtalkXmppClient client, Delegate exit)
        {
            InitializeComponent();

            this.exit = exit;
            SetClient(client);

            rtbMessageField.Text = "Bem vindo!\n\n";
            Focus();
            Select();
            txtMessage.Select();
        }

        public void SetClient(ArtalkXmppClient client)
        {
            if (client == null) return;
            this.client = client;
            this.client.Message += OnNewMessage;
        }

        private void SendMessageClick(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (txtMessage.Text == "") return;
            if (!client.Connected) return;
            //MessageBox.Show(client.Jid.ToString());
            client.SendMessage("chat@conference.desktop-22frc2k.dlink", client.Jid.ToString().Split("@")[0] + ":" + txtMessage.Text);
            txtMessage.Text = "";
        }

        void OnNewMessage(object? sender, MessageEventArgs e)
        {
            var body = e.Message.Body.Trim();
            var user = body.Split(':')[0];
            user = char.ToUpper(user[0]) + user[1..];
            var message = body[(user.Length + 1)..];
            AddMessage(user, message);
        }

        private void AddMessage(string user, string message)
        {
            if (rtbMessageField.InvokeRequired)
            {
                rtbMessageField.Invoke((MethodInvoker)delegate { rtbMessageField.Text += user + ": " + message + Environment.NewLine; });
            }
            else
            {
                rtbMessageField.Text += user + ": " + message + Environment.NewLine;
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            exit.DynamicInvoke();
        }

        private void AboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Carlos Machado", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TextFieldMessageKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }
    }
}
