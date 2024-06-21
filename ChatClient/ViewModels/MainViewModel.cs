using ChatClient.Services;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class MainViewModel : ViewModelBase, IMainViewModel
{
    IChatService _chatService;
    IUIService _uiService;

    ChatPageViewModel _chatPageViewModel;

    public MainViewModel(IChatService chatService, IUIService uiService)
    {
        _chatService = chatService;
        _uiService = uiService;
        _chatPageViewModel = uiService.GetViewModel<ChatPageViewModel>();
    }

    public IChatPageViewModel ChatPageViewModel => _chatPageViewModel;
}