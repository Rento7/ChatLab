using Avalonia.Threading;
using ChatAPI;
using ChatClient.Utility;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.Services;

internal class ChatService : IChatService, IDisposable
{
    IUIServiceInternal _uiService;
    HubConnection _connection = null!;
    string access_token;

    public ChatService(IUIService uiService) 
    {
        _uiService = (uiService as IUIServiceInternal)!;
        ArgumentNullException.ThrowIfNull(_uiService, nameof(uiService));

        _connection = new HubConnectionBuilder()
            .WithUrl(Urls.ChatUrl, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(access_token);
            })
            .Build();

        _connection.On<string>("Receive", message => _uiService.OnMessageReceived(message));
    }

    public event EventHandler<string> MessageReceived
    {
        add => _uiService.MessageReceived += value;
        remove => _uiService.MessageReceived -= value;
    }

    public event EventHandler LoginSuccessfully 
    {
        add => _uiService.LoginSuccessfully += value;
        remove => _uiService.LoginSuccessfully -= value;
    }

    public event EventHandler<LoginEventArgs> LoginUnsuccessfully
    {
        add => _uiService.LoginUnsuccessfully += value;
        remove => _uiService.LoginUnsuccessfully -= value;
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

    public async Task Login(string _login, string _password)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(Urls.LoginUrl);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonConvert.SerializeObject(new
            {
                login = _login,
                password = _password
            });

            streamWriter.Write(json);
        }

        HttpWebResponse response = null!;

        try
        {
            response = (HttpWebResponse)httpWebRequest.GetResponse();
        }
        catch (Exception ex) 
        {
            _uiService.OnLoginUnsuccessfully(new LoginEventArgs() { SuccessfulConnection = false, StatusCode = HttpStatusCode.NotFound });
        }

        if (response == null)
            return;

        if (response.StatusCode == HttpStatusCode.OK)
        {
            string result;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            var jResult = JObject.Parse(result);
            access_token = jResult.GetValue(nameof(access_token)).ToString();

            ConnectToServer();

            _uiService.OnLoginSuccessfully();
        }
        else 
        {
            _uiService.OnLoginUnsuccessfully(new LoginEventArgs() { SuccessfulConnection = false, StatusCode = response.StatusCode });
        }
    }

    public void Dispose()
    {
        _connection.StopAsync().Wait();
        _connection.DisposeAsync().AsTask().Wait();
    }
}
