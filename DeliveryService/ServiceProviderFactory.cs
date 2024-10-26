using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre;
using Serilog;
namespace DeliveryService
{
	public class ServiceProviderFactory
	{
		public ServiceCollectionRegistrar Create(ILogger logger)
		{
			var services = new ServiceCollection();
			services.AddTransient<IFilterService,FilterRepository>();
			services.AddTransient<IValidatorService,Validator>();
			services.AddTransient<IDateTimeFormatterService, DateTimeFormatter>();
			services.AddSingleton(logger);
			var registar = new ServiceCollectionRegistrar(services);
			return registar;
		}
	}
}
