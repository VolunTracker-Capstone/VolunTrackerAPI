using Microsoft.EntityFrameworkCore;
using AzureTest2.Models;

namespace AzureTest2.Data
{
    public class AzureTest2Context : DbContext
    {
        public AzureTest2Context(DbContextOptions<AzureTest2Context> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

    }
}