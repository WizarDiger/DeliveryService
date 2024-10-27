
namespace DeliveryService.Interfaces;

public interface IFindCommandValidator
{
    public Spectre.Console.ValidationResult Validate(FindParameters? parameters);
}
