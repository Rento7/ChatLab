using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using ReactiveUI;

namespace ChatClient.ViewModels;

internal class MainViewModel : ViewModelBase, IMainViewModel
{
    ChatsListViewModel _chatListViewModel;
    ChatViewModel _chatViewModel;

    public MainViewModel(IChatService chatService, IUIService uiService)
    {
        _chatListViewModel = uiService.GetViewModel<ChatsListViewModel>();
        _chatViewModel = uiService.GetViewModel<ChatViewModel>();
    }

    public IChatsListViewModel ChatsListViewModel => _chatListViewModel;

    public IChatViewModel SelectedChatViewModel 
    {
        get => _chatViewModel;
        set 
        {
            var val = value as ChatViewModel;
            this.RaiseAndSetIfChanged(ref _chatViewModel!, val); 
        }
    }
}