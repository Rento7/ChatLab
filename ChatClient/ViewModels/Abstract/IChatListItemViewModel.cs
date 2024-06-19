using ChatAPI.Models;

namespace ChatClient.ViewModels.Abstract;

internal interface IChatListItemViewModel
{
    Chat Chat { get; }
    string Name { get; set; }
}
