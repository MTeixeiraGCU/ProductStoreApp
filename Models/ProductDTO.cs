using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreApp.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceString { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Tax { get; set; }

        public ProductDTO(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            PriceString = string.Format("{0:C}", price);
            Description = description;
            ShortDescription = description.Length <= 25 ? description : description.Substring(0, 25);
            Tax = price * 0.08M;
        }
    }
}
