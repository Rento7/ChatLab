using ChatDb.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatDb;

public class ChatRepository : IChatRepository, IDisposable
{
    ChatContext _context;
    bool _disposed = false;
    public ChatRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers() 
    {
        return await _context.Users.Include(o => o.Contacts).ToListAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}