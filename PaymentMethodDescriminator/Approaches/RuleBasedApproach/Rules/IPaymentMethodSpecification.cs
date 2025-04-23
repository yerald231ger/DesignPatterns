using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public interface IPaymentMethodSpecification : IPaymentMethodRule
{
    IPaymentMethodSpecification And(IPaymentMethodSpecification other);
    IPaymentMethodSpecification Or(IPaymentMethodSpecification other);
} 