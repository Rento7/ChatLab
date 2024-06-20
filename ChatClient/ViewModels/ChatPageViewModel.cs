using ReactiveUI;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class ChatPageViewModel : ViewModelBase, IChatPageViewModel
{
    IChatService _chatService;
    IUIService _uiService;

    ChatsListViewModel _chatListViewModel;
    ChatViewModel _chatViewModel;

    public ChatPageViewModel(IChatService chatService, IUIService uiService)
    {
        _chatService = chatService;
        _uiService = uiService;
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
