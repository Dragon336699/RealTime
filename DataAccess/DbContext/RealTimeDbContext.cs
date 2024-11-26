using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbContext
{
    public class RealTimeDbContext : IdentityDbContext<User , Role , Guid>
    {
        public RealTimeDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<MessageStatus> MessageStatus {  get; set; }
        public DbSet<UserStatus> UserStatus {  get; set; }
        public DbSet<ChatUser> ChatUser { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Attachment>()
                .HasOne(t => t.Message)
                .WithMany(t => t.Attachments)
                .HasForeignKey(t => t.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(t => t.Chat)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MessageStatus>()
                .HasData(
                    new MessageStatus { Id = Guid.NewGuid(), Name = "Sending" },
                    new MessageStatus { Id = Guid.NewGuid(), Name = "Sent" },
                    new MessageStatus { Id = Guid.NewGuid(), Name = "Delivered" },
                    new MessageStatus { Id = Guid.NewGuid(), Name = "Seen" },
                    new MessageStatus { Id = Guid.NewGuid(), Name = "Error" }

                );

            foreach(var entityType in builder.Model.GetEntityTypes())
            {
                var idProperty = entityType.FindProperty("Id");
                if (idProperty != null && idProperty.ClrType == typeof(Guid))
                {
                    idProperty.SetDefaultValueSql("uuid_generate_v4()");
                }
            }
            
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role");
            });
        }
    }
}
