using System;
using ChatAPI.Models;
using ChatClient.Utility;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.Services;

internal interface IUIService
{
    T GetViewModel<T>() where T : ViewModelBase;
}

internal interface IUIServiceInternal : IUIService
{
    event EventHandler LoginSuccessfully;
    event EventHandler<LoginEventArgs> LoginUnsuccessfully;
    event EventHandler<User> UserInitialized;
    event EventHandler<Message> MessageReceived;
    event EventHandler<Message> MessageEdited;
    event EventHandler<Guid> MessageDeleted;
    event EventHandler<Chat> ChatRenamed;
    event EventHandler<Chat> SelectedChatChanged;
    void OnLoginSuccessfully();
    void OnLoginUnsuccessfully(LoginEventArgs args);
    void OnUserInitialized(User user);
    void OnMessageReceived(Message message);
    void OnMessageEdited(Message message);
    void OnMessageDeleted(Guid messageId);

    void OnChatRenamed(Chat chat);
    void OnSelectedChatChanged(Chat chat);
}