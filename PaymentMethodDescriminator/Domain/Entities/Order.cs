using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentMethodDescriminator.Domain.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public string Type { get; set; }
    public int StationId { get; set; }
    public int Pump { get; set; }
    public int? AdministrativeDayId { get; set; }
    public DateTime? AdministrativeDay { get; set; }
    public long OrderNumber { get; set; }
    public decimal Total { get; set; }
    public decimal Tip { get; set; }
    public bool TicketPrinted { get; set; }
    public string Status { get; set; }
    public bool HasDispatch { get; set; }
    public bool HasManyPayment { get; set; }
    public int CountItem { get; set; }
    public DateTime? CutOffDate { get; set; }
    public int CountPrinted { get; set; }
    public int GasNGoId { get; set; }
    public int? LiquidationDetailId { get; set; }
    public int? EmployeeNumber { get; set; }
    public string TransactionId { get; set; }
    public string IsCoalition { get; set; }
    public byte TicketReprintCountPanel { get; set; }
    public int Folio { get; set; }
    public long ConsecutiveFolio { get; set; }
    public int? FleetFolio { get; set; }
    public bool Sync { get; set; }
    public Guid? InvoiceUuid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<OrderPayment> OrderPayments { get; set; }

    public Order()
    {
        OrderId = Guid.NewGuid();
        OrderDetails = [];
        OrderPayments = [];
        CreatedAt = DateTime.UtcNow;
        Status = "Created";
        TicketPrinted = false;
        HasDispatch = false;
        HasManyPayment = false;
        CountItem = 0;
        CountPrinted = 0;
        GasNGoId = 0;
        TicketReprintCountPanel = 0;
        Sync = true;
        IsCoalition = "N";
        Type = "Order";
        TransactionId = Guid.NewGuid().ToString();
    }

    public void AddDetail(Product product, decimal quantity, decimal price)
    {
        var detail = new OrderDetail(product, quantity, price);
        OrderDetails.Add(detail);
        CountItem = OrderDetails.Count;
        RecalculateTotal();
    }

    public void AddPayment(PaymentMethod paymentMethod, decimal amount)
    {
        var payment = new OrderPayment(paymentMethod, amount);
        OrderPayments.Add(payment);
        HasManyPayment = OrderPayments.Count > 1;
    }

    private void RecalculateTotal()
    {
        Total = OrderDetails.Sum(d => d.Total);
    }
} 