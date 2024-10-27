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
using DeliveryService.Services;
using DeliveryService.Validators;
namespace DeliveryService
{
	public class ServiceProviderFactory
	{
		public ServiceCollectionRegistrar Create(Settings settings)
		{
			//Для демонстрации batchSize равен 1 чтобы логи писались сразу
            var logger = new LoggerConfiguration().WriteTo.SQLite(settings.DbFilePath, batchSize: 1).CreateLogger();
            var services = new ServiceCollection();
			services.AddTransient<IFilterResultRepository,FilterResultRepository>();
			services.AddTransient<IDateTimeFormatter, DateTimeFormatter>();
			services.AddTransient<IFilterResultService, FilterResultService>();
			services.AddTransient<FindCommand>();
			services.AddTransient<FindCommandValidator>();
			services.AddSingleton<ILogger>(logger);
			services.AddSingleton(settings);
			var registar = new ServiceCollectionRegistrar(services);
			return registar;
		}
	}
}
