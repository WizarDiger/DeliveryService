using DeliveryService.Interfaces;
using DeliveryService.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Repositories
{
	public class FilterRepository:IFilterService
	{
		public void FilterData(int districtId,DateTime startTime, DateTime endTime)
		{
			var connectionString = "Data Source=ordersdata.db";
			var orders = SelectOrders(connectionString, districtId);
			var filteredOtders = FilterFile(orders, startTime,endTime);
			PrintResult(filteredOtders);
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
		private List<Order> FilterFile(List<(int, int, int, string)> ordersData, DateTime startTime, DateTime endTime)
		{
			var filteredData = new List<Order>();
			foreach (var order in ordersData)
			{
				var timeData = order.Item4.Split(' ');
				var date = timeData[0].Split("-");
				var time = timeData[1].Split(":");
				var dateTime = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
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
