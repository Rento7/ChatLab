using ChatDb.Models;

using Dto = ChatAPI.Models;

namespace ChatServer.Extensions;

public static class DtoExtensions
{
    public static Dto.Message ToDto(this Message message) 
    {
        var messageDto = new Dto.Message()
        {
            Id = message.Id,
            Text = message.Text,
            ChatId = message.ChatId,
            SenderId = message.SenderId
        };

        return messageDto;
    }

    public static Dto.ContactUser ToContactDto(this User user)
    {
        var contactUserDto = new Dto.ContactUser()
        {
            Id = user.Id,
            Name = user.Name,
        };

        return contactUserDto;
    }

    public static Dto.Chat ToDto(this Chat chat)
    {
        var chatDto = new Dto.Chat()
        {
            Id = chat.Id,
            Name = chat.Name,
            Messages = new List<Dto.Message>(),
            Users = new List<Dto.ContactUser>(),
        };

        foreach (var message in chat.Messages)
            chatDto.Messages.Add(ToDto(message));

        foreach (var user in chat.Users)
            chatDto.Users.Add(ToContactDto(user));

        return chatDto;
    }

    public static Dto.User ToDto(this User user)
    {
        var userDto = new Dto.User()
        {
            Id = user.Id,
            Name = user.Name,
            Login = user.Login,
            Chats = new List<Dto.Chat>(),
            Contacts = new List<Dto.ContactUser>(),
        };

        foreach (var chat in user.Chats)
            userDto.Chats.Add(ToDto(chat));

        foreach (var contact in user.Contacts)
            userDto.Contacts.Add(ToContactDto(contact));

        return userDto;
    }
}
