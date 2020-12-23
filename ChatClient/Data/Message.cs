using ChatClient.ClientConnection;

namespace ChatClient.Data
{
    public class Message
    {
        public double Timestamp { get; private set; }
        public Client Sender { get; private set; }
        public Client Receiver { get; private set; }
        public MessageStatus Status { get; set; }
        public MessageType Type { get; set; }

        private string MessageText;
    }
}
