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

        public void FilterData(int cityDistrict, DateTime firstDeliveryTime)
        {
            var orders = filterResultRepository.SelectOrders(cityDistrict);
            var filteredOrders = FilterFile(orders, firstDeliveryTime, firstDeliveryTime.AddMinutes(30.0));
            Console.WriteLine("Отфильтрованные записи");
            PrintResult(filteredOrders);
            filterResultRepository.SaveResult(filteredOrders);
        }
        private List<Order> FilterFile(List<(int, int, int, string)> ordersData, DateTime startTime, DateTime endTime)
        {
            var filteredData = new List<Order>();
            foreach (var order in ordersData)
            {
                var dateTimeString = dateTimeFormatter.Format(order.Item4);
                var dateTime = DateTime.Parse(dateTimeString);
                if (dateTime > startTime && dateTime < endTime)
                {
                    filteredData.Add(new Order() { Id = order.Item1, Weight = order.Item2, DisctrictId = order.Item3, DeliveryTime = dateTime });
                }
            }
            logger.Information("Данные были отфильтрованы");
            return filteredData;
        }
        private void PrintResult(List<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.Id + " " + order.Weight + " " + order.DisctrictId + " " + order.DeliveryTime);
            }
        }
    }

}
