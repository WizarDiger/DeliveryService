using DeliveryService.Interfaces;

namespace DeliveryService;

public class DateTimeFormatter:IDateTimeFormatter
{
    public DateTime Format(string dateTime)
    {
        var timeData =  dateTime.Split(' ');
        var date = timeData[0].Split("-");
        var time = timeData[1].Split(":");
        var resultDateTime = $"{date[0]}.{date[1]}.{date[2]} {time[0]}:{time[1]}:{time[2]}";
        return DateTime.Parse(resultDateTime);
    }
}
