using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using ReactiveUI;
using ChatAPI.Models;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class ChatViewModel : ViewModelBase, IChatViewModel, IDisposable
{
    IChatService _chatService;
    IUIService _uiService;

    IMessageItemViewModel _selectedMessageVm;

    User _user;
    Chat _chat;

    bool _isEditMode;
    string _name = string.Empty;
    string _messageText = string.Empty;
    ObservableCollection<IMessageItemViewModel> _messages;
    ReactiveCommand<Unit, Unit> _sendMessageCommand;
    ReactiveCommand<Unit, Unit> _startEditMessageCommand;
    ReactiveCommand<Unit, Unit> _editMessageCommand;
    ReactiveCommand<Unit, Unit> _cancelEditingCommand;
    ReactiveCommand<Unit, Unit> _deselectMessageCommand;
    ReactiveCommand<Unit, Unit> _renameChatCommand;
    ReactiveCommand<Unit, Unit> _deleteSelectedMessageCommand;

    public ChatViewModel(IChatService chatService, IUIService uiService)
    {
        _chatService = chatService;
        _uiService = uiService;

        _messages = new ObservableCollection<IMessageItemViewModel>();

        IObservable<bool> isMessageInputValid = this.WhenAnyValue(
            vm => vm.MessageText,
            msg => !string.IsNullOrWhiteSpace(msg) && _chat != null
            );

        IObservable<bool> isOwnedByUser = this.WhenAnyValue<ChatViewModel, bool, bool>(
           vm => vm.IsMessageOwner,
           msg => msg
           );

        _sendMessageCommand = ReactiveCommand.Create(() =>
        {
            SendMessage();
            MessageText = string.Empty;


        }, isMessageInputValid);

        _startEditMessageCommand = ReactiveCommand.Create(() =>
        {
            IsEditMode = true;
            if (_selectedMessageVm != null)
                MessageText = _selectedMessageVm.Text;
        }, isOwnedByUser);

        _editMessageCommand = ReactiveCommand.Create(() =>
        {
            EditMessage();
            IsEditMode = false;
            MessageText = string.Empty;
        }, Observable.Merge(isMessageInputValid, isOwnedByUser));

        _cancelEditingCommand = ReactiveCommand.Create(() =>
        {
            IsEditMode = false;
            MessageText = string.Empty;
        });

        _deselectMessageCommand = ReactiveCommand.Create(() =>
        {
            SelectedMessageVm = null!;
        });

        //checking for null shouldn't be in body but have skill issue with observable checking null

        _renameChatCommand = ReactiveCommand.Create(() =>
        {
            if (_chat == null)
                return;

            _chatService.RenameChat(_chat.Id, _name);
        });

        _deleteSelectedMessageCommand = ReactiveCommand.Create(() =>
        {
            if (_selectedMessageVm == null)
                return;

            _chatService.DeleteMessage(_selectedMessageVm.MessageId);
        });

        _chatService.UserInitialized += _chatService_UserInitialized;
        _chatService.MessageReceived += _chatService_MessageReceived;
        _chatService.ChatRenamed += _chatService_ChatRenamed;
        _chatService.ChatHasReselected += _chatService_ChatHasReselected;
        _chatService.MessageEdited += _chatService_MessageEdited; ;
        _chatService.MessageDeleted += _chatService_MessageDeleted;
    }


    private void SendMessage() 
    {
        var message = new Message()
        {
            ChatId = _chat.Id,
            SenderId = _user.Id,
            Text = MessageText
        };
        _chatService.SendMessage(message);
    }

    private void EditMessage() 
    {
        if (_selectedMessageVm == null || !IsMessageSelected)
            return;

        _chatService.EditMessage(_selectedMessageVm.MessageId, MessageText);
    }

    private void _chatService_MessageEdited(object? sender, Message message)
    {
        var messageVm = _messages.FirstOrDefault(mvm => mvm.MessageId == message.Id);

        if (messageVm == null)
            return;

        messageVm.UpdateModel(message);
    }

    private void _chatService_MessageDeleted(object? sender, Guid id)
    {
        var messageToRemove = _messages.FirstOrDefault(mvm => mvm.MessageId == id);

        if(messageToRemove != null)
            _messages.Remove(messageToRemove);

        if (_selectedMessageVm != null && _selectedMessageVm.MessageId == id)
            SelectedMessageVm = null!;
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
            this.RaisePropertyChanged(nameof(IsMessageOwner));
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

    public bool IsEditMode
    {
        get => _isEditMode;
        set 
        {
            this.RaiseAndSetIfChanged(ref _isEditMode, value);
            this.RaisePropertyChanged(nameof(OnTextBoxEnterCommand));
        }
    }

    public bool IsMessageSelected => _selectedMessageVm != null;
    public bool IsMessageOwner => IsMessageSelected && _selectedMessageVm.IsSentByUser;

    public IReactiveCommand SendMessageCommand => _sendMessageCommand;
    
    public IReactiveCommand RenameChatCommand => _renameChatCommand;
    
    public IReactiveCommand StartEditMessageCommand => _startEditMessageCommand;
    
    public IReactiveCommand DeleteSelectedMessageCommand => _deleteSelectedMessageCommand;
    
    public IReactiveCommand DeselectMessageCommand => _deselectMessageCommand;
    
    public IReactiveCommand CancelEditingCommand => _cancelEditingCommand;
    
    public IReactiveCommand EditMessageCommand => _editMessageCommand;

    public IReactiveCommand OnTextBoxEnterCommand => IsEditMode ? _editMessageCommand : _sendMessageCommand;

    public void Dispose()
    {
        _chatService.UserInitialized -= _chatService_UserInitialized;
        _chatService.MessageReceived -= _chatService_MessageReceived;
        _chatService.ChatRenamed -= _chatService_ChatRenamed;
        _chatService.ChatHasReselected -= _chatService_ChatHasReselected;
    }
}
