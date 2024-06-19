using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels;
internal class ChatsListViewModel : ViewModelBase, IChatsListViewModel
{
    IUIService _uiService;

    ObservableCollection<IChatViewModel> _chats = new ObservableCollection<IChatViewModel>();

    public ChatsListViewModel(IChatService chatService, IUIService uiService) 
    {
        _uiService = uiService;
    }

    public ObservableCollection<IChatViewModel> Chats 
    { 
        get => _chats; 
        set => this.RaiseAndSetIfChanged(ref _chats, value); 
    }
}
