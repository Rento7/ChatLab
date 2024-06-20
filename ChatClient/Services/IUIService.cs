using ChatAPI.Models;
using ChatClient.Utility;
using ChatClient.ViewModels.Abstract;
using System;

namespace ChatClient.Services;

internal interface IUIService
{
    T GetViewModel<T>() where T : ViewModelBase;

    event EventHandler<Chat> SelectedChatChanged;
    void OnSelectedChatChanged(Chat chat);
}

internal interface IUIServiceInternal : IUIService
{
    event EventHandler LoginSuccessfully;
    event EventHandler<LoginEventArgs> LoginUnsuccessfully;
    event EventHandler<User> UserInitialized;
    event EventHandler<Message> MessageReceived;
    event EventHandler<Chat> ChatRenamed;
    void OnLoginSuccessfully();
    void OnLoginUnsuccessfully(LoginEventArgs args);
    void OnUserInitialized(User user);
    void OnMessageReceived(Message message);
    void OnChatRenamed(Chat chat);
}