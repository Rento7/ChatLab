using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModels.Abstract
{
    internal interface IMainViewModel
    {
        IChatsListViewModel ChatsListViewModel { get; }
        IChatViewModel SelectedChatViewModel { get; set; }
    }
}
