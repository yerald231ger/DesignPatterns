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
} 