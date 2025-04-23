using PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;
using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Repositories;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Services;

public class ChainPaymentService
{
    private readonly IPaymentMethodHandler _handler;

    public ChainPaymentService(ICategoryPaymentRuleRepository repository)
    {
        var categoryHandler = new CategoryHandler(repository);
        var priceRangeHandler = new PriceRangeHandler();

        categoryHandler.SetNext(priceRangeHandler);
        _handler = categoryHandler;
    }

    public bool ValidatePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        return _handler.Handle(product, paymentMethod);
    }
} 