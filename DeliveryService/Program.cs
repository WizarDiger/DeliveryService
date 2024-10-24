// See https://aka.ms/new-console-template for more information
using DeliveryService;
using DeliveryService.Interfaces;
using DeliveryService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceProviderFactory().Create();
IFilterService? filterService = serviceProvider.GetRequiredService<IFilterService>();
filterService.FilterData(1, DateTime.Parse("14.10.2024 16:20:06"), DateTime.Parse("27.10.2024 16:20:06"));