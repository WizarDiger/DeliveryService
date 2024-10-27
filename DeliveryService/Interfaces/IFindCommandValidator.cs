using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace DeliveryService.Interfaces
{
    public interface IFindCommandValidator
    {
        public Spectre.Console.ValidationResult Validate(FindParameters? parameters);
    }
}
