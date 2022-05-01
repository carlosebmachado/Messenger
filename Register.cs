//using Matrix;
//using Matrix.Xmpp.Client;
//using Matrix.Xmpp.Register;
//using Matrix.Xmpp.XData;

//namespace Messenger
//{
//    internal class Register
//    {
//        XmppClient xmppClient = new XmppClient();

//        public Register()
//        {
//            xmppClient.OnRegister += new EventHandler<Matrix.EventArgs>(xmppClient_OnRegister);
//            xmppClient.OnRegisterInformation += new EventHandler<RegisterEventArgs>(xmppClient_OnRegisterInformation);
//            xmppClient.OnRegisterError += new EventHandler<IqEventArgs>(xmppClient_OnRegisterError);

//            xmppClient.SetUsername("carlos");
//            xmppClient.SetXmppDomain("192.168.0.117");
//            xmppClient.Password = "admin";
//            xmppClient.RegisterNewAccount = true;

//            xmppClient.Open();
//        }

//        private void xmppClient_OnRegisterInformation(object? sender, RegisterEventArgs e)
//        {
//            e.Register.RemoveAll<Data>();

//            e.Register.Username = xmppClient.Username;
//            e.Register.Password = xmppClient.Password;
//        }

//        private void xmppClient_OnRegister(object? sender, EventArgs e)
//        {
//            // registration was successful
//            MessageBox.Show("sucesso");
//            xmppClient.Close();
//        }

//        private void xmppClient_OnRegisterError(object? sender, IqEventArgs e)
//        {
//            // registration failed.
//            MessageBox.Show("falha");
//            xmppClient.Close();
//        }

//    }
//}
