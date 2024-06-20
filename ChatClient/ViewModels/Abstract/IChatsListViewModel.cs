
using System.Collections.ObjectModel;
 
namespace ChatClient.ViewModels.Abstract;

public interface IChatsListViewModel
{
    ObservableCollection<IChatListItemViewModel> Chats { get; set; }
    IChatListItemViewModel SelectedChat { get; set; }
}
