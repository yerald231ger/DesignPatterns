using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Domain.Repositories;
 
public interface ICategoryPaymentRuleRepository
{
    Task<IEnumerable<CategoryPaymentMethodRule>> GetAllActiveRulesAsync();
    Task<IEnumerable<CategoryPaymentMethodRule>> GetActiveRulesByCategoryAsync(string category);
} 