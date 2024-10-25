// See https://aka.ms/new-console-template for more information
using DeliveryService;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

//DbManager dbManager = new DbManager();
//dbManager.CreateResultTable("Data Source=ordersdata.db");
var serviceProvider = new ServiceProviderFactory().Create();
IFilterService? filterService = serviceProvider.GetService<IFilterService>();
IValidatorService? validatorService = serviceProvider.GetService<IValidatorService>();
var filterData = validatorService.Validate(serviceProvider);
filterService.FilterData(filterData.Item1, filterData.Item2, serviceProvider);
//DateTime.Parse("14.10.2024 16:20:06")