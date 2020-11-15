using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace ChatClient.ClientConnection
{
    public class Client
    {
        #region Singleton
        private static Client instance;
        public static Client GetInstance()
        {
            if (instance == null)
            {
                instance = new Client();
            }
            return instance;
        }

        private Client() 
        {
            Connect();
        }

        #endregion

        public string Id { get; private set; }
        public string Username { get; private set; }

        #region Connection
        private NetworkStream stream;
        private TcpClient client;
        const int PORT_NO = 8888;
        const string SERVER_IP = "127.0.0.1";
        #endregion

        public Client(TcpClient tcpClient)
        {
        }

        public void Connect()
        {
            try
            {
                client = new TcpClient(SERVER_IP, PORT_NO);
                stream = client.GetStream();
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

        public void SendMessage()
        {
            if (client != null)
            {
                if (client.Connected && stream.CanWrite)
                {
                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes("moin");

                    stream.Write(bytesToSend, 0, bytesToSend.Length);
                }
            }
            //byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            //   client.Close();
        }

        public void Close()
        {
            if (stream != null)
                stream.Close();

            if (client != null)
                client.Close();
        }
    }
}
