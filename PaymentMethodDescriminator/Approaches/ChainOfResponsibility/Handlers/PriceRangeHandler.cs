using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;

public class PriceRangeHandler : AbstractPaymentMethodHandler
{
    private readonly Dictionary<PaymentMethodType, (decimal min, decimal max)> _priceRanges;

    public PriceRangeHandler()
    {
        _priceRanges = new Dictionary<PaymentMethodType, (decimal min, decimal max)>
        {
            { PaymentMethodType.Cash, (0, 1000) },
            { PaymentMethodType.CreditCard, (0, 50000) }
        };
    }

    public override bool Handle(Product product, PaymentMethod paymentMethod)
    {
        if (_priceRanges.TryGetValue(paymentMethod.Type, out var range))
        {
            if (product.Price < range.min || product.Price > range.max)
                return false;
        }

        return base.Handle(product, paymentMethod);
    }
} 