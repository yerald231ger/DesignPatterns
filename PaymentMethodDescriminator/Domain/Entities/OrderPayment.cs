namespace PaymentMethodDescriminator.Domain.Entities;

public class OrderPayment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid PaymentMethodId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public string? TransactionReference { get; private set; }

    public Order Order { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }

    public OrderPayment(Order order, PaymentMethod paymentMethod, decimal amount, string? transactionReference = null)
    {
        Id = Guid.NewGuid();
        Order = order;
        OrderId = order.Id;
        PaymentMethod = paymentMethod;
        PaymentMethodId = paymentMethod.Id;
        Amount = amount;
        PaymentDate = DateTime.UtcNow;
        TransactionReference = transactionReference;
    }

    // Protected constructor for EF Core
    protected OrderPayment() { }
} 