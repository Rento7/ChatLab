using System.Collections.ObjectModel;
using ChatClient.Models;
using System.Windows.Input;

namespace ChatClient.ViewModels.Abstract
{
    internal interface IChatViewModel
    {
        string Name { get; set; }
        ObservableCollection<Message> Messages { get; set; }
        ICommand SendMessageCommand { get; }
    }
}
