using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;

public class DefaultCategoryStrategy : ICategoryStrategy
{
    private readonly Dictionary<string, HashSet<PaymentMethodType>> _categoryRules;

    public DefaultCategoryStrategy()
    {
        _categoryRules = new Dictionary<string, HashSet<PaymentMethodType>>
        {
            { "Food", new HashSet<PaymentMethodType> { 
                PaymentMethodType.Cash, 
                PaymentMethodType.SodexoVoucher,
                PaymentMethodType.DebitCard
            }},
            { "Electronics", new HashSet<PaymentMethodType> { 
                PaymentMethodType.CreditCard, 
                PaymentMethodType.DebitCard,
                PaymentMethodType.Cash
            }}
        };
    }

    public bool IsValidForCategory(string category, PaymentMethodType paymentType)
    {
        if (_categoryRules.TryGetValue(category, out var allowedMethods))
        {
            return allowedMethods.Contains(paymentType);
        }
        return true;
    }
} 