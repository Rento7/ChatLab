using System;
using System.Threading.Tasks;
using ChatAPI;
using ChatAPI.Models;
using ChatClient.Utility;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient.Services
{
    internal partial class ChatService : IClientApi
    {
        public event EventHandler<Message> MessageReceived
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

        public event EventHandler<User> UserInitialized
        {
            add => _uiService.UserInitialized += value;
            remove => _uiService.UserInitialized -= value;
        }

        public void InitUser(User user)
        {
            _user = user;
            _uiService.OnUserInitialized(_user);
        }
 
        public void Test(string test)
        {
            var helloSignalR = test;
        }

        public async Task SendMessage(Message message)
        {
            try
            {
                await _connection.InvokeAsync(nameof(IServerApi.SendMessage), message);
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
