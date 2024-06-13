using ChatClient.Utility;
using ChatClient.ViewModels.Abstract;
using System;
using System.Net;

namespace ChatClient.Services;

internal interface IUIService
{
    T GetViewModel<T>() where T : ViewModelBase;
}

internal interface IUIServiceInternal : IUIService
{
    event EventHandler<string> MessageReceived;
    event EventHandler<LoginEventArgs> LoginUnsuccessfully;
    event EventHandler LoginSuccessfully;
    void OnMessageReceived(string message);
    void OnLoginSuccessfully();
    void OnLoginUnsuccessfully(LoginEventArgs args);
}