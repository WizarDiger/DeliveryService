using DeliveryService.Interfaces;
using Spectre.Console;
using Spectre.Console.Cli;
using Serilog;
using DeliveryService.Validators;
namespace DeliveryService;

public class FindCommand:Command<FindParameters>
{
    private readonly ILogger logger;
    private readonly IFilterResultService filterService;
    private readonly IFindCommandValidator findCommandValidator;
    public FindCommand(IFilterResultService filterService, ILogger logger, IFindCommandValidator findCommandValidator)
    {
        this.logger = logger;
        this.filterService = filterService;
        this.findCommandValidator = findCommandValidator;

    }
    public override ValidationResult Validate(CommandContext context, FindParameters? parameters) 
    {
        return findCommandValidator.Validate(parameters);
    }
    public override int Execute(CommandContext context, FindParameters parameters)
    {
        filterService.FilterData(int.Parse(parameters.CityRegion),parameters.FirstDeliveryDateTime);
        logger.Information($"Приняты параметры {parameters.CityRegion} {parameters.FirstDeliveryDateTime}");
        logger.Information("Успешная фильтрация");
        return 0;
    }
}
