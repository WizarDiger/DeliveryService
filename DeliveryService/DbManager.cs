using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
	public class DbManager
	{
		public void InitDb()
		{
			 using (var connection = new SqliteConnection("Data Source=ordersdata.db"))
            {
                connection.Open();
            }
		}
		public void AddToDb(string connectionString)
		{
			using (var connection = new SqliteConnection(connectionString))
			{
				connection.Open();

				SqliteCommand command = new SqliteCommand();
				command.Connection = connection;
				command.CommandText = "CREATE TABLE Orders(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Weight INTEGER NOT NULL, DistrictId INTEGER NOT NULL, DeliveryTime TEXT NOT NULL)";
				command.ExecuteNonQuery();

				Console.WriteLine("Таблица Orders создана");
			}
		}
	}
}
