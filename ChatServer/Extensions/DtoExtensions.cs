using ChatAPI.Models;
using ChatDb.Models;
using ChatServer.Models;

namespace ChatServer.Extensions;

public static class DtoExtensions
{
    public static MessageDto ToDto(this Message message) 
    {
        var messageDto = new MessageDto()
        {
            Id = message.Id,
            Text = message.Text,
            ChatId = message.ChatId,
            SenderId = message.SenderId
        };

        return messageDto;
    }

    public static ContactUserDto ToContactDto(this User user)
    {
        var contactUserDto = new ContactUserDto()
        {
            Id = user.Id,
            Name = user.Name,
        };

        return contactUserDto;
    }

    public static ChatDto ToDto(this Chat chat)
    {
        var chatDto = new ChatDto()
        {
            Id = chat.Id,
            Name = chat.Name,
            Messages = new List<IMessage>(),
            Users = new List<IContactUser>(),
        };

        foreach (var message in chat.Messages)
            chatDto.Messages.Add(ToDto(message));

        foreach (var user in chat.Users)
            chatDto.Users.Add(ToContactDto(user));

        return chatDto;
    }

    public static UserDto ToDto(this User user)
    {
        var userDto = new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Login = user.Login,
            Chats = new List<IChat>(),
            Contacts = new List<IContactUser>(),
        };

        foreach (var chat in user.Chats)
            userDto.Chats.Add(ToDto(chat));

        foreach (var contact in user.Contacts)
            userDto.Contacts.Add(ToContactDto(contact));

        return userDto;
    }
}
