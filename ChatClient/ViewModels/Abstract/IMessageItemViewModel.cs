
using ChatAPI.Models;
using System;

namespace ChatClient.ViewModels.Abstract;

public interface IMessageItemViewModel
{
    void UpdateModel(Message message);
    string Text { get; set; }
    Guid MessageId { get; }
    bool IsSentByUser { get; }
}
