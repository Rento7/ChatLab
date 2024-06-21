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

        public event EventHandler<Message> MessageEdited
        {
            add => _uiService.MessageEdited += value;
            remove => _uiService.MessageEdited -= value;
        }

        public event EventHandler<Guid> MessageDeleted
        {
            add => _uiService.MessageDeleted += value;
            remove => _uiService.MessageDeleted -= value;
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

        public event EventHandler<Chat> ChatRenamed
        {
            add => _uiService.ChatRenamed += value;
            remove => _uiService.ChatRenamed -= value;
        }

        public event EventHandler<Chat> ChatHasReselected
        {
            add => _uiService.SelectedChatChanged += value;
            remove => _uiService.SelectedChatChanged -= value;
        }

        public void InitUser(User user)
        {
            _user = user;
            _uiService.OnUserInitialized(_user);
        }

        public void ReceiveMessage(Message message) 
        {
            _uiService.OnMessageReceived(message);
        }

        public void MessageHasEdited(Message message)
        {
            _uiService.OnMessageEdited(message);
        }

        public void MessageHasDeleted(Guid messageId)
        {
            _uiService.OnMessageDeleted(messageId);
        }

        public void ChatHasRenamed(Chat chat)
        {
            _uiService.OnChatRenamed(chat);
        }

        public void ChatHasSelected(Chat chat)
        {
            _uiService.OnSelectedChatChanged(chat);
        }

        public async Task SendMessage(Message message)
        {
            await _connection.InvokeAsync(nameof(IServerApi.SendMessage), message);
        }

        public async Task EditMessage(Guid messageId, string newText)
        {
            await _connection.InvokeAsync(nameof(IServerApi.EditMessage), messageId, newText);
        }
        public async Task DeleteMessage(Guid messageId)
        {
            await _connection.InvokeAsync(nameof(IServerApi.DeleteMessage), messageId);
        }

        public async Task RenameChat(Guid chatId, string newName) 
        {
            await _connection.InvokeAsync(nameof(IServerApi.RenameChat), chatId, newName);
        }

        public async Task SelectChat(Guid chatId)
        {
            await _connection.InvokeAsync(nameof(IServerApi.SelectChat), chatId);
        }

    }
}
