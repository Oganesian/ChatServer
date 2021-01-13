﻿using ChatClient.Data;
using ChatClient.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;

namespace ChatClient.ClientConnection
{
    [Serializable()]
    public class Client
    {
        //private static Client instance;
        //public static Client GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new Client();
        //    }
        //    return instance;
        //}

        public Client() // TODO: clean up after debug
        {
            Id = 1234;
            Username = "Test";
            Connect();
        }

        public int UniqueId { get; set; }
        public int Id { get; set; } // TODO: private set?
        public string Username { get; set; }
        public List<Chat> Chats { get; set; }

        #region Connection
        [NonSerialized()] private NetworkStream stream;
        [NonSerialized()] public TcpClient tcpClient;
        [NonSerialized()] private const int PORT_NO = 8888;
        [NonSerialized()] private const string SERVER_IP = "127.0.0.1";
        #endregion

        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        public void Connect()
        {
            try
            {
                tcpClient = new TcpClient(SERVER_IP, PORT_NO);
                stream = tcpClient.GetStream();
            }
            catch (Exception) // TODO: clean up
            {
                //MessageBox.Show("Server is not available");
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

        public void SendMessage(object data)
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected && stream.CanWrite)
                {
                    JsonSerializerProvider.SerializeBinary(stream, data); // Writes serialized binary data into the stream
                    
                    //stream.Write(bytesToSend, 0, bytesToSend.Length);
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

            if (tcpClient != null)
                tcpClient.Close();
        }
    }
}
