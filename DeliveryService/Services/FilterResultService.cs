using DeliveryService.Interfaces;
using DeliveryService.Models;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services
{
    public class FilterResultService:IFilterResultService
    {
        private readonly IFilterResultRepository filterResultRepository;
        private readonly IDateTimeFormatter dateTimeFormatter;
        private readonly ILogger logger;

        public FilterResultService(IFilterResultRepository filterResultRepository,IDateTimeFormatter dateTimeFormatter, ILogger logger)
        {
            this.filterResultRepository = filterResultRepository;
            this.dateTimeFormatter = dateTimeFormatter;
            this.logger = logger;
        }

        public void FilterData(int cityDistrict, string firstDeliveryDateTime)
        {
            var orders = filterResultRepository.SelectOrders(cityDistrict, firstDeliveryDateTime);
  
            Console.WriteLine("Отфильтрованные записи");
            PrintResult(orders);
            filterResultRepository.SaveResult(orders);
        }
    
        private void PrintResult(List<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.Id + " " + order.Weight + " " + order.DisctrictId + " " + order.DeliveryTime);
            }
            if (orders.Count == 0)
            {
                Console.WriteLine("Записей не найдено");
            }
        }
    }

}
