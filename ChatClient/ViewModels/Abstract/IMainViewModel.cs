namespace ChatClient.ViewModels.Abstract;

public interface IMainViewModel
{
    IChatsListViewModel ChatsListViewModel { get; }
    IChatViewModel SelectedChatViewModel { get; set; }
}
