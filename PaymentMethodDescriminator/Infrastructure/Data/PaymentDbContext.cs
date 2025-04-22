using Microsoft.EntityFrameworkCore;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Infrastructure.Data;

public class PaymentDbContext : DbContext
{
    private readonly string _dbPath;

    public PaymentDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        _dbPath = Path.Join(path, "payment_rules.db");
    }

    public DbSet<CategoryPaymentRule> CategoryPaymentRules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryPaymentRule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Category).IsRequired();
            entity.Property(e => e.PaymentMethodType).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            // Seed initial data
            entity.HasData(
                new CategoryPaymentRule { Id = 1, Category = "Food", PaymentMethodType = PaymentMethodType.Cash, IsActive = true },
                new CategoryPaymentRule { Id = 2, Category = "Food", PaymentMethodType = PaymentMethodType.SodexoVoucher, IsActive = true },
                new CategoryPaymentRule { Id = 3, Category = "Electronics", PaymentMethodType = PaymentMethodType.CreditCard, IsActive = true },
                new CategoryPaymentRule { Id = 4, Category = "Electronics", PaymentMethodType = PaymentMethodType.DebitCard, IsActive = true }
            );
        });
    }
} 