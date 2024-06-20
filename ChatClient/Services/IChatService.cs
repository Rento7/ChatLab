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

    Task Login(string login, string password);

    event EventHandler LoginSuccessfully;

    event EventHandler<LoginEventArgs> LoginUnsuccessfully;

    event EventHandler<User> UserInitialized;

    event EventHandler<Message> MessageReceived;
}

