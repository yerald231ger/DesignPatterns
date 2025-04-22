namespace PaymentMethodDescriminator.Domain.Entities;

public class OrderDetail
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Total { get; private set; }

    public Order Order { get; private set; }
    public Product Product { get; private set; }

    public OrderDetail(Order order, Product product, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        Order = order;
        OrderId = order.Id;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CalculateAmounts();
    }

    private void CalculateAmounts()
    {
        Subtotal = Quantity * UnitPrice;
        Tax = Subtotal * 0.16m; // Assuming 16% tax rate
        Total = Subtotal + Tax;
    }

    // Protected constructor for EF Core
    protected OrderDetail() { }
} 