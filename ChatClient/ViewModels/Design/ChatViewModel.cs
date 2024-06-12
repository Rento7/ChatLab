using ChatClient.Models;
using ChatClient.ViewModels.Abstract;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Design;

internal class ChatViewModel : ViewModelBase, IChatViewModel
{
    string _name = "design chat name";

    ObservableCollection<Message> _fakeMessages = new ObservableCollection<Message>() 
    {
        new Message() { Text = "Message 11111111" },
        new Message() { Text = "Message 22222222 abc" },
        new Message() { Text = "Message 33333333 qwert" },
        new Message() { Text = "Message 44444444 o0o" },
    };

    public string Name 
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public ObservableCollection<Message> Messages 
    { 
        get => _fakeMessages;
        set => this.RaiseAndSetIfChanged(ref _fakeMessages, value);
    }
}

