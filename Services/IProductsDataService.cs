using ProductStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreApp.Services
{
    public interface IProductsDataService
    {
        List<ProductModel> AllProducts();
        List<ProductModel> SearchProducts(string searchTerm);
        ProductModel GetProductById(int id);
        int Insert(ProductModel product);
        bool Delete(ProductModel product);
        int Update(ProductModel product);
    }
}
