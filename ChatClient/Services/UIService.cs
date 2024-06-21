using System;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Threading;
using ChatAPI.Models;
using ChatClient.Utility;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.Services;

internal class UIService : IUIServiceInternal
{
    IServiceProvider _serviceProvider;

    public UIService(IServiceProvider serviceProvider) 
    {
        _serviceProvider = serviceProvider;
    }

    public event EventHandler LoginSuccessfully;
    public event EventHandler<LoginEventArgs> LoginUnsuccessfully;
    public event EventHandler<User> UserInitialized;
    public event EventHandler<Message> MessageReceived;
    public event EventHandler<Message> MessageEdited;
    public event EventHandler<Guid> MessageDeleted;
    public event EventHandler<Chat> ChatRenamed;
    public event EventHandler<Chat> SelectedChatChanged;
    public T GetViewModel<T>() where T : ViewModelBase
    {
        var vm = _serviceProvider.GetService<T>();
        ArgumentNullException.ThrowIfNull(vm, nameof(T));

        return vm;
    }

    public void OnMessageReceived(Message message)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            MessageReceived?.Invoke(this, message);
        });
    }

    public void OnMessageEdited(Message message)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            MessageEdited?.Invoke(this, message);
        });
    }

    public void OnMessageDeleted(Guid messageId)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            MessageDeleted?.Invoke(this, messageId);
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

    public void OnUserInitialized(User user) 
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            UserInitialized?.Invoke(this, user);
        });
    }

    public void OnSelectedChatChanged(Chat chat)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            SelectedChatChanged?.Invoke(this, chat);
        });
    }

    public void OnChatRenamed(Chat chat)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            ChatRenamed?.Invoke(this, chat);
        });
    }

    public void OnUpdateChatHistory(Chat chat) 
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            SelectedChatChanged?.Invoke(this, chat);
        });
    }



}

