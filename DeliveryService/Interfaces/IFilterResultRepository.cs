using DeliveryService.Models;

namespace DeliveryService.Interfaces;

	public interface IFilterResultRepository
	{
    void SaveResult(List<Order> orders);
    List<Order> SelectOrders(int districtId, string firstDeliveryDateTime);

}
