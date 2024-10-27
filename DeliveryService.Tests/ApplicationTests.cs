using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using Spectre.Console.Cli;
using Spectre.Console;
using System.Data;
namespace DeliveryService.Tests
{
    public class ApplicationTests
    {
        private readonly string connectionString = $"Data Source=ordersdata.db";
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TestOrders()
        {
            var settings = new Settings() { DbFilePath = "ordersdata.db", ConnectionString = "Data Source=ordersdata.db" };
            var registrar = new ServiceProviderFactory().Create(settings);
            var app = new CommandApp<FindCommand>(registrar);
            var arguments = new[] {"1", "2024-10-24 13:13:13" };
            app.Run(arguments);
            using (var connection = new SqliteConnection(settings.ConnectionString))
            {
                connection.Open();
                using var command = new SqliteCommand($@"SELECT * FROM Orders WHERE _id = @id", connection)
                {
                    Parameters =
                    {
                        new("@id", 1)
                    }
                };
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Assert.AreEqual(reader.GetInt32(1), 5);
                    Assert.AreEqual(reader.GetInt32(2), 1);
                    Assert.AreEqual(reader.GetString(3), "2024-10-24 16:07:15");
                }
            }
        }
        [Test]
        public void TestResults()
        {
            var settings = new Settings() { DbFilePath = "ordersdata.db", ConnectionString = "Data Source=ordersdata.db" };
            var registrar = new ServiceProviderFactory().Create(settings);
            var app = new CommandApp<FindCommand>(registrar);
            var arguments = new[] { "1", "2024-10-24 13:13:13" };
            app.Run(arguments);
            using (var connection = new SqliteConnection(settings.ConnectionString))
            {
                connection.Open();
                using var command = new SqliteCommand($@"SELECT * FROM Results WHERE _id = @id", connection)
                {
                    Parameters =
                    {
                        new("@id", 9)
                    }
                };
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Assert.AreEqual(reader.GetInt32(1), 6);
                    Assert.AreEqual(reader.GetInt32(2), 15);
                    Assert.AreEqual(reader.GetInt32(3), 1);
                    Assert.AreEqual(reader.GetString(4), "2024-10-24 13:23:15");
                }
            }
        }
    }
}
