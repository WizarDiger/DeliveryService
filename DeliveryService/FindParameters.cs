using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class FindParameters : CommandSettings
    {
        [CommandArgument(0,"[cityRegion]")]
        public string? CityRegion { get; init; }
        [CommandArgument(1,"[firstDeliveryDateTime]")]
        public string? FirstDeliveryDateTime { get; init; }
    }
}
