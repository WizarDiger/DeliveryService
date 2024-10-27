using DeliveryService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Interfaces
{
	public interface IFilterResultRepository
	{
        void SaveResult(List<Order> orders);
        List<Order> SelectOrders(int districtId, string firstDeliveryDateTime);

    }
}
