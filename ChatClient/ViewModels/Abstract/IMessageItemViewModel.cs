
using System;

namespace ChatClient.ViewModels.Abstract;

public interface IMessageItemViewModel
{
    string Text { get; set; }
    Guid MessageId { get; }
    bool IsSentByUser { get; }
}
