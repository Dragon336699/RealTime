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
    public class RealTimeDbContext : IdentityDbContext<User , Role , int>
    {
        public RealTimeDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Message_status> Message_statuses {  get; set; }
        public DbSet<User_status> User_statuses {  get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Attachment>()
                .HasOne(t => t.Message)
                .WithMany(t => t.Attachments)
                .HasForeignKey(t => t.messageid)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(t => t.Chat)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.chatid)
                .OnDelete(DeleteBehavior.Cascade);
            
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
