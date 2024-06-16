using ChatDb.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatDb;
public class ChatContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Chat> Chats { get; set; }

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

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(x => x.ContactsOwner)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.ContactsOwnerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany<Message>()
                .WithOne(e => e.Sender)
                .HasForeignKey(e => e.SenderId)
                .IsRequired();

            entity.HasMany(e => e.Chats).WithMany(e => e.Users);
        });


        modelBuilder.Entity<Chat>(entity =>
        {
            entity.ToTable(nameof(Chat));

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(e => e.Messages)
                .WithOne(e => e.Chat)
                .HasForeignKey(e => e.ChatId)
                .IsRequired();

            entity.HasMany(e => e.Users).WithMany(e => e.Chats);
        });


        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable(nameof(Message));

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Text).HasMaxLength(450);

            entity.HasOne(e => e.Chat)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.ChatId)
                .IsRequired();

            entity.HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(e => e.SenderId)
                .IsRequired();
        });
    }
}
