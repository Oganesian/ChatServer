using ChatClient.Data;
using ChatClient.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.ClientConnection
{
    [Serializable]
    public class TcpClientContainer // TODO: delete
    {

        public TcpClientContainer()
        {
            Connect();
        }

        public Func<Message, Task> MessageReceived { get; set; } // TODO: check safety

        #region Connection
        [NonSerialized]
        private NetworkStream stream;

        [NonSerialized]
        public TcpClient tcpClient;

        [NonSerialized]
        private const int PORT_NO = 8888;

        [NonSerialized]
        private const string SERVER_IP = "127.0.0.1";
        #endregion

        public void Connect()
        {
            try
            {
                tcpClient = new TcpClient(SERVER_IP, PORT_NO);
                stream = tcpClient.GetStream();
                JsonSerializerProvider.SerializeBinary(stream, this);
                new Thread(() => ListenToServer()).Start();
            }
            catch (Exception e) // TODO: clean up
            {
                MessageBox.Show("Server is not available: " + e.Message);
            }
        }

        private void ListenToServer()
        {
            NetworkStream nwStream = tcpClient.GetStream();

            while (true)
            {
                try
                {
                    object received = JsonSerializerProvider.DeserializeBinary(nwStream);

                    if (received is Message message)
                    {
                        try
                        {
                            MessageReceived.Invoke(message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception:" + e.Message);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Server is not available");
                    break;
                }
            }
        }

        public void SendMessage(object data)
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected && stream.CanWrite)
                {
                    // Writes serialized binary data into the stream
                    JsonSerializerProvider.SerializeBinary(stream, data); 
                }
            }
        }

        public void Close()
        {
            if (stream != null)
                stream.Close();

            if (tcpClient != null)
                tcpClient.Close();
        }
    }
}
