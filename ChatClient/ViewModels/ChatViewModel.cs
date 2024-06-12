using ChatClient.Models;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels
{
    internal class ChatViewModel : Abstract.ViewModelBase
    {
        ObservableCollection<Message> _messages;
        string _name;

        public virtual ObservableCollection<Message> Messages
        {
            get => _messages;
            set => this.RaiseAndSetIfChanged(ref _messages, value);
        }

        public virtual string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
    }
}
