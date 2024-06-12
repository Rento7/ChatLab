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
        ObservableCollection<IChatViewModel> Chats { get; set; }
    }
}
