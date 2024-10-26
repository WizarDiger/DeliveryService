using DeliveryService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class Validator:IValidatorService
    {
        public (int,DateTime) Validate(ServiceProvider serviceProvider)
        {
            IDateTimeFormatterService? dateTimeFormatterService = serviceProvider.GetRequiredService<IDateTimeFormatterService>();
            Console.WriteLine("Введите id района");
            var cityDistrict = Console.ReadLine();
            Console.WriteLine("Введите время первого заказа в формате yyyy-MM-dd HH:mm:ss");
            var firstDeliveryDateTime = Console.ReadLine();
            ValidateDistrictId(cityDistrict,serviceProvider);
            ValidateDateTime(firstDeliveryDateTime,serviceProvider);
            var dateTimeString = dateTimeFormatterService.Format(firstDeliveryDateTime);
            return (int.Parse(cityDistrict), DateTime.Parse(dateTimeString));
            
        }
        public void ValidateDistrictId(string districtId, ServiceProvider serviceProvider)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (!regex.Match(districtId).Success)
            {
                Console.WriteLine("Идентификатор региона должен быть целым числом");
                throw new Exception("Идентификатор региона должен быть целым числом");
            }
        }
        public void ValidateDateTime(string dateTime, ServiceProvider serviceProvider) 
        {
            Regex regex = new Regex("^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}$");
            IDateTimeFormatterService? dateTimeFormatterService = serviceProvider.GetRequiredService<IDateTimeFormatterService>();
            if (!regex.Match(dateTime).Success)
            {
                Console.WriteLine("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
                throw new Exception("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                try
                {
                    DateTime.Parse(dateTimeFormatterService.Format(dateTime));
                }
                catch
                {
                    Console.WriteLine("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
                    throw new Exception("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
                }
            }
        }
    }
}
