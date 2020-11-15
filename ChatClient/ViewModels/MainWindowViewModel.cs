using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        const int PORT_NO = 8888;
        const string SERVER_IP = "127.0.0.1";
        private TcpClient client;
        NetworkStream nwStream;

        #region Commands
        private ICommand _connectToServer;
        private ICommand _sendMessage;
        #endregion

        public MainWindowViewModel()
        {
            ConectToServerExec();
        }

        #region Commands Getters
        public ICommand ConnectToServer
        {
            get
            {
                if (_connectToServer == null)
                {
                    _connectToServer = new CommandHandler(() => ConectToServerExec(), () => CanExecute);
                }
                return _connectToServer;
            }
        }

        public ICommand SendMessage
        {
            get
            {
                if (_sendMessage == null)
                {
                    _sendMessage = new CommandHandler(() => SendMessageExec(), () => CanExecute);
                }
                return _sendMessage;
            }
        }
        
        #endregion

        private void ConectToServerExec()
        {
            try
            {
                client = new TcpClient(SERVER_IP, PORT_NO);
                nwStream = client.GetStream();
            }
            catch (Exception)
            {
                MessageBox.Show("Server is not available");
            }            

            //---data to send to the server---
            //string textToSend = "Hello World";

            //---create a TCPClient object at the IP and port no.---

            
            //byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //---send the text---
            //Debug.WriteLine("Sending : " + textToSend);
            //nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            ////---read back the text---
            //byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            //Debug.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));            
           // client.Close();
        }

        private void SendMessageExec()
        {
            if(client != null)
            {
                if (client.Connected && nwStream.CanWrite)
                {
                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes("moin");

                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                }
            }
            //byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
         //   client.Close();
        }

    }
}