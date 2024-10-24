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
			var serviceProvier = services.BuildServiceProvider();
			return serviceProvier;
		}
	}
}
