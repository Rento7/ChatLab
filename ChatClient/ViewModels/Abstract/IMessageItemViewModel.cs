
namespace ChatClient.ViewModels.Abstract;

public interface IMessageItemViewModel
{
    public string Text { get; set; }
    bool IsSentByUser { get; }
}
