using System.Collections.Generic;
using System.Linq;

namespace PaymentMethodDescriminator.Domain.Entities;

public class Order
{
    private readonly List<OrderDetail> _orderDetails;
    private readonly List<OrderPayment> _orderPayments;

    public Guid Id { get; private set; }
    public string Folio { get; private set; }
    public DateTime CreationDate { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Total { get; private set; }
    public string Status { get; private set; }

    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();
    public IReadOnlyCollection<OrderPayment> OrderPayments => _orderPayments.AsReadOnly();

    public Order(string folio)
    {
        Id = Guid.NewGuid();
        Folio = folio;
        CreationDate = DateTime.UtcNow;
        Status = "Created";
        _orderDetails = new List<OrderDetail>();
        _orderPayments = new List<OrderPayment>();
    }

    public void AddOrderDetail(Product product, int quantity, decimal unitPrice)
    {
        var orderDetail = new OrderDetail(this, product, quantity, unitPrice);
        _orderDetails.Add(orderDetail);
        RecalculateOrderTotals();
    }

    public void AddPayment(PaymentMethod paymentMethod, decimal amount, string? transactionReference = null)
    {
        var payment = new OrderPayment(this, paymentMethod, amount, transactionReference);
        _orderPayments.Add(payment);
    }

    private void RecalculateOrderTotals()
    {
        Subtotal = _orderDetails.Sum(x => x.Subtotal);
        Tax = _orderDetails.Sum(x => x.Tax);
        Total = _orderDetails.Sum(x => x.Total);
    }

    // Protected constructor for EF Core
    protected Order() 
    {
        _orderDetails = new List<OrderDetail>();
        _orderPayments = new List<OrderPayment>();
    }
} 