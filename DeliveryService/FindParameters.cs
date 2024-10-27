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
        [CommandOption("--cityRegion")]
        public string cityRegion { get; init; }
        [CommandOption("--firstDeliveryDateTime")]
        public string firstDeliveryDateTime { get; init; }
    }
}
