using PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;
using PaymentMethodDescriminator.Domain.Entities;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Services;

public class ChainPaymentService
{
    private readonly IPaymentMethodHandler _handler;

    public ChainPaymentService()
    {
        // Set up the chain
        var priceHandler = new PriceRangeHandler();
        var categoryHandler = new CategoryHandler();
        
        priceHandler.SetNext(categoryHandler);
        
        _handler = priceHandler;
    }

    public bool ValidatePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        return _handler.Handle(product, paymentMethod);
    }
} 