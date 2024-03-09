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
    }
}