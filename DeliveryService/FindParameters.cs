using Spectre.Console.Cli;

namespace DeliveryService;

public class FindParameters : CommandSettings
{
    [CommandArgument(0,"[cityRegion]")]
    public string? CityRegion { get; init; }
    [CommandArgument(1,"[firstDeliveryDateTime]")]
    public string? FirstDeliveryDateTime { get; init; }
}
