using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public class ProductCategoryPaymentRule : IPaymentMethodRule
{
    private readonly Dictionary<string, HashSet<PaymentMethodType>> _categoryRules;

    public ProductCategoryPaymentRule()
    {
        _categoryRules = new Dictionary<string, HashSet<PaymentMethodType>>
        {
            { "Food", new HashSet<PaymentMethodType> { 
                PaymentMethodType.Cash, 
                PaymentMethodType.SodexoVoucher 
            }},
            { "Electronics", new HashSet<PaymentMethodType> { 
                PaymentMethodType.CreditCard, 
                PaymentMethodType.DebitCard 
            }}
        };
    }

    public bool CanUsePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        return _categoryRules.TryGetValue(product.Category, out var allowedMethods) &&
               allowedMethods.Contains(paymentMethod.Type);
    }
} 