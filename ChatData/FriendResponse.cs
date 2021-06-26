using System;

namespace ChatData
{
    [Serializable]
    public class FriendResponse
    {
        public FriendRequest Request { get; private set; }
        public bool PositiveResponse { get; private set; }
        public byte[] SenderPublicKey { get; private set; }

        public FriendResponse(FriendRequest request, bool positiveResponse, byte[] senderPublicKey)
        {
            Request = request;
            PositiveResponse = positiveResponse;
            SenderPublicKey = senderPublicKey;
        }
    }
}
