using ChatClient.Models;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Design;

internal class ChatViewModel : ViewModels.ChatViewModel
{
    ObservableCollection<Message> _fakeMessages = new ObservableCollection<Message>() 
    {
        new Message() { Text = "Message 11111111" },
        new Message() { Text = "Message 22222222 abc" },
        new Message() { Text = "Message 33333333 qwert" },
        new Message() { Text = "Message 44444444 o0o" },
    };

    public override string Name 
    {
        get => "Chat name";
        set { }  
    }

    public override ObservableCollection<Message> Messages 
    { 
        get => _fakeMessages;
        set { }
    }
}

