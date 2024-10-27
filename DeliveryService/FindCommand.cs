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
namespace DeliveryService
{
    public class FindCommand:Command<FindParameters>
    {
        private readonly ILogger logger;
        private readonly IDateTimeFormatterService dateTimeFormatterService;
        private readonly IFilterService filterService;
        private readonly string connectionString;
        public FindCommand(IFilterService filterService,IDateTimeFormatterService dateTimeformatterService, ILogger logger)
        {
            this.logger = logger;
            this.dateTimeFormatterService = dateTimeformatterService;
            this.filterService = filterService;
            this.connectionString = "Data Source=ordersdata.db";
        }
        public override ValidationResult Validate(CommandContext context, FindParameters? parameters) 
        {
            Regex cityRegionRegex = new Regex(@"^[0-9]+$");

            if (parameters.cityRegion == null || parameters.firstDeliveryDateTime == null)
            {
                logger.Error("Значение одного или нескольких параметров равно null");
                return ValidationResult.Error("Значение одного или нескольких параметров равно null");
            }

            if (!cityRegionRegex.Match(parameters.cityRegion).Success)
            {
                logger.Error("Некорректный параметр {cityRegion}", parameters.cityRegion);
                return ValidationResult.Error("Идентификатор региона должен быть целым числом");
            }

            Regex firstDeliveryDateTimeRegex = new Regex("^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}$");
            if (!firstDeliveryDateTimeRegex.Match(parameters.firstDeliveryDateTime).Success)
            {
                logger.Error("Некорректный параметр {firstDeliveryDateTime}", parameters.firstDeliveryDateTime);
                return ValidationResult.Error("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                try
                {
                    DateTime.Parse(dateTimeFormatterService.Format(parameters.firstDeliveryDateTime));
                }
                catch
                {
                    logger.Error("Некорректный параметр {firstDeliveryDateTime}", parameters.firstDeliveryDateTime);
                    return ValidationResult.Error("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
                }
                return ValidationResult.Success();
            }
        }
        public override int Execute(CommandContext context, FindParameters parameters)
        {
            filterService.FilterData(int.Parse(parameters.cityRegion),DateTime.Parse(parameters.firstDeliveryDateTime),connectionString);
            logger.Information("Успешная фильтрация");
            return 0;
        }
    }
}
