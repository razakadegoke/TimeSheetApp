using Microsoft.EntityFrameworkCore;

namespace TimeSheetApp.Auth{
    public class UserAccountDbContext : DbContext
{
    public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>()
            .HasKey(u => u.Id);
    }

    public DbSet<UserAccount> UserAccount { get; set; }
} 
}
