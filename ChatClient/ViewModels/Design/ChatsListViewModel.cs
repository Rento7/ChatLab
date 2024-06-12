using System.Collections.ObjectModel;

namespace ChatClient.ViewModels.Design;
internal class ChatsListViewModel : ViewModels.ChatsListViewModel
{
    public override ObservableCollection<ViewModels.ChatViewModel> Chats { get; set; } = new()
    {
        new ChatViewModel() { Name = "User chat 11111"},
        new ChatViewModel() { Name = "User chat 22222"},
        new ChatViewModel() { Name = "User chat 33333"},
    };
}
