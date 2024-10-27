using DeliveryService.Interfaces;
using DeliveryService.Models;
using Microsoft.Data.Sqlite;
using Serilog;

namespace DeliveryService.Repositories;

public class FilterResultRepository:IFilterResultRepository
{
	private readonly IDateTimeFormatter dateTimeFormatter;
    private readonly ILogger logger;
	private readonly Settings settings;
	public FilterResultRepository(IDateTimeFormatter dateTimeFormatter, ILogger logger, Settings settings)
	{
		this.dateTimeFormatter = dateTimeFormatter;
		this.logger = logger;
		this.settings = settings;
	}

	public void SaveResult(List<Order> orders)
	{
            using (var connection = new SqliteConnection(settings.ConnectionString))
            {
                connection.Open();
			foreach (var order in orders)
			{

				using var saveResultCommand = new SqliteCommand($@"INSERT INTO ""Results"" (""OrderId"",""Weight"", ""DistrictId"", ""DeliveryTime"") VALUES (@orderId,@weight,@districtId,@deliveryTime)", connection)
				{
					Parameters =
					{
						new("@orderId",order.Id),
						new("@weight",order.Weight),
                            new("@districtId",order.DisctrictId),
                            new("@deliveryTime",order.DeliveryTime)
					}
				};
				saveResultCommand.ExecuteNonQuery();
			}
            }
		logger.Information("Результат сохранён в базу данных");
        }
	public List<Order> SelectOrders(int districtId, string firstDeliveryDateTime)
	{
		using (var connection = new SqliteConnection(settings.ConnectionString))
		{
			connection.Open();
			using var command = new SqliteCommand($@"SELECT * FROM Orders WHERE DistrictId = @districtId AND DeliveryTime BETWEEN @firstDeliveryDateTime AND DATETIME(@firstDeliveryDateTime,""30 minutes"")", connection)
			{
				Parameters =
				{
					new("@districtId", districtId),

                        new("@firstDeliveryDateTime", firstDeliveryDateTime)
                    }
			};
			var reader = command.ExecuteReader();
			var ordersData = new List<Order>();
			while (reader.Read())
			{
				ordersData.Add(new Order() { Id = reader.GetInt32(0), Weight = reader.GetInt32(1), DisctrictId = reader.GetInt32(2), DeliveryTime = dateTimeFormatter.Format(reader.GetString(3)) });
			}
                return ordersData;
		}
	}
}
