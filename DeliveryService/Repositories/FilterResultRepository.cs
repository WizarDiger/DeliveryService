using DeliveryService.Interfaces;
using DeliveryService.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Repositories
{
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
		public List<(int, int, int, string)> SelectOrders(int districtId)
		{
			using (var connection = new SqliteConnection(settings.ConnectionString))
			{
				connection.Open();
				using var command = new SqliteCommand($@"SELECT * FROM Orders WHERE DistrictId = @districtId", connection)
				{
					Parameters =
					{
						new("@districtId", districtId)
					}
				};
				var reader = command.ExecuteReader();
				var ordersData = new List<(int, int, int, string)>();
				while (reader.Read())
				{
					ordersData.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3)));
				}

				return ordersData;
			}
		}
	}
}
