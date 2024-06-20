using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using ChatAPI.Models;

namespace ChatClient.ViewModels;

internal class ChatViewModel : ViewModelBase, IChatViewModel, IDisposable
{
    IChatService _chatService;
    IUIService _uiService;

    User _user;
    Chat _chat;

    string _name = string.Empty;
    string _messageText = string.Empty;
    ObservableCollection<IMessageItemViewModel> _messages;
    ReactiveCommand<Unit, Unit> _sendMessageCommand;
    ReactiveCommand<Unit, Unit> _renameChatCommand;

    public ChatViewModel(IChatService chatService, IUIService uiService)
    {
        _chatService = chatService;
        _uiService = uiService;

        _messages = new ObservableCollection<IMessageItemViewModel>();

        IObservable<bool> isMessageInputValid = this.WhenAnyValue(
            vm => vm.MessageText,
            msg => !string.IsNullOrWhiteSpace(msg) && _chat != null
            );

        _sendMessageCommand = ReactiveCommand.Create(() =>
        {
            var message = new Message()
            {
                ChatId = _chat.Id,
                SenderId = _user.Id,
                Text = MessageText
            };

            _chatService.SendMessage(message);
            MessageText = string.Empty;

        }, isMessageInputValid);


        _renameChatCommand = ReactiveCommand.Create(() =>
        {
            if (_chat == null)
                return;

            _chatService.RenameChat(_chat.Id, _name);
        });

        _chatService.UserInitialized += _chatService_UserInitialized;
        _chatService.MessageReceived += _chatService_MessageReceived;
        _chatService.ChatRenamed += _chatService_ChatRenamed; 
        _uiService.SelectedChatChanged += _uiService_SelectedChatChanged;
    }

    private void _chatService_ChatRenamed(object? sender, Chat chat)
    {
        if (_chat.Id == chat.Id) 
        {
            _chat = chat;
            this.RaisePropertyChanged(nameof(Name));
        }
    }

    private void _chatService_UserInitialized(object? sender, User user)
    {
        _user = user;
    }

    private void _uiService_SelectedChatChanged(object? sender, Chat chat)
    {
        _chat = chat;

        if (_user == null)
            _user = _chatService.CurrentUser;

        if (_user == null)
            return;

        _name = chat.Name;
        this.RaisePropertyChanged(nameof(Name));

        _messages.Clear();

        foreach (var message in chat.Messages)
            _messages.Add(new MessageItemViewModel(message, _user.Id == message.SenderId));

        
    }

    private void _chatService_MessageReceived(object? sender, Message message)
    {
        _messages.Add(new MessageItemViewModel(message, _user.Id == message.SenderId));
    }

    public ObservableCollection<IMessageItemViewModel> Messages
    {
        get => _messages;
        set => this.RaiseAndSetIfChanged(ref _messages, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string MessageText
    {
        get => _messageText;
        set => this.RaiseAndSetIfChanged(ref _messageText, value);
    }

    public IReactiveCommand SendMessageCommand => _sendMessageCommand;
    public IReactiveCommand RenameChatCommand => _renameChatCommand;

    public void Dispose()
    {
        _chatService.MessageReceived -= _chatService_MessageReceived;
    }
}
