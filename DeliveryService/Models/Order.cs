
namespace DeliveryService.Models;

public class Order
{
	public int Id { get; set; }
	public float Weight { get; set; }
	public int DisctrictId { get; set; }
	public DateTime DeliveryTime { get; set; }
}
