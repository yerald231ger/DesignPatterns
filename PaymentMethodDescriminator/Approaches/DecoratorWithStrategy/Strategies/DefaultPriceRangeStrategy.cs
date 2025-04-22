using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.DecoratorWithStrategy.Strategies;

public class DefaultPriceRangeStrategy : IPriceRangeStrategy
{
    private readonly Dictionary<PaymentMethodType, (decimal min, decimal max)> _ranges;

    public DefaultPriceRangeStrategy()
    {
        _ranges = new Dictionary<PaymentMethodType, (decimal min, decimal max)>
        {
            { PaymentMethodType.Cash, (0, 1000) },
            { PaymentMethodType.CreditCard, (0, 50000) },
            { PaymentMethodType.DebitCard, (0, 10000) },
            { PaymentMethodType.SodexoVoucher, (0, 500) }
        };
    }

    public bool IsInRange(decimal price, PaymentMethodType paymentType)
    {
        if (_ranges.TryGetValue(paymentType, out var range))
        {
            return price >= range.min && price <= range.max;
        }
        return true;
    }
} 