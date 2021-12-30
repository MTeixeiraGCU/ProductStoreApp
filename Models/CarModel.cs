using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreApp.Models
{
    public class CarModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime ManufactureDate { get; set; }

        public CarModel()
        {
        }

        public CarModel(string name, int id, DateTime manufactureDate)
        {
            Name = name;
            Id = id;
            ManufactureDate = manufactureDate;
        }
    }
}
