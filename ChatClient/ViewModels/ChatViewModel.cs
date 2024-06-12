using ReactiveUI;

namespace ChatClient.ViewModels
{
    internal class ChatViewModel : Abstract.ViewModelBase
    {
        string _name;
        public virtual string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
    }
}
