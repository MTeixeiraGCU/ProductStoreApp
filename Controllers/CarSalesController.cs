using ProductStoreApp.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreApp.Controllers
{
    public class CarSalesController : Controller
    {
        public IActionResult Index()
        {
            List<CarModel> cars = new List<CarModel>();

            for(int i = 0;i< 100;i++)
            {
                cars.Add(new Faker<CarModel>()
                    .RuleFor(p => p.Id, i + 5)
                    .RuleFor(p => p.Name, f => f.Commerce.Product())
                    .RuleFor(p => p.ManufactureDate, f => f.Date.Past())
                );
            }
            return View(cars);
        }
    }
}
