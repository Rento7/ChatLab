using System;
using ReactiveUI;
using ChatAPI.Models;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels
{
    internal class ChatListItemViewModel : ViewModelBase, IChatListItemViewModel
    {
        Chat _chat;

        public ChatListItemViewModel(Chat chat) 
        {
            ArgumentNullException.ThrowIfNull(chat);
            _chat = chat;
        }

        public Chat Chat => _chat;

        public string Name 
        {
            get 
            {
                if (string.IsNullOrEmpty(_chat.Name))
                    return "Chat";

                return _chat.Name;
            }
            set
            {
                if (Chat.Name == value)
                    return;

                //TODO change
                _chat.Name = value;

                this.RaisePropertyChanged();
            }
        }
    }
}
