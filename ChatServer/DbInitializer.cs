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

            context.SaveChanges();
        }
    }
}
