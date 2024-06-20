using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using ChatAPI.Models;
using System.Linq;

namespace ChatClient.ViewModels;
internal class ChatsListViewModel : ViewModelBase, IChatsListViewModel, IDisposable
{
    IChatService _chatService;
    IUIService _uiService;

    User _user;

    IChatListItemViewModel _selectedChat;
    ObservableCollection<IChatListItemViewModel> _chats = new ObservableCollection<IChatListItemViewModel>();

    public ChatsListViewModel(IChatService chatService, IUIService uiService) 
    {
        _uiService = uiService;
        _chatService = chatService;
        _chatService.UserInitialized += _chatService_UserInitialized;
        _chatService.ChatRenamed += _chatService_ChatRenamed;
    }

    private void _chatService_ChatRenamed(object? sender, Chat chat)
    {
        var chatItemVm = _chats.FirstOrDefault(c => c.Chat.Id == chat.Id);
        
        if(chatItemVm != null)
            chatItemVm.Name = chat.Name;
    }

    private void _chatService_UserInitialized(object? sender, User user)
    {
        _user = user;
        
        _chats.Clear();

        foreach (var chat in user.Chats)
            _chats.Add(new ChatListItemViewModel(chat));

        SelectedChat = _chats[0];
    }

    public ObservableCollection<IChatListItemViewModel> Chats
    { 
        get => _chats; 
        set => this.RaiseAndSetIfChanged(ref _chats, value); 
    }

    public IChatListItemViewModel SelectedChat 
    {
        get => _selectedChat;
        set 
        {
            if (_selectedChat == value)
                return;

            _selectedChat = value;
            _uiService.OnSelectedChatChanged(_selectedChat.Chat);
            this.RaisePropertyChanged();
        }
    }

    public void Dispose()
    {
        _chatService.UserInitialized -= _chatService_UserInitialized;
    }
}
