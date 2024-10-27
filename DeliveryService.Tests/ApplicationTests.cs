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
        public void Test()
        {
            var settings = new Settings() { DbFilePath = "ordersdata.db", ConnectionString = "Data Source=ordersdata.db" };
            var registrar = new ServiceProviderFactory().Create(settings);
            var app = new CommandApp<FindCommand>(registrar);
            var arguments = new[] {"1", "2024-10-24 13:13:13" };
            app.Run(arguments);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand($@"SELECT * FROM Orders WHERE DistrictId = 1", connection);
                var reader = command.ExecuteReader();
                Assert.AreEqual(int.Parse(reader.GetString(1)), 5);
                Assert.AreEqual(int.Parse(reader.GetString(2)), 1);
                Assert.AreEqual(reader.GetString(0), "2024 - 10 - 24 16:07:15");
            }
        }
}
}
