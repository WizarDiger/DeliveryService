using DeliveryService.Interfaces;
using DeliveryService.Models;
using Serilog;
using Spectre.Console;

namespace DeliveryService.Services;

public class FilterResultService:IFilterResultService
{
    private readonly IFilterResultRepository filterResultRepository;
    private readonly ILogger logger;

    public FilterResultService(IFilterResultRepository filterResultRepository, ILogger logger)
    {
        this.filterResultRepository = filterResultRepository;
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
        var resultTable = new Table();
        resultTable.AddColumn("Id");
        resultTable.AddColumn("Weight");
        resultTable.AddColumn("CityDistrict");
        resultTable.AddColumn("DeliveryDate");
        foreach (var order in orders)
        {              
            resultTable.AddRow(order.Id.ToString(), order.Weight.ToString(), order.DisctrictId.ToString(),order.DeliveryTime.ToString());
            AnsiConsole.Write(resultTable);
        }
        if (orders.Count == 0)
        {
            Console.WriteLine("Записей не найдено");
        }
    }
}
