using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;

public abstract class AbstractPaymentMethodHandler : IPaymentMethodHandler
{
    private IPaymentMethodHandler? _nextHandler;

    public IPaymentMethodHandler SetNext(IPaymentMethodHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual bool Handle(Product product, PaymentMethod paymentMethod)
    {
        if (_nextHandler == null) return true;
        
        return _nextHandler.Handle(product, paymentMethod);
    }
} 