using System;

namespace PaymentMethodDescriminator.Domain.Entities;

public class OrderDetail
{
    public Guid OrderDetailId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Total { get; set; }
    public decimal Price { get; set; }
    public Guid? DispatchId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public Guid OrderId { get; set; }
    public int? LocationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }

    public OrderDetail(Product product, decimal quantity, decimal price)
    {
        OrderDetailId = Guid.NewGuid();
        ProductId = product.ProductId;
        ProductName = product.Name;
        CategoryName = product.Category.Name;
        Quantity = quantity;
        Price = price;
        Total = quantity * price;
        CreatedAt = DateTime.UtcNow;
    }

    protected OrderDetail() { }
} 