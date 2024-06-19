using System;
using ReactiveUI;
using ChatAPI.Models;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class MessageItemViewModel : ViewModelBase, IMessageItemViewModel
{
    Message _message;
    public MessageItemViewModel(Message message) 
    {
        ArgumentNullException.ThrowIfNull(message);
        _message = message; 
    }

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
