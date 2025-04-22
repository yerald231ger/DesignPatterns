using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public interface IAsyncPaymentMethodRule : IPaymentMethodRule
{
    Task<bool> CanUsePaymentMethodAsync(Product product, PaymentMethod paymentMethod);
} 