using DeliveryService.Interfaces;
using DeliveryService.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Repositories
{
	public class FilterRepository:IFilterService
	{
		public void FilterData(int cityDistrict,DateTime firstDeliveryTime, ServiceProvider serviceProvider)
		{
			var connectionString = "Data Source=ordersdata.db";
			var orders = SelectOrders(connectionString, cityDistrict);
			var filteredOrders = FilterFile(orders, firstDeliveryTime, firstDeliveryTime.AddMinutes(30.0), serviceProvider);
			Console.WriteLine("Отфильтрованные записи");
			PrintResult(filteredOrders);
			SaveResult(filteredOrders, connectionString);
		}
		private void SaveResult(List<Order> orders, string connectionString)
		{
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
				var command = new SqliteCommand($@"DELETE FROM Results", connection);
                var reader = command.ExecuteReader();
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


            
        }
		private List<(int, int, int, string)> SelectOrders(string connectionString, int districtId)
		{
			using (var connection = new SqliteConnection(connectionString))
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
					//Console.WriteLine(reader.GetString(3));
				}

				return ordersData;
			}
		}
		private List<Order> FilterFile(List<(int, int, int, string)> ordersData, DateTime startTime, DateTime endTime, ServiceProvider serviceProvider)
		{
			IDateTimeFormatterService? dateTimeFormatterService = serviceProvider.GetService<IDateTimeFormatterService>();
			var filteredData = new List<Order>();
			foreach (var order in ordersData)
			{
				var dateTimeString = dateTimeFormatterService.Format(order.Item4);
				var dateTime = DateTime.Parse(dateTimeString);
				if (dateTime > startTime && dateTime < endTime)
				{
					filteredData.Add(new Order() { Id = order.Item1, Weight = order.Item2, DisctrictId = order.Item3, DeliveryTime = dateTime });
				}
			}
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
