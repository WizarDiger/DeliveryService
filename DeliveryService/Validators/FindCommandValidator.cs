using DeliveryService.Interfaces;
using Serilog;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace DeliveryService.Validators;

public class FindCommandValidator:IFindCommandValidator
{
    private readonly ILogger logger;
    public FindCommandValidator(ILogger logger)
    {
        this.logger = logger;
    }
    public ValidationResult Validate(FindParameters? parameters)
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
        try
        {
            DateTime.Parse(parameters.FirstDeliveryDateTime);
        }
        catch
        {
            logger.Error("Некорректный параметр {firstDeliveryDateTime}", parameters.FirstDeliveryDateTime);
            return ValidationResult.Error("Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
        }
        return ValidationResult.Success();

    }

}
