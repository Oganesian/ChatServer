using AccountAndConnection;
using ChatData;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class FreindRequestReceivedDialogViewModel : BindableBase
    {
        private string _mainText;

        public string MainText
        {
            get => _mainText;
            set => SetProperty(ref _mainText, value);
        }

        private ICommand acceptFriendRequest;
        private ICommand cancelFriendRequest;

        private readonly Account account;
        private readonly FriendRequest friendRequest;

        public FreindRequestReceivedDialogViewModel(FriendRequest request, Account account)
        {
            this.friendRequest = request;
            this.account = account;

            MainText = string.Format("{0}#{1} wants to be your friend", request.SenderUsername, request.SenderPublicId);
        }

        public ICommand AcceptFriendRequest => acceptFriendRequest ??= new CommandHandler(() => PerformAcceptFriendRequest(), () => CanExecute);

        
        public ICommand CancelFriendRequest => cancelFriendRequest ??= new CommandHandler(() => PerformCancelFriendRequest(), () => CanExecute);

        private void PerformAcceptFriendRequest()
        {
            SendFriendResponse(true, account.publicKey);
            Chat newChat = new Chat()
            {
                senderId = account.PublicId.ToString(),
                senderUniqueId = account.Id,
                senderUsername = account.Username,
                receiverId = friendRequest.SenderPublicId.ToString(),
                receiverUniqueId = friendRequest.SenderId,
                receiverUsername = friendRequest.SenderUsername,
                messages = new System.Collections.ObjectModel.ObservableCollection<Message>()
            };

            newChat.sharedPrivateKey = account.CreateSharedPrivateKey(friendRequest.senderPublicKey);

            account.Chats.Add(newChat);
            //account.SendMessage(new Message()
            //{
            //    SenderUniqueId = account.Id,
            //    ReceiverUniqueId = friendRequest.SenderId,
            //    Type = MessageType.OUTGOING,
            //    Timestamp = System.DateTime.Now,
            //    EncryptedMessageString = "Accepted the friend request."
            //});

            var mwvm = MainWindowViewModel.GetInstance();
            mwvm.DisplayChats();
            mwvm.CloseDialog();
        }

        private void PerformCancelFriendRequest()
        {
            SendFriendResponse(false, null);
        }

        private void SendFriendResponse(bool positive, byte[] publicKey)
        {
            FriendResponse response = new FriendResponse(friendRequest, positive, publicKey);
            account.SendFriendResponse(response);
        }
    }
}
