using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Abstract
{
    public interface IChatViewModel
    {
        string Name { get; set; }
        bool IsMessageSelected { get; }

        IMessageItemViewModel SelectedMessageVm { get; set; }
        ObservableCollection<IMessageItemViewModel> Messages { get; set; }
        IReactiveCommand SendMessageCommand { get; }
        IReactiveCommand RenameChatCommand { get; }
        IReactiveCommand StartEditMessageCommand { get; }
        IReactiveCommand DeleteSelectedMessageCommand { get; }
        IReactiveCommand DeselectMessageCommand { get; }
    }
}
