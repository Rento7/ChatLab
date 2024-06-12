using ChatClient.ViewModels.Abstract;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Design;
internal class ChatsListViewModel : ViewModelBase, IChatsListViewModel
{
    ObservableCollection<IChatViewModel> _fakeChats = new()
    {
        new ChatViewModel() { Name = "User chat 11111"},
        new ChatViewModel() { Name = "User chat 22222"},
        new ChatViewModel() { Name = "User chat 33333"},
    };

    public ObservableCollection<IChatViewModel> Chats 
    { 
        get => _fakeChats;
        set => this.RaiseAndSetIfChanged(ref _fakeChats, value); 
    } 
}
