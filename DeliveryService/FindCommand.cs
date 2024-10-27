using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Spectre;
using Spectre.Console;
using Spectre.Console.Cli;
using Serilog;
using DeliveryService.Validators;
namespace DeliveryService
{
    public class FindCommand:Command<FindParameters>
    {
        private readonly ILogger logger;
        private readonly IDateTimeFormatter dateTimeFormatterService;
        private readonly IFilterResultService filterService;
        private readonly FindCommandValidator findCommandValidator;
        public FindCommand(IFilterResultService filterService,IDateTimeFormatter dateTimeformatterService, ILogger logger, FindCommandValidator findCommandValidator)
        {
            this.logger = logger;
            this.dateTimeFormatterService = dateTimeformatterService;
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
            logger.Information("Успешная фильтрация");
            return 0;
        }
    }
}
