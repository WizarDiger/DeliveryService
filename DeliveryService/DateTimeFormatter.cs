using DeliveryService.Interfaces;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class DateTimeFormatter:IDateTimeFormatterService
    {
        public string Format(string dateTime)
        {
            var timeData =  dateTime.Split(' ');
            var date = timeData[0].Split("-");
            var time = timeData[1].Split(":");
            var resultDateTime = $"{date[0]}.{date[1]}.{date[2]} {time[0]}:{time[1]}:{time[2]}";
            return resultDateTime;
        }
    }
}
