using ChatDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ChatDb;

public class ChatRepository : IChatRepository, IDisposable
{
    ChatContext _context;
    bool _disposed = false;
    public ChatRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.Include(o => o.Contacts).ToListAsync();
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user != null) 
        {
            await _context.Entry(user).Collection(u => u.Contacts).LoadAsync();
            await _context.Entry(user).Collection(u => u.Chats).Query().Include(c => c.Messages).LoadAsync();
        }

        return user!;
    }

    public async Task<IEnumerable<Chat>> GetChatsAsync()
    {
        return await _context.Chats.Include(o => o.Messages).Include(o => o.Users).ToListAsync();
    }

    public async Task<IEnumerable<Chat>> GetChatsByUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        IEnumerable<Chat> chats = null!;

        if (user != null)
        {
            await _context.Entry(user).Collection(u => u.Chats).Query().Include(c => c.Messages).Include(c => c.Users).LoadAsync();
            chats = user.Chats;
        }

        return chats;
    }

    public async Task<Chat> GetChatByIdAsync(Guid id)
    {
        var chat = await _context.Chats.FindAsync(id);

        if (chat != null)
        {
            await _context.Entry(chat).Collection(c => c.Messages).LoadAsync();
        }

        return chat!;
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync()
    {
        return await _context.Messages.ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetMessagesByChatAsync(Guid chatId)
    {
        var chat = await _context.Chats.FindAsync(chatId);
        IEnumerable<Message> messages = null!;


        if (chat != null)
        {
            await _context.Entry(chat).Collection(u => u.Messages).LoadAsync();
            messages = chat.Messages;
        }

        return messages;
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task CreateChatAsync(Chat chat)
    {
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public async Task CreateMessageAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
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