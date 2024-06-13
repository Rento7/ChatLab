using ChatDb.Models;

namespace ChatDb;

public interface IChatRepository
{
    Task<IEnumerable<User>> GetUsers();
}
