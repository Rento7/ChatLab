using ChatClient.Models;
using System;
using System.Threading.Tasks;

namespace ChatClient.Services;

public interface IChatService
{
    Task ConnectToServer();

    Task SendMessage(string message);

    event EventHandler<string> MessageReceived;
}

