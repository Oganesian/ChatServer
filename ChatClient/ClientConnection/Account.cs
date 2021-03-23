﻿using ChatClient.Data;
using ChatClient.Models;
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
    public class Account : BaseAccount
    {
        public List<Chat> Chats { get; private set; }

        public Func<Message, Task> MessageReceivedCallback
        {
            get => messageReceivedCallback;
            set => messageReceivedCallback = value;
        }
        //[NonSerialized]
        //public TcpClientContainer Client { get; private set; }

        public Account()
        {
            Chats = new List<Chat>();

            Connect();
            LoadChats();
        }

        public Account(BaseAccount account)
        {
            Id = account.Id;
            Email = account.Email;
            PasswordHash = account.PasswordHash;
            PublicId = account.PublicId;
            Username = account.Username;

            Chats = new List<Chat>();

            Connect();
            LoadChats();
        }

        public void LoadChats()
        {
            Chats = JsonSerializerProvider.DeserializeChats(Username, PublicId);
        }

        //public void SendMessage(Message message)
        //{
        //    Client.SendMessage(message);
        //}

        //public TcpClientContainer()
        //{
        //    Connect();
        //}

        #region Connection
        [NonSerialized]
        private Func<Message, Task> messageReceivedCallback;
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
                            MessageReceivedCallback.Invoke(message);
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
