using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;
 
public interface IPaymentMethodHandler
{
    IPaymentMethodHandler SetNext(IPaymentMethodHandler handler);
    bool Handle(Product product, PaymentMethod paymentMethod);
} 