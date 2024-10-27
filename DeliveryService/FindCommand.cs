﻿using System;
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
        private readonly IDateTimeFormatter dateTimeFormatterService;
        private readonly IFilterResultService filterService;
        public FindCommand(IFilterResultService filterService,IDateTimeFormatter dateTimeformatterService, ILogger logger)
        {
            this.logger = logger;
            this.dateTimeFormatterService = dateTimeformatterService;
            this.filterService = filterService;

        }
        public override ValidationResult Validate(CommandContext context, FindParameters? parameters) 
        {
            Regex cityRegionRegex = new Regex(@"^[0-9]+$");

            if (parameters.CityRegion == null || parameters.FirstDeliveryDateTime == null)
            {
                logger.Error("Значение одного или нескольких параметров равно null");
                return ValidationResult.Error("Значение одного или нескольких параметров равно null");
            }

            if (!cityRegionRegex.Match(parameters.CityRegion).Success)
            {
                logger.Error("Некорректный параметр {cityRegion}", parameters.CityRegion);
                return ValidationResult.Error("Идентификатор региона должен быть целым числом");
            }

            Regex firstDeliveryDateTimeRegex = new Regex("^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}$");
            if (!firstDeliveryDateTimeRegex.Match(parameters.FirstDeliveryDateTime).Success)
            {
                logger.Error("Некорректный параметр {firstDeliveryDateTime}", parameters.FirstDeliveryDateTime);
                return ValidationResult.Error("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                try
                {
                    DateTime.Parse(dateTimeFormatterService.Format(parameters.FirstDeliveryDateTime));
                }
                catch
                {
                    logger.Error("Некорректный параметр {firstDeliveryDateTime}", parameters.FirstDeliveryDateTime);
                    return ValidationResult.Error("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
                }
                return ValidationResult.Success();
            }
        }
        public override int Execute(CommandContext context, FindParameters parameters)
        {
            filterService.FilterData(int.Parse(parameters.CityRegion),DateTime.Parse(parameters.FirstDeliveryDateTime));
            logger.Information("Успешная фильтрация");
            return 0;
        }
    }
}
