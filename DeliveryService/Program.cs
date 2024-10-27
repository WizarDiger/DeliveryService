using DeliveryService;
using System.CommandLine.Parsing;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;
Console.OutputEncoding = System.Text.Encoding.UTF8;
var logger = new LoggerConfiguration().WriteTo.SQLite("ordersdata.db", batchSize:1).CreateLogger();
var registrar = new ServiceProviderFactory().Create(logger);
AppDomain.CurrentDomain.ProcessExit += (_, _) => logger.Dispose();
//var connectionString = "Data Source=ordersdata.db";
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
