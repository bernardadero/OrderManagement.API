namespace OrderManagement.API.Models;

public class OrderAnalyticsDto
{
    public decimal AverageOrderValue { get; set; }
    public TimeSpan AverageFulfillmentTime { get; set; }
    public int TotalOrders { get; set; }
}
