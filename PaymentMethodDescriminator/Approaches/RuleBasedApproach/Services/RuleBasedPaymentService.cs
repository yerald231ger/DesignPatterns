using PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;
using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Services;

public class RuleBasedPaymentService
{
    private readonly IEnumerable<IPaymentMethodRule> _rules;
    
    public RuleBasedPaymentService(IEnumerable<IPaymentMethodRule> rules)
    {
        _rules = rules;
    }
    
    public bool ValidatePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        return _rules.All(rule => rule.CanUsePaymentMethod(product, paymentMethod));
    }

    public async Task<bool> ValidatePaymentMethodAsync(Product product, PaymentMethod paymentMethod)
    {
        foreach (var rule in _rules)
        {
            if (rule is IAsyncPaymentMethodRule asyncRule)
            {
                if (!await asyncRule.CanUsePaymentMethodAsync(product, paymentMethod))
                    return false;
            }
            else
            {
                if (!rule.CanUsePaymentMethod(product, paymentMethod))
                    return false;
            }
        }

        return true;
    }
} 