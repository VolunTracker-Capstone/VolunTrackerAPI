using AzureTest2.Controllers;
using Microsoft.EntityFrameworkCore;
using AzureTest2.Models; // Use your actual namespace

namespace AzureTest2.Data // Remove the comment here if it's part of the problem
{
    public class MyAzureContext : DbContext
    {
        public MyAzureContext(DbContextOptions<MyAzureContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Member { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<UserJoinsOrg> UserJoinsOrg { get; set; }
        public DbSet<UserAttendsEvent> UserAttendsEvent { get; set; }
        public DbSet<TagsList> TagsList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserJoinsOrg>()
                .HasKey(ujo => new { ujo.MemberID, ujo.OrgID });
            modelBuilder.Entity<UserAttendsEvent>()
                .HasKey(uae => new { uae.MemberID, uae.EventID });
            modelBuilder.Entity<TagsList>()
                .HasKey(tl => new { tl.TagID, tl.OrgID });
        }
    }
}