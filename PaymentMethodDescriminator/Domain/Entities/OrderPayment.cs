using System;

namespace PaymentMethodDescriminator.Domain.Entities;

public class OrderPayment
{
    public Guid OrderPaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public decimal? CurrencyExchangeRate { get; set; }
    public int PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; }
    public string MethodType { get; set; }
    public Guid? ElectronicPaymentsId { get; set; }
    public decimal? Tip { get; set; }
    public decimal? AmountPaidBack { get; set; }
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Order Order { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public OrderPayment(PaymentMethod paymentMethod, decimal amount)
    {
        OrderPaymentId = Guid.NewGuid();
        PaymentMethodId = paymentMethod.PaymentMethodId;
        PaymentMethodName = paymentMethod.Name;
        MethodType = paymentMethod.MethodType;
        Amount = amount;
        CreatedAt = DateTime.UtcNow;
    }

    protected OrderPayment() { }
} 