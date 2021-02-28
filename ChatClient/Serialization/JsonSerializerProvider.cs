using ChatClient.ClientConnection;
using ChatClient.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChatClient.Serialization
{
    public class JsonSerializerProvider
    {
        private static readonly string CHATS_DIR = "Chats/";

        public static void SerializeClient(Client client)
        {
            string fullPath = CHATS_DIR + client.Username + client.Id;
            string jsonString = JsonConvert.SerializeObject(client, Formatting.Indented);

            if (!Directory.Exists(CHATS_DIR))
                Directory.CreateDirectory(CHATS_DIR);

            File.WriteAllText(fullPath, jsonString);
        }

        public static void SerializeChat(Client client, Chat chat)
        {
            string fullPath = CHATS_DIR + client.Username + client.Id + "/";
            string jsonString = JsonConvert.SerializeObject(chat, Formatting.Indented);

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            File.WriteAllText(fullPath + chat.receiverUsername + chat.receiverId + chat.receiverUniqueId, jsonString);
        }

        //public static List<Chat> DeserializeClientChats(Client client)
        //{
        //    string fullPath = CHATS_DIR + client.Username + client.Id;
        //    string jsonString = File.ReadAllText(fullPath);
        //    return JsonConvert.DeserializeObject<Client>(jsonString).Chats;
        //}

        public static List<Chat> DeserializeClientChats(Client client)
        {
            List<Chat> chats = new List<Chat>();
            string fullPath = CHATS_DIR + client.Username + client.Id + "/";
            foreach (var file in Directory.GetFiles(fullPath))
            {
                string jsonString = File.ReadAllText(file);
                chats.Add(JsonConvert.DeserializeObject<Chat>(jsonString));
            }
            return chats;
        }

        public static void SerializeBinary(Stream stream, object data)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }

        public static object DeserializeBinary(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
