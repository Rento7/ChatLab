using Avalonia.Threading;
using ChatAPI.Models;
using ChatClient.Models;
using ChatClient.Utility;
using ChatClient.ViewModels;
using ChatClient.ViewModels.Abstract;
using ChatClient.ViewModels.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ChatClient.Services;

internal class UIService : IUIServiceInternal
{
    IServiceProvider _serviceProvider;

    public UIService(IServiceProvider serviceProvider) 
    {
        _serviceProvider = serviceProvider;
    }

    public event EventHandler<string> MessageReceived;
    public event EventHandler LoginSuccessfully;
    public event EventHandler<LoginEventArgs> LoginUnsuccessfully;
    public event EventHandler<IUser> UserInitialized;

    public T GetViewModel<T>() where T : ViewModelBase
    {
        var vm = _serviceProvider.GetService<T>();
        ArgumentNullException.ThrowIfNull(vm, nameof(T));

        return vm;
    }

    public void OnMessageReceived(string message)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            MessageReceived?.Invoke(this, message);
        });
    }

    public void OnLoginSuccessfully()
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            LoginSuccessfully?.Invoke(this, EventArgs.Empty);
        });
    }

    public void OnLoginUnsuccessfully(LoginEventArgs args)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            LoginUnsuccessfully?.Invoke(this, args);
        });
    }

    public void OnUserInitialized(IUser user) 
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            UserInitialized?.Invoke(this, IUser);
        });
    }

}

