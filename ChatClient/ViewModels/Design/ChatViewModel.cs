using System.Collections.ObjectModel;
using ReactiveUI;
using ChatClient.Commands;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels.Design;

internal class ChatViewModel : ViewModelBase, IChatViewModel
{
    string _name = "design chat name";

    ObservableCollection<IMessageItemViewModel> _fakeMessages = new ObservableCollection<IMessageItemViewModel>() 
    {
        new MessageItemViewModel() { Text = "Message 11111111" },
        new MessageItemViewModel() { Text = "Message 22222222 abc" },
        new MessageItemViewModel() { Text = "Message 33333333 qwert" },
        new MessageItemViewModel() { Text = "Message 44444444 o0o" },
    };

    public string Name 
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public ObservableCollection<IMessageItemViewModel> Messages 
    { 
        get => _fakeMessages;
        set => this.RaiseAndSetIfChanged(ref _fakeMessages, value);
    }

    public IReactiveCommand SendMessageCommand => null!;

}

