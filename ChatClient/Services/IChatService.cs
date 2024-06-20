using System;
using System.Threading.Tasks;
using ChatAPI.Models;
using ChatClient.Utility;

namespace ChatClient.Services;


public interface IChatService
{
    User CurrentUser { get; }
    Task ConnectToServer();
    Task SendMessage(Message message);
    Task EditMessage(Guid messageId, string newText);
    Task RenameChat(Guid chatId, string newName);
    Task SelectChat(Guid chatId);
    Task Login(string login, string password);

    event EventHandler LoginSuccessfully;

    event EventHandler<LoginEventArgs> LoginUnsuccessfully;

    event EventHandler<User> UserInitialized;

    event EventHandler<Message> MessageReceived;
    event EventHandler<Chat> ChatRenamed;
    event EventHandler<Chat> ChatHasReselected;
}

