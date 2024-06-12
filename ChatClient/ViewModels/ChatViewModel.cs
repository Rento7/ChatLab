using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ChatClient.Models;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using System.Windows.Input;

namespace ChatClient.ViewModels;

internal class ChatViewModel : ViewModelBase, IChatViewModel, IDisposable
{
    IChatService _chatService;
    IUIService _uiService;

    ObservableCollection<Message> _messages;
    string _name = string.Empty;
    string _messageText = string.Empty;
    ReactiveCommand<Unit, Unit> _sendMessageCommand;

    public ChatViewModel(IChatService chatService, IUIService uiService)
    {
        _chatService = chatService;
        _uiService = uiService;

        _messages = new ObservableCollection<Message>();

        IObservable<bool> isInputValid = this.WhenAnyValue(
            x => x.MessageText,
            msg => !string.IsNullOrWhiteSpace(msg)
            );

        _sendMessageCommand = ReactiveCommand.Create(() =>
        {
            //_messages.Add(new Message() { Text = MessageText });
            _chatService.SendMessage(MessageText);
            MessageText = string.Empty;
        }, isInputValid);

        _chatService.MessageReceived += _chatService_MessageReceived;
    }

    private void _chatService_MessageReceived(object? sender, string message)
    {
        _messages.Add(new Message() { Text = message });
    }

    public ObservableCollection<Message> Messages
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

    public ICommand SendMessageCommand => _sendMessageCommand;

    public void Dispose()
    {
        _chatService.MessageReceived -= _chatService_MessageReceived;
    }
}
