using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ChatDb;
using ChatAPI;
using ChatServer.Extensions;

using Dto = ChatAPI.Models;



namespace ChatServer
{
    [Authorize]
    public class ChatHub : Hub, IServerApi
    {
        IChatRepository _repository;
        public ChatHub(IChatRepository chatRepository) 
        {
            _repository = chatRepository;
        }

        public async Task SendMessage(Dto.Message messageDto)
        {
            var message = messageDto.FromDto();
            await _repository.CreateMessageAsync(message);

            var chat = await _repository.GetChatByIdAsync(message.ChatId);
            var users = chat.Users.Select(user => user.Id.ToString());

            //var createdMessage = await _repository.GetMessageByIdAsync(message.Id);

            await Clients.Users(users).SendAsync(nameof(IClientApi.ReceiveMessage), message.ToDto());
        }

        public async Task RequestUser() 
        {
            if (Context.UserIdentifier is string id)
            {
                var user = await _repository.GetUserAsync(Guid.Parse(id));
                await Clients.User(id).SendAsync(nameof(IClientApi.InitUser), user.ToDto());
            }
        }
    }
}
