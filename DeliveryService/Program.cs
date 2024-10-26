// See https://aka.ms/new-console-template for more information
using DeliveryService;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.CommandLine.Parsing;
using Serilog;
using Spectre;
using Spectre.Console;
using Spectre.Console.Cli;
Console.OutputEncoding = System.Text.Encoding.UTF8;
var logger = new LoggerConfiguration().WriteTo.SQLite("ordersdata.db").CreateLogger();
var registrar = new ServiceProviderFactory().Create(logger);
AppDomain.CurrentDomain.ProcessExit += (_, _) => logger.Dispose();
var connectionString = "Data Source=ordersdata.db";
while (true)
{
    try
    {
        var app = new CommandApp<FindCommand>(registrar);
        AnsiConsole.Write("> ");
        var input = Console.ReadLine();
        var arguments = CommandLineStringSplitter.Instance.Split(input);
        app.Run(arguments);

    }
    catch
    {
    }
}
//DateTime.Parse("14.10.2024 16:20:06")