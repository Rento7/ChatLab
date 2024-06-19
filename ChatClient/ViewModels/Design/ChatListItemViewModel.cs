using ReactiveUI;
using ChatAPI.Models;
using ChatClient.ViewModels.Abstract;
 
namespace ChatClient.ViewModels.Design
{
    internal class ChatListItemViewModel : ViewModelBase, IChatListItemViewModel
    {
        string _chat = "chat";
        public Chat Chat => null;

        public string Name 
        { 
            get => _chat;
            set => this.RaiseAndSetIfChanged(ref _chat, value);
        }
    }
}
