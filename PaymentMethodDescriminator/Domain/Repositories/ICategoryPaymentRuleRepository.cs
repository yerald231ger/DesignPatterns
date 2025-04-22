using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Domain.Repositories;

public interface ICategoryPaymentRuleRepository
{
    Task<IEnumerable<CategoryPaymentRule>> GetAllActiveRulesAsync();
    Task<IEnumerable<CategoryPaymentRule>> GetActiveRulesByCategoryAsync(string category);
} 