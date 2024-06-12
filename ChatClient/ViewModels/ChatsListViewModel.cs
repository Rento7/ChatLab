using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels;
internal class ChatsListViewModel : Abstract.ViewModelBase
{
    ObservableCollection<ChatViewModel> _chats;

    public virtual ObservableCollection<ChatViewModel> Chats 
    { 
        get => _chats; 
        set => this.RaiseAndSetIfChanged(ref _chats, value); 
    }
}
