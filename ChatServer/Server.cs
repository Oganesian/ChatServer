using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace ChatServer
{
    public class Server
    {
        private const int PORT = 8888;
        private const string SERVER_IP = "127.0.0.1";

        private List<Client> connectedClients = new List<Client>();
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

                var client = new Client(tcpClient);
                
                AddConnection(client);
                Console.WriteLine("A new client connected");
                //Console.WriteLine("ClientInfos: {0}#{1}", client.Username, client.Id);

                var newClientThread = new Thread(() => ListenToClient(client));
                newClientThread.Start();

                //---get the incoming data through a network stream---
                //---write back the text to the client---
                //Console.WriteLine("Sending back : " + dataReceived);
                //nwStream.Write(buffer, 0, bytesRead);
                //client.Close();
                //listener.Stop();
                //Console.ReadLine();
            }
        }

        private void ListenToClient(Client client)
        {
            NetworkStream nwStream = client.tcpClient.GetStream();

            while (true)
            {
                try
                {
                    //byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

                    ////---read incoming stream---
                    //int bytesRead = nwStream.Read(buffer, 0, tcpClient.ReceiveBufferSize);

                    ////---convert the data received into a string---
                    //string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    InterpretIncomingMessage(nwStream);
                }
                catch
                {
                    RemoveConnection(client.UniqueId);
                    Console.WriteLine("Client disconnected");
                    break;
                }
            }
        }

        private void InterpretIncomingMessage(NetworkStream nwStream)
        {
            object received = JsonSerializerProvider.DeserializeBinary(nwStream);

            if (received is Client client)
            {
                Console.WriteLine("Received: {0}#{1}", client.Username, client.Id);
            }
            else if (received is Message message)
            {
                Console.WriteLine("Message: to {0}, {1}, {2}", message.ReceiverUniqueId, message.Timestamp, message.EncryptedMessageString);
            }
            else
            {
                Console.WriteLine("Received: " + received);
            }
        }

        protected internal void AddConnection(Client clientObject)
        {
            connectedClients.Add(clientObject);
        }

        protected internal void RemoveConnection(int id)
        {
            Client client = connectedClients.FirstOrDefault(c => c.Id == id);

            if (client != null)
                connectedClients.Remove(client);
        }

        public void Disconnect()
        {
            listener.Stop();

            foreach (var client in connectedClients)
            {
                client.Close();
            }

            Environment.Exit(0);
        }
    }
}