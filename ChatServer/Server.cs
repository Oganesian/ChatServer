using ChatClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    public class Server
    {
        private const int PORT_NO = 8888;
        private const string SERVER_IP = "127.0.0.1";

        List<Client> clients = new List<Client>();

        TcpListener listener;

        public void Listen()
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();
            //---incoming client connected---


            while (true)
            {
                TcpClient tcpClient = listener.AcceptTcpClient();

                var client = new Client(tcpClient);
                AddConnection(client);

                var newClientThread = new Thread(() => ListenToClient(client, tcpClient));
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

        private void ListenToClient(Client client, TcpClient tcpClient)
        {
            NetworkStream nwStream = tcpClient.GetStream();

            while (true)
            {
                try
                {
                    byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

                    //---read incoming stream---
                    int bytesRead = nwStream.Read(buffer, 0, tcpClient.ReceiveBufferSize);

                    //---convert the data received into a string---
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received : " + dataReceived);
                }
                catch
                {
                    RemoveConnection(client.Id);
                }
            }
        }


        protected internal void AddConnection(Client clientObject)
        {
            clients.Add(clientObject);
        }

        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            Client client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}