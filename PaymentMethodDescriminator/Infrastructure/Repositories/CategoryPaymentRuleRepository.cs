using Microsoft.EntityFrameworkCore;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Repositories;
using PaymentMethodDescriminator.Infrastructure.Data;

namespace PaymentMethodDescriminator.Infrastructure.Repositories;

public class CategoryPaymentRuleRepository : ICategoryPaymentRuleRepository
{
    private readonly PaymentDbContext _context;

    public CategoryPaymentRuleRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryPaymentMethodRule>> GetAllActiveRulesAsync()
    {
        return await _context.CategoryPaymentMethodRules
            .Include(r => r.Category)
            .Include(r => r.PaymentMethod)
            .Where(r => r.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<CategoryPaymentMethodRule>> GetActiveRulesByCategoryAsync(string category)
    {
        return await _context.CategoryPaymentMethodRules
            .Include(r => r.Category)
            .Include(r => r.PaymentMethod)
            .Where(r => r.IsActive && r.Category.Name == category)
            .ToListAsync();
    }
} 