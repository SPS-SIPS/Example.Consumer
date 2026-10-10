using Microsoft.EntityFrameworkCore;
using SIPS.Example.Consumer.Enums;
using SIPS.Example.Consumer.Models;
using SIPS.Example.Consumer.Models.DB;

namespace SIPS.Example.Consumer.Services;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<InterBankTransaction> InterBankTransactions { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InterBankTransaction>().Property(e => e.ChargeBearer).HasConversion(x => x.Id, x => Enumeration<string>.FromId<ChargeBearerType>(x));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
