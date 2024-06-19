using ChatDb;
using ChatDb.Models;

namespace ChatServer.Data
{
    public class DbInitializer
    {
        public static void Initialize(ChatContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.AddRange(new User[]
            {
                new User()
                {
                    Login = "login1",
                    Password = "login1",
                    Name = "firstUser",
                },
                new User()
                {
                    Login = "login2",
                    Password = "login2",
                    Name = "secondUser",
                },
                new User()
                {
                    Login = "login3",
                    Password = "login3",
                    Name = "thirdUser",
                },
                new User()
                {
                    Login = "login4",
                    Password = "login4",
                    Name = "fourthUser",
                }
            });

            context.SaveChanges();

            var users = context.Users.ToArray();

            users[0].ContactsOwner = users[0];
            users[0].ContactsOwnerId = users[0].Id;
            users[0].Contacts.Add(users[1]);
            users[0].Contacts.Add(users[2]);

            users[1].ContactsOwner = users[1];
            users[1].ContactsOwnerId = users[1].Id;
            users[1].Contacts.Add(users[0]);

            users[2].ContactsOwner = users[2];
            users[2].ContactsOwnerId = users[2].Id;
            users[2].Contacts.Add(users[0]);

            context.SaveChanges();

            var chats = new List<Chat>();
            var chat = new Chat() { Name = "Chat 1" };
            chat.Users.Add(users[0]);
            chat.Users.Add(users[1]);
            chats.Add(chat);

            chat = new Chat() { Name = "Chat 2" };
            chat.Users.Add(users[0]);
            chat.Users.Add(users[2]);
            chats.Add(chat);

            context.Chats.AddRange(chats);
            context.SaveChanges();

            chats = context.Chats.ToList();

            var message = new Message()
            {
                Text = "first message",
                Chat = chats[0],
                ChatId = chats[0].Id,
                Sender = users[1],
                SenderId = users[1].Id,
            };
            context.Messages.Add(message);

            message = new Message()
            {
                Text = "second message",
                Chat = chats[0],
                ChatId = chats[0].Id,
                Sender = users[1],
                SenderId = users[1].Id,
            };
            context.Messages.Add(message);

            message = new Message()
            {
                Text = "third message",
                Chat = chats[0],
                ChatId = chats[0].Id,
                Sender = users[0],
                SenderId = users[0].Id,
            };
            context.Messages.Add(message);

            message = new Message()
            {
                Text = "fourth message",
                Chat = chats[0],
                ChatId = chats[0].Id,
                Sender = users[1],
                SenderId = users[1].Id,
            };
            context.Messages.Add(message);

            message = new Message()
            {
                Text = "first message",
                Chat = chats[1],
                ChatId = chats[1].Id,
                Sender = users[0],
                SenderId = users[0].Id,
            };
            context.Messages.Add(message);

            message = new Message()
            {
                Text = "second message",
                Chat = chats[1],
                ChatId = chats[0].Id,
                Sender = users[2],
                SenderId = users[2].Id,
            };
            context.Messages.Add(message);

            context.SaveChanges();
        }
    }
}
