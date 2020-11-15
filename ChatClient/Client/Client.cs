using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ChatClient.Client
{
    public class Client
    {
        public string Id { get; private set; }
        public string userName;
        private NetworkStream stream;
        private TcpClient client;

        public Client(TcpClient tcpClient)
        {
        }
    }
}
