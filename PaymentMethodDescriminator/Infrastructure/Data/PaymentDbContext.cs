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

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
        _dbPath = ":memory:";
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryPaymentMethodRule> CategoryPaymentMethodRules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.MethodType).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.CategoryId).IsRequired();
            entity.HasOne(e => e.Category)
                  .WithMany()
                  .HasForeignKey(e => e.CategoryId);
        });

        modelBuilder.Entity<CategoryPaymentMethodRule>(entity =>
        {
            entity.HasKey(e => e.RuleId);
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.PaymentMethodRules)
                  .HasForeignKey(e => e.CategoryId);
            entity.HasOne(e => e.PaymentMethod)
                  .WithMany(p => p.CategoryRules)
                  .HasForeignKey(e => e.PaymentMethodId);
        });
    }
} 