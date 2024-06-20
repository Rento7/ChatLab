using ChatClient.ViewModels.Abstract;
using ReactiveUI;
using System;
 
namespace ChatClient.ViewModels.Design
{
    internal class MessageItemViewModel : ViewModelBase, IMessageItemViewModel
    {
        string _text;
        public string Text 
        { 
            get => _text; 
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public bool IsSentByUser => false;
    }
}
