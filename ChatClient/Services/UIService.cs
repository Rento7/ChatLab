using ChatClient.ViewModels;
using ChatClient.ViewModels.Abstract;
using ChatClient.ViewModels.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ChatClient.Services;

internal class UIService : IUIService
{
    IServiceProvider _serviceProvider;

    public UIService(IServiceProvider serviceProvider) 
    {
        _serviceProvider = serviceProvider;
    }

    public T GetViewModel<T>() where T : ViewModelBase
    {
        var vm = _serviceProvider.GetService<T>();
        ArgumentNullException.ThrowIfNull(vm, nameof(T));

        return vm;
    }
}

