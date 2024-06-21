 namespace ChatClient.ViewModels.Abstract;

public interface IChatPageViewModel
{
    IChatsListViewModel ChatsListViewModel { get; }
    IChatViewModel SelectedChatViewModel { get; set; }
}
