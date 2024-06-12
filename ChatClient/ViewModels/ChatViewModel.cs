using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ChatClient.Models;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class ChatViewModel : ViewModelBase, IChatViewModel
{
    ObservableCollection<Message> _messages;
    string _name = string.Empty;
    string _messageText = string.Empty;
    public ChatViewModel(IUIService uiService)
    {
        _messages = new ObservableCollection<Message>();

        IObservable<bool> isInputValid = this.WhenAnyValue(
            x => x.MessageText,
            msg => !string.IsNullOrWhiteSpace(msg)
            );

        SendMessageCommand = ReactiveCommand.Create(() =>
        {
            _messages.Add(new Message() { Text = MessageText });
        }, isInputValid);
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

    public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }

}
