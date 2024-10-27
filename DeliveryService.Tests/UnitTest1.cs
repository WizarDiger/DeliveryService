using DeliveryService;
using System.CommandLine.Parsing;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DeliveryService.Tests
{
    public class Tests
    {
        private FindCommand findCommand;
        private readonly IDateTimeFormatterService dateTimeFormatterService;
        private readonly IFilterService filterService;
        private readonly Serilog.ILogger logger;
        private readonly Spectre.Console.Cli.CommandContext commandContext;
        private ServiceCollectionRegistrar registrar;
        private FindParameters findParameters;
        [SetUp]
        public void Setup()
        {
            this.findCommand = new FindCommand(filterService,dateTimeFormatterService,logger);
            this.findParameters = findParameters;
            this.registrar = new ServiceProviderFactory().Create(logger);
        }

        [Test]
        public void Test1()
        {

            var app = new CommandApp<FindCommand>(registrar);

            // When
            var result = Record.Exception(() => app.Run(new[] { "--cityRegion", "1", "--firstDeliveryDateTime", "2024-10-24 13:13:13" }));

            // Then
            result.ShouldBeOfType<CommandRuntimeException>().And(e =>
            {
                e.Message.ShouldBe("-");
            });
        }
    }
}