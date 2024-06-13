using ChatDb.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatDb;
public class ChatContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ChatContext()
    {
    }

    public ChatContext(DbContextOptions<ChatContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(nameof(User));

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
    }
}
