namespace OrderManagement.API.Models;

public enum OrderStatus { Pending, Processing, Shipped, Delivered, Cancelled }

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FulfilledAt { get; set; }
}
