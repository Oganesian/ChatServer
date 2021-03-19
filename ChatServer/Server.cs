using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    public class Server
    {
        private const int PORT = 8888;
        private const string SERVER_IP = "127.0.0.1";

        private List<Account> connectedAccounts = new List<Account>();
        private TcpListener listener;

        public void Listen()
        {
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            listener = new TcpListener(localAdd, PORT);
            Console.WriteLine("Listening...");
            listener.Start();

            while (true)
            {
                TcpClient tcpClient = listener.AcceptTcpClient();

                // var client = new Client(tcpClient);

                NetworkStream nwStream = tcpClient.GetStream();
                object received = JsonSerializerProvider.DeserializeBinary(nwStream);

                if (received is Account account)
                {
                    Console.WriteLine("A new client connected");
                    Console.WriteLine("Received: {0}#{1}", account.Username, account.PublicId);
                    //connectedClients.RemoveAll(x => x.tcpClient == client.tcpClient);
                    account.Client.tcpClient = tcpClient;
                    AddConnection(account);
                    new Thread(() => ListenToClient(account)).Start();
                }
            }
        }

        private void ListenToClient(Account account)
        {
            NetworkStream nwStream = account.Client.tcpClient.GetStream();

            while (true)
            {
                try
                {
                    InterpretIncomingMessage(nwStream);
                }
                catch
                {
                    RemoveConnection(account.Id);
                    Console.WriteLine("Client disconnected");
                    break;
                }
            }
        }

        private void InterpretIncomingMessage(NetworkStream nwStream)
        {
            object received = JsonSerializerProvider.DeserializeBinary(nwStream);

            if (received is Message message)
            {
                Console.WriteLine("Message: to {0}, {1}, {2}", message.ReceiverUniqueId, message.Timestamp, message.EncryptedMessageString);
                try
                {
                    var receiver = connectedAccounts.FirstOrDefault(x => x.Id == message.ReceiverUniqueId);
                    if (receiver != null)
                    {
                        var receiverNwStream = receiver.Client.tcpClient.GetStream();
                        // Writes serialized binary data into the stream
                        JsonSerializerProvider.SerializeBinary(receiverNwStream, message); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:" + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Received: " + received);
            }
        }

        protected internal void AddConnection(Account clientObject)
        {
            connectedAccounts.Add(clientObject);
        }

        protected internal void RemoveConnection(int uniqueId)
        {
            Account client = connectedAccounts.FirstOrDefault(c => c.Id == uniqueId);

            if (client != null)
                connectedAccounts.Remove(client);
        }

        public void Disconnect()
        {
            listener.Stop();

            foreach (var account in connectedAccounts)
            {
                account.Client.Close();
            }

            Environment.Exit(0);
        }
    }
}