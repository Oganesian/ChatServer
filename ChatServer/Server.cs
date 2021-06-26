using AccountAndConnection;
using ChatData;
using Serialization;
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

        private readonly List<Account> connectedAccounts = new List<Account>();
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
                    account.tcpClient = tcpClient;
                    AddConnection(account);
                    new Thread(() => ListenToClient(account)).Start();
                }
            }
        }

        private void ListenToClient(Account account)
        {
            NetworkStream nwStream = account.tcpClient.GetStream();

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
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("Message from {0} to {1}, {2}: {3}", message.SenderUniqueId, message.ReceiverUniqueId, message.Timestamp, 
                    System.Text.Encoding.UTF8.GetString(message.EncryptedMessage));
                try
                {
                    var receiver = connectedAccounts.FirstOrDefault(x => x.Id == message.ReceiverUniqueId);
                    if (receiver != null)
                    {
                        var receiverNwStream = receiver.tcpClient.GetStream();
                        // Writes serialized binary data into the stream
                        JsonSerializerProvider.SerializeBinary(receiverNwStream, message); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            else if(received is FriendRequest request)
            {
                HandleFriendRequest(request);
            }
            else if(received is FriendResponse response)
            {
                HandleFriendResponse(response);
            }
            else
            {
                Console.WriteLine("Received: " + received);
            }
        }

        private void HandleFriendResponse(FriendResponse response)
        {
            try
            {
                var targetAccount = connectedAccounts.Find(x => x.Username == response.Request.SenderUsername && x.PublicId == response.Request.SenderPublicId);
                if (targetAccount != null)
                {
                    var receiverNwStream = targetAccount.tcpClient.GetStream();
                    JsonSerializerProvider.SerializeBinary(receiverNwStream, response);
                    Console.WriteLine("Friend Response from {0}#{1} to {2}#{3}, positive: {4} | public key: {5}", 
                        response.Request.TargetUsername, response.Request.TargetPublicId, response.Request.SenderUsername, 
                        response.Request.SenderPublicId, response.PositiveResponse, System.Text.Encoding.UTF8.GetString(response.SenderPublicKey));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void HandleFriendRequest(FriendRequest request)
        {
            try
            {
                var targetAccount = connectedAccounts.Find(x => x.Username == request.TargetUsername && x.PublicId == request.TargetPublicId);
                if (targetAccount != null)
                {
                    request.TargetId = targetAccount.Id;
                    var receiverNwStream = targetAccount.tcpClient.GetStream();
                    JsonSerializerProvider.SerializeBinary(receiverNwStream, request);
                    Console.WriteLine("Friend Request from {0}#{1} to {2}#{3}  | public key: {4}", request.SenderUsername, request.SenderPublicId, 
                        request.TargetUsername, request.TargetPublicId, System.Text.Encoding.UTF8.GetString(request.senderPublicKey));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
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
                account.Close();
            }

            Environment.Exit(0);
        }
    }
}