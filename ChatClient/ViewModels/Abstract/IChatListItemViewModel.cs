using ChatAPI.Models;

namespace ChatClient.ViewModels.Abstract;

public interface IChatListItemViewModel
{
    Chat Chat { get; }
    string Name { get; set; }
}
