using Avalonia.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.Services;

internal class ChatService : IChatService, IDisposable
{
    HubConnection _connection = null!;

    IUIServiceInternal _uiService;
    
    public ChatService(IUIService uiService) 
    {
        _uiService = (uiService as IUIServiceInternal)!;
        ArgumentNullException.ThrowIfNull(_uiService, nameof(uiService));


        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5177/chat")
            .Build();

        _connection.On<string>("Receive", message => _uiService.ReceiveMessage(message));

    }

    public event EventHandler<string> MessageReceived
    {
        add => _uiService.MessageReceived += value;
        remove => _uiService.MessageReceived -= value;
    }

    public async Task ConnectToServer()
    {
        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            //TODO
        }
    }

    public async Task SendMessage(string message)
    {
        try
        {
            await _connection.InvokeAsync("Send", message);
        }
        catch (Exception ex)
        {
            //TODO
        }
    }

    public void Dispose()
    {
        _connection.StopAsync().Wait();
        _connection.DisposeAsync().AsTask().Wait();
    }
 
}
