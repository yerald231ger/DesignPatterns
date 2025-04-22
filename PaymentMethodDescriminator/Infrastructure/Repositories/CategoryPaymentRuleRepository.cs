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

    public async Task<IEnumerable<CategoryPaymentRule>> GetAllActiveRulesAsync()
    {
        return await _context.CategoryPaymentRules
            .Where(r => r.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<CategoryPaymentRule>> GetActiveRulesByCategoryAsync(string category)
    {
        return await _context.CategoryPaymentRules
            .Where(r => r.IsActive && r.Category == category)
            .ToListAsync();
    }
} 