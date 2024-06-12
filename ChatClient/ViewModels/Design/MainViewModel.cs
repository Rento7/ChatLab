using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels.Design;

internal class MainViewModel : ViewModelBase, IMainViewModel 
{ 
    public IChatsListViewModel ChatsListViewModel => new Design.ChatsListViewModel();

    public IChatViewModel SelectedChatViewModel 
    {
        get => new Design.ChatViewModel();
        set { } 
    }
}
