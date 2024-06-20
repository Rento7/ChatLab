using System;
using ReactiveUI;
using ChatAPI.Models;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class MessageItemViewModel : ViewModelBase, IMessageItemViewModel
{
    bool _isSentByUser;
    Message _message;

    public MessageItemViewModel(Message message, bool isSentByUser = false) 
    {
        ArgumentNullException.ThrowIfNull(message);
        _message = message; 
        _isSentByUser = isSentByUser;
    }

    public bool IsSentByUser => _isSentByUser;

    public Guid MessageId => _message.Id;

    public string Text 
    { 
        get => _message.Text;
        set 
        {
            if (_message.Text == value)
                return;

            //TODO change 
            _message.Text = Text;
            this.RaisePropertyChanged();
        }
    }
}
