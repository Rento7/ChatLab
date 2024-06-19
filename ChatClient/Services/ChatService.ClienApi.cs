using System;
using System.Threading.Tasks;
using ChatAPI;
using ChatAPI.Models;
using ChatClient.Models;
using ChatClient.Utility;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient.Services
{
    internal partial class ChatService : IClientApi
    {
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

        public void InitUser(IUser user)
        {
            _user = user;
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

    }
}
