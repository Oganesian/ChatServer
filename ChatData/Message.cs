using System;

namespace ChatData
{
    [Serializable()]
    public class Message
    {
        public DateTime Timestamp { get; set; }
        //public Client Sender { get; set; }
        //public Client Receiver { get; set; }
        public int SenderUniqueId { get; set; }
        public int ReceiverUniqueId { get; set; }
        public MessageStatus Status { get; set; }
        public MessageType Type { get; set; }

        public string EncryptedMessageString;
    }
}
