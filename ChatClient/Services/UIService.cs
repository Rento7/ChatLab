using Avalonia.Threading;
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

    public T GetViewModel<T>() where T : ViewModelBase
    {
        var vm = _serviceProvider.GetService<T>();
        ArgumentNullException.ThrowIfNull(vm, nameof(T));

        return vm;
    }

    public void ReceiveMessage(string message)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            MessageReceived?.Invoke(this, message);
        });
    }
}

