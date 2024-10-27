
using Serilog;
using DeliveryService.Validators;
using NSubstitute;
namespace DeliveryService.Tests;

public class FindCommandValidatorTests
{
    private FindCommandValidator validator;
    [SetUp]
    public void Setup()
    {
        this.validator = new FindCommandValidator(new DateTimeFormatter(), Substitute.For<ILogger>());    
    }

    [Test]
    public void Success()
    {
        var parameters = new FindParameters { CityRegion = "1",FirstDeliveryDateTime = "2024-10-24 13:13:13" };
        var result = validator.Validate(parameters);
        Assert.AreEqual(result.Successful, true);
        Assert.AreEqual(result.Message, null);
    }
    [Test]
    public void FailNoArgumentsGiven()
    {
        var parameters = new FindParameters { CityRegion = null, FirstDeliveryDateTime = null };
        var result = validator.Validate(parameters);
        Assert.AreEqual(result.Successful, false);
        Assert.AreEqual(result.Message, "Значение одного или нескольких параметров равно null");
    }
    [Test]
    public void FailOneArgumentIsNull()
    {
        var parameters = new FindParameters { CityRegion = "1", FirstDeliveryDateTime = null };
        var result = validator.Validate(parameters);
        Assert.AreEqual(result.Successful, false);
        Assert.AreEqual(result.Message, "Значение одного или нескольких параметров равно null");
    }
    [Test]
    public void FailInvalidDateTime()
    {
        var parameters = new FindParameters { CityRegion = "1", FirstDeliveryDateTime = "13513-5315 135 12:12:12" };
        var result = validator.Validate(parameters);
        Assert.AreEqual(result.Successful, false);
        Assert.AreEqual(result.Message, "Дата первого заказа должна быть в формате yyyy-MM-dd HH:mm:ss");
    }
    [Test]
    public void FailInvalidCityDistrict()
    {
        var parameters = new FindParameters { CityRegion = "aaa", FirstDeliveryDateTime = "2024-10-24 13:13:13" };
        var result = validator.Validate(parameters);
        Assert.AreEqual(result.Successful, false);
        Assert.AreEqual(result.Message, "Идентификатор региона должен быть целым числом");
    }  
}