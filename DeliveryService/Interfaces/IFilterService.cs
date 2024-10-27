using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Interfaces
{
	public interface IFilterService
	{
		void FilterData(int cityDisctrict, DateTime firstDeliveryTime);
	}
}
