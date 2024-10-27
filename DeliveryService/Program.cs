using DeliveryService;
using System.CommandLine.Parsing;
using Spectre.Console;
using Spectre.Console.Cli;
Console.OutputEncoding = System.Text.Encoding.UTF8;
var settings = new Settings() {DbFilePath= "ordersdata.db",ConnectionString= "Data Source=ordersdata.db" };
var registrar = new ServiceProviderFactory().Create(settings);
while (true)
{
    try
    {
        var app = new CommandApp<FindCommand>(registrar);
        AnsiConsole.Write("> ");
        var input = Console.ReadLine();
        var arguments = CommandLineStringSplitter.Instance.Split(input);
        
        app.Configure(config =>
        {
            config.PropagateExceptions();
        });
        app.Run(arguments);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
