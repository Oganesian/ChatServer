using ChatData;
using CryptographyServices.KeyExchangeServices;
using CryptographyServices.EncryptionServices;
using CryptographyServices.DecryptionServices;
using Serialization;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace AccountAndConnection
{
    [Serializable]
    public class Account : BaseAccount
    {
        [NonSerialized]
        public byte[] publicKey;

        [NonSerialized]
        private byte[] privateKey;

        [NonSerialized]
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;

        [NonSerialized]
        private readonly IDiffieHellmanEncryptionService _messageEncryptionService;

        [NonSerialized]
        private readonly IDiffieHellmanDecryptionService _messageDecryptionService;

        public List<Chat> Chats { get; private set; }

        public Func<Message, Task> MessageReceivedCallback
        {
            get => messageReceivedCallback;
            set => messageReceivedCallback = value;
        }

        public Func<FriendRequest, Task> FriendRequestReceivedCallback
        {
            get => friendRequestReceivedCallback;
            set => friendRequestReceivedCallback = value;
        }

        public Func<FriendResponse, Task> FriendResponseReceivedCallback
        {
            get => friendResponseReceivedCallback;
            set => friendResponseReceivedCallback = value;
        }


        public Account(IDiffieHellmanKeyExchangeService keyExchangeService, IDiffieHellmanEncryptionService messageEncryptionService, IDiffieHellmanDecryptionService messageDecryptionService)
        {
            _keyExchangeService = keyExchangeService;
            InitializeAccount();
            _messageEncryptionService = messageEncryptionService;
            _messageDecryptionService = messageDecryptionService;
        }

        public Account(IDiffieHellmanKeyExchangeService keyExchangeService, BaseAccount account, IDiffieHellmanEncryptionService messageEncryptionService, IDiffieHellmanDecryptionService messageDecryptionService)
        {
            Id = account.Id;
            Email = account.Email;
            PasswordHash = account.PasswordHash;
            PublicId = account.PublicId;
            Username = account.Username;

            _keyExchangeService = keyExchangeService;
            InitializeAccount();
            _messageEncryptionService = messageEncryptionService;
            _messageDecryptionService = messageDecryptionService;
        }

        private void InitializeAccount()
        {
            // For new connections
            var keysTuple = _keyExchangeService.CreateKeyPair();
            publicKey = keysTuple.publicKey;
            privateKey = keysTuple.privateKey;

            Chats = new List<Chat>();
            Connect();
            LoadChats();
        }

        public void LoadChats()
        {
            Chats = JsonSerializerProvider.DeserializeChats(Username, PublicId);
        }

        #region Connection
        [NonSerialized]
        private Func<Message, Task> messageReceivedCallback;
        private Func<FriendRequest, Task> friendRequestReceivedCallback;
        private Func<FriendResponse, Task> friendResponseReceivedCallback;

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
                //MessageBox.Show("Server is not available: " + e.Message);
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
                            var targetChat = Chats.Find(x => x.receiverUniqueId == message.SenderUniqueId);
                            message.MessageString = _messageDecryptionService.Decrypt(message.EncryptedMessage, targetChat.sharedPrivateKey, message.IV);

                            Task.Run(() => MessageReceivedCallback(message));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception:" + e.Message);
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
                }
                catch
                {
                    Console.WriteLine("Server is not available");
                    break;
                }
            }
        }

        private void HandleFriendResponse(FriendResponse response)
        {
            try
            {
                Chat newChat = new Chat()
                {
                    senderId = PublicId.ToString(),
                    senderUniqueId = Id,
                    senderUsername = Username,
                    receiverId = response.Request.TargetPublicId.ToString(),
                    receiverUniqueId = response.Request.TargetId,
                    receiverUsername = response.Request.TargetUsername,
                    messages = new System.Collections.ObjectModel.ObservableCollection<Message>()
                };

                newChat.sharedPrivateKey = CreateSharedPrivateKey(response.SenderPublicKey);

                Chats.Add(newChat);

                //SendMessage(new Message()
                //{
                //    SenderUniqueId = Id,
                //    ReceiverUniqueId = response.Request.TargetId,
                //    Type = MessageType.OUTGOING,
                //    Timestamp = System.DateTime.Now,
                //    EncryptedMessageString = "First message."
                //});

                Task.Run(() => FriendResponseReceivedCallback(response));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }
        }

        private void HandleFriendRequest(FriendRequest request)
        {
            try
            {
                Task.Run(() => FriendRequestReceivedCallback(request));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }
        }

        public void SendMessage(object data)
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected && stream.CanWrite)
                {
                    if (data is Message message)
                    {
                        var targetChat = Chats.Find(x => x.receiverUniqueId == message.ReceiverUniqueId);
                        message.EncryptedMessage = _messageEncryptionService.Encrypt(message.MessageString, targetChat.sharedPrivateKey, out byte[] iv);
                        message.IV = iv;
                        message.MessageString = string.Empty;
                    }
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

        public bool SendFriendRequest(string newFriendUsername)
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected && stream.CanWrite)
                {
                    var targetUsernameSplitted = newFriendUsername.Split('#');
                    if(targetUsernameSplitted.Length == 2)
                    {
                        if(int.TryParse(targetUsernameSplitted[1], out int targetPublicId))
                        {
                            var targetUsername = targetUsernameSplitted[0];
                            var friendRequest = new FriendRequest(publicKey, Id, PublicId, Username, targetUsername, targetPublicId);

                            // Writes serialized binary data into the stream
                            JsonSerializerProvider.SerializeBinary(stream, friendRequest);
                            return true;
                        }

                    }

                }
            }
            return false;
        }

        public void SendFriendResponse(FriendResponse friendResponse)
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected && stream.CanWrite)
                {
                    JsonSerializerProvider.SerializeBinary(stream, friendResponse);
                }
            }
        }

        public byte[] CreateSharedPrivateKey(byte[] publicKey)
        {
            return _keyExchangeService.CreateSharedPrivateKey(privateKey, publicKey);
        }
    }
}
