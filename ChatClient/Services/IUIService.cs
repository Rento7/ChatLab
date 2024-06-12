using ChatClient.ViewModels.Abstract;
using System;

namespace ChatClient.Services;

internal interface IUIService
{
    T GetViewModel<T>() where T : ViewModelBase;
}

internal interface IUIServiceInternal : IUIService
{
    event EventHandler<string> MessageReceived;
    void ReceiveMessage(string message);
}