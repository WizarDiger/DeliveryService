using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace DeliveryService
{
	internal class ServiceProviderFactory
	{
		public ServiceProvider Create()
		{
			var services = new ServiceCollection();
			services.AddTransient<IFilterService,FilterRepository>();
			services.AddTransient<IValidatorService,Validator>();
			services.AddTransient<IDateTimeFormatterService, DateTimeFormatter>();
			var serviceProvier = services.BuildServiceProvider();
			return serviceProvier;
		}
	}
}
