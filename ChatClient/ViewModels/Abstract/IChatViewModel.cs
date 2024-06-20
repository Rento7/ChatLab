using ReactiveUI;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Abstract
{
    public interface IChatViewModel
    {
        string Name { get; set; }
        bool IsMessageSelected { get; }
        bool IsEditMode { get; set; }
        IMessageItemViewModel SelectedMessageVm { get; set; }
        ObservableCollection<IMessageItemViewModel> Messages { get; set; }
        IReactiveCommand SendMessageCommand { get; }
        IReactiveCommand RenameChatCommand { get; }
        IReactiveCommand StartEditMessageCommand { get; }
        IReactiveCommand DeleteSelectedMessageCommand { get; }
        IReactiveCommand DeselectMessageCommand { get; }
        IReactiveCommand CancelEditingCommand { get; }
        IReactiveCommand EditMessageCommand { get; }
        IReactiveCommand OnTextBoxEnterCommand { get; }
    }
}
