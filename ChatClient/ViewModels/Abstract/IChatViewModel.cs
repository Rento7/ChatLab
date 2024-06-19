using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Abstract
{
    internal interface IChatViewModel
    {
        string Name { get; set; }
        ObservableCollection<IMessageItemViewModel> Messages { get; set; }
        IReactiveCommand SendMessageCommand { get; }
    }
}
