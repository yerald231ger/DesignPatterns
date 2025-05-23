using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public class PriceRangePaymentRule : IPaymentMethodSpecification
{
    private readonly Dictionary<PaymentMethodType, (decimal min, decimal max)> _priceRanges;

    public PriceRangePaymentRule()
    {
        _priceRanges = new Dictionary<PaymentMethodType, (decimal min, decimal max)>
        {
            { PaymentMethodType.Cash, (0, 1000) },
            { PaymentMethodType.CreditCard, (0, 50000) }
        };
    }

    public bool CanUsePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        if (_priceRanges.TryGetValue(paymentMethod.Type, out var range))
        {
            return product.Price >= range.min && product.Price <= range.max;
        }
        return true;
    }

    public IPaymentMethodSpecification And(IPaymentMethodSpecification other)
    {
        return new AndSpecification(this, other);
    }

    public IPaymentMethodSpecification Or(IPaymentMethodSpecification other)
    {
        return new OrSpecification(this, other);
    }
} 