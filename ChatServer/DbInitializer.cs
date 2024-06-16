using ChatDb;
using ChatDb.Models;

namespace ChatServer
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
        }
    }
}
