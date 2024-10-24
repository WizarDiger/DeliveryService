using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeliveryService.Models
{
	public class Order
	{
		public int Id { get; set; }
		public float Weight { get; set; }
		public int DisctrictId { get; set; }
		public DateTime DeliveryTime { get; set; }
	}
}
