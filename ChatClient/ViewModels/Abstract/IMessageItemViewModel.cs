using System;
using ChatAPI.Models;

namespace ChatClient.ViewModels.Abstract;

public interface IMessageItemViewModel
{
    void UpdateModel(Message message);
    string Text { get; set; }
    Guid MessageId { get; }
    bool IsSentByUser { get; }
}
