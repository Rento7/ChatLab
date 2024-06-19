using ChatAPI.Models;
using ChatClient.Utility;
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
    event EventHandler<LoginEventArgs> LoginUnsuccessfully;
    event EventHandler LoginSuccessfully;
    event EventHandler<IUser> UserInitialized;
    void OnMessageReceived(string message);
    void OnLoginSuccessfully();
    void OnLoginUnsuccessfully(LoginEventArgs args);
    void OnUserInitialized(IUser user);
}