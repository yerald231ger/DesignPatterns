using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;

public class CategoryHandler : AbstractPaymentMethodHandler
{
    private readonly Dictionary<string, HashSet<PaymentMethodType>> _categoryRules;

    public CategoryHandler()
    {
        _categoryRules = new Dictionary<string, HashSet<PaymentMethodType>>
        {
            ["Food"] = new HashSet<PaymentMethodType> 
            { 
                PaymentMethodType.Cash,
                PaymentMethodType.SodexoVoucher
            },
            ["Electronics"] = new HashSet<PaymentMethodType> 
            { 
                PaymentMethodType.Cash,
                PaymentMethodType.CreditCard,
                PaymentMethodType.DebitCard
            }
        };
    }

    public override bool Handle(Product product, PaymentMethod paymentMethod)
    {
        if (_categoryRules.TryGetValue(product.Category, out var allowedMethods))
        {
            if (!allowedMethods.Contains(paymentMethod.Type))
                return false;
        }

        return base.Handle(product, paymentMethod);
    }
} 