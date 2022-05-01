using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;

namespace Messenger
{
    public partial class MessengerUC : UserControl
    {
        private ArtalkXmppClient client;
        private Roster roster;
        private string name = null;

        private readonly Delegate exit;

        private string[] users = {
            "admin@desktop-22frc2k.dlink",
            "carlos@desktop-22frc2k.dlink"
        };

        public MessengerUC(ArtalkXmppClient client, Delegate exit)
        {
            InitializeComponent();

            this.exit = exit;
            SetClient(client);

            Focus();
            Select();
            txtMessage.Select();

            foreach (var user in users)
            {
                cbUsers.Items.Add(user);
            }
            cbUsers.SelectedItem = users[0];

            tsmiOnline.Checked = false;
            tsmiOffiline.Checked = true;
        }

        public void SetClient(ArtalkXmppClient client)
        {
            if (client == null) return;
            this.client = client;
            this.client.Message += OnNewMessage;
            this.client.StatusChanged += Client_StatusChanged;
            SetName();
        }

        private void Client_StatusChanged(object? sender, StatusEventArgs e)
        {
            if (e.Status.Availability == Availability.Online)
            {
                tsslStatus.Text = "Status: Online";
                tsmiOffiline.Checked = false;
                tsmiOnline.Checked = true;
            }
            else
            {
                tsslStatus.Text = "Status: Offline";
                tsmiOffiline.Checked = true;
                tsmiOnline.Checked = false;
            }
        }

        private void SetName()
        {
            if (name == null)
            {
                name = client.Username;
                name = char.ToUpper(name[0]) + name[1..];

                tsslName.Text = "Name: " + name;
            }
        }

        private void SendMessageClick(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (txtMessage.Text == "") return;
            if (!client.Connected) return;
            SetName();
            client.SendMessage(cbUsers.Text, name + ":" + txtMessage.Text);
            AddMessage(name, txtMessage.Text);
            txtMessage.Text = "";
        }

        private void SetStatus(Availability status)
        {
            if (client.Connected)
            {
                try
                {
                    client.SetStatus(status);
                }
                catch(Exception e)
                {

                }
            }
        }

        void OnNewMessage(object? sender, MessageEventArgs e)
        {
            var body = e.Message.Body.Trim();
            var user = body.Split(':')[0];
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

        private void SetStatusOnlineClick(object sender, EventArgs e)
        {
            SetStatus(Availability.Online);
            tsmiOffiline.Checked = true;
            tsmiOnline.Checked = false;
        }

        private void SetStatusOffilineClick(object sender, EventArgs e)
        {
            SetStatus(Availability.Offline);
            tsmiOffiline.Checked = false;
            tsmiOnline.Checked = true;
        }
    }
}
