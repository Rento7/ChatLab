using ReactiveUI;
using System.Collections.ObjectModel;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels.Design;
public class ChatsListViewModel : ViewModelBase, IChatsListViewModel
{
    ObservableCollection<IChatListItemViewModel> _fakeChats = new()
    {
        new ChatListItemViewModel () { Name = "User chat 11111"},
        new ChatListItemViewModel () { Name = "User chat 22222"},
        new ChatListItemViewModel () { Name = "User chat 33333"},
    };

    public ObservableCollection<IChatListItemViewModel> Chats 
    { 
        get => _fakeChats;
        set => this.RaiseAndSetIfChanged(ref _fakeChats, value); 
    }
    public IChatListItemViewModel SelectedChat 
    { 
        get => _fakeChats[0]; 
        set => throw new System.NotImplementedException(); 
    }

    public ChatsListViewModel() 
    {
    
    }
}
