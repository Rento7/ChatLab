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

    IMessageItemViewModel _selectedMessageVm;

    User _user;
    Chat _chat;

    string _name = string.Empty;
    string _messageText = string.Empty;
    ObservableCollection<IMessageItemViewModel> _messages;
    ReactiveCommand<Unit, Unit> _sendMessageCommand;
    ReactiveCommand<Unit, Unit> _renameChatCommand;
    ReactiveCommand<Unit, Unit> _startEditMessageCommand;
    ReactiveCommand<Unit, Unit> _deleteSelectedMessageCommand;
    ReactiveCommand<Unit, Unit> _deselectMessageCommand;

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


        _deselectMessageCommand = ReactiveCommand.Create(() =>
        {
            SelectedMessageVm = null!;
        });

        _chatService.UserInitialized += _chatService_UserInitialized;
        _chatService.MessageReceived += _chatService_MessageReceived;
        _chatService.ChatRenamed += _chatService_ChatRenamed;
        _chatService.ChatHasReselected += _chatService_ChatHasReselected;
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

    private void _chatService_ChatHasReselected(object? sender, Chat chat)
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
        if (_chat == null || message.ChatId != _chat.Id)
            return;

        _messages.Add(new MessageItemViewModel(message, _user.Id == message.SenderId));
    }

    public ObservableCollection<IMessageItemViewModel> Messages
    {
        get => _messages;
        set => this.RaiseAndSetIfChanged(ref _messages, value);
    }

    public IMessageItemViewModel SelectedMessageVm 
    {
        get => _selectedMessageVm;
        set 
        {
            this.RaiseAndSetIfChanged(ref _selectedMessageVm, value);
            this.RaisePropertyChanged(nameof(IsMessageSelected));
        }
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

    public bool IsMessageSelected => _selectedMessageVm != null; 

    public IReactiveCommand SendMessageCommand => _sendMessageCommand;
    public IReactiveCommand RenameChatCommand => _renameChatCommand;
    public IReactiveCommand StartEditMessageCommand => _startEditMessageCommand;

    public IReactiveCommand DeleteSelectedMessageCommand => _deleteSelectedMessageCommand;

    public IReactiveCommand DeselectMessageCommand => _deselectMessageCommand;

    public void Dispose()
    {
        _chatService.MessageReceived -= _chatService_MessageReceived;
    }
}
