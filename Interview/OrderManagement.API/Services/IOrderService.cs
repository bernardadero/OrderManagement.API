using OrderManagement.API.Models;

namespace OrderManagement.API.Services;

public interface IOrderService
{
    decimal ApplyDiscount(Order order, Customer customer, IEnumerable<Order> orderHistory);
    bool CanTransition(OrderStatus current, OrderStatus next);
    OrderAnalyticsDto GetAnalytics(IEnumerable<Order> orders);
}
