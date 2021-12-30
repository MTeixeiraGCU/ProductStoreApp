using ProductStoreApp.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreApp.Services
{
    public class HardCodedSampleDataRepository : IProductsDataService
    {

        static List<ProductModel> productList;

        public HardCodedSampleDataRepository()
        {
            productList = new List<ProductModel>();

            productList.Add(new ProductModel(1, "MousePad", 5.99m, "A square piece of plastic to make mousing easier."));
            productList.Add(new ProductModel(2, "Web Cam", 45.50m, "Enables you to attend more Zoom meetings."));
            productList.Add(new ProductModel(3, "4 TB USB Hard Drive", 130.00m, "Back up all of your work. You wont regret it."));
            productList.Add(new ProductModel(4, "Wireless Mouse", 15.99m, "Notebook mice really do not work very well. Do they?"));

            for (int i = 0; i < 100; i++)
            {
                productList.Add(new Faker<ProductModel>()
                    .RuleFor(p => p.Id, i + 5)
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(100))
                    .RuleFor(p => p.Description, f => f.Rant.Review())
                );
            }
        }

        public List<ProductModel> AllProducts()
        {
            return productList;
        }

        public bool Delete(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public int Update(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}
