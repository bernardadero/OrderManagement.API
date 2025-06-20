using OrderManagement.API.Models;

namespace OrderManagement.API.Services;

public class OrderService : IOrderService
{
    public decimal ApplyDiscount(Order order, Customer customer, IEnumerable<Order> history)
    {
        decimal totalHistory = history.Sum(o => o.TotalAmount);
        int count = history.Count();

        return customer.Segment switch
        {
            CustomerSegment.VIP when totalHistory > 10000 => order.TotalAmount * 0.85m,
            CustomerSegment.Regular when count > 5 => order.TotalAmount * 0.95m,
            _ => order.TotalAmount
        };
    }

    public bool CanTransition(OrderStatus current, OrderStatus next) => (current, next) switch
    {
        (OrderStatus.Pending, OrderStatus.Processing) => true,
        (OrderStatus.Processing, OrderStatus.Shipped) => true,
        (OrderStatus.Shipped, OrderStatus.Delivered) => true,
        (_, OrderStatus.Cancelled) => true,
        _ => false
    };

    public OrderAnalyticsDto GetAnalytics(IEnumerable<Order> orders)
    {
        int totalOrders = orders.Count();
        decimal totalValue = orders.Sum(o => o.TotalAmount);
        double totalFulfillmentTime = orders
            .Where(o => o.FulfilledAt.HasValue)
            .Sum(o => (o.FulfilledAt.Value - o.CreatedAt).TotalMinutes);

        int fulfilledCount = orders.Count(o => o.FulfilledAt.HasValue);

        return new OrderAnalyticsDto
        {
            TotalOrders = totalOrders,
            AverageOrderValue = totalOrders > 0 ? totalValue / totalOrders : 0,
            AverageFulfillmentTime = fulfilledCount > 0
                ? TimeSpan.FromMinutes(totalFulfillmentTime / fulfilledCount)
                : TimeSpan.Zero
        };
    }
}
