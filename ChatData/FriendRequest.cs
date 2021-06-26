using System;

namespace ChatData
{
    [Serializable]
    public class FriendRequest
    {
        public byte[] senderPublicKey;
        public int SenderId { get; private set; }
        public int SenderPublicId { get; private set; }
        public string SenderUsername { get; private set; }


        public string TargetUsername { get; private set; }
        public int TargetPublicId { get; private set; }
        public int TargetId { get; set; }

        public FriendRequest(byte[] senderPublicKey, int senderId, int senderPublicId, string senderUsername, string targetUsername, int targetPublicId)
        {
            this.senderPublicKey = senderPublicKey;
            SenderId = senderId;
            SenderPublicId = senderPublicId;
            SenderUsername = senderUsername;
            TargetUsername = targetUsername;
            TargetPublicId = targetPublicId;
        }
    }
}
