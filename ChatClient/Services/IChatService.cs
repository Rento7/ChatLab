using ChatClient.Models;
using ChatClient.Utility;
using System;
using System.Threading.Tasks;

namespace ChatClient.Services;

public interface IChatService
{
    Task ConnectToServer();

    Task SendMessage(string message);

    Task Login(string login, string password);

    event EventHandler LoginSuccessfully;

    event EventHandler<LoginEventArgs> LoginUnsuccessfully;

    event EventHandler<string> MessageReceived;
}

