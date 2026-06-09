using SQLite;

namespace FieldSalesForce.Models;

public enum OrderStatus
{
    Draft,
    Submitted,
    Synced,
    Failed
}

[Table("Orders")]
public class Order
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    public bool IsPendingSync { get; set; } = true;
    public decimal TotalAmount { get; set; }
}
