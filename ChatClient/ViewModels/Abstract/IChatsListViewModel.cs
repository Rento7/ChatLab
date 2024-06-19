using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModels.Abstract
{
    internal interface IChatsListViewModel
    {
        ObservableCollection<IChatListItemViewModel> Chats { get; set; }
        IChatListItemViewModel SelectedChat { get; set; }
    }
}
