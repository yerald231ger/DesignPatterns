using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public class AndSpecification : IPaymentMethodSpecification
{
    private readonly IPaymentMethodSpecification _left;
    private readonly IPaymentMethodSpecification _right;

    public AndSpecification(IPaymentMethodSpecification left, IPaymentMethodSpecification right)
    {
        _left = left;
        _right = right;
    }

    public bool CanUsePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        return _left.CanUsePaymentMethod(product, paymentMethod) && 
               _right.CanUsePaymentMethod(product, paymentMethod);
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