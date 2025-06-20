namespace OrderManagement.API.Models;

public enum CustomerSegment { Regular, VIP, New }

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CustomerSegment Segment { get; set; }
}
