using ProductStoreApp.Models;
using ProductStoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProductStoreApp.Controllers
{
    public class ProductsController : Controller
    {
        public IProductsDataService Repository { get; set; }

        public ProductsController(IProductsDataService dataService)
        {
            Repository = dataService;
        }

        public IActionResult ShowCreateOne()
        {
            return View();
        }

        public IActionResult AddNewProduct(ProductModel model)
        {
            model.Id = Repository.Insert(model);
            if (model.Id >= 0)
                return View("ShowOneProduct", model);
            else
            {
                Console.WriteLine("Product was not added!");
                return View("Index");
            }
        }

        public IActionResult ShowEditForm(int Id)
        {
            return View(Repository.GetProductById(Id));
        }

        public IActionResult RemoveProduct(int Id)
        {
            Repository.Delete(Repository.GetProductById(Id));
            return View("Index", Repository.AllProducts());
        }

        public IActionResult ProcessEdit(ProductModel product)
        {
            Repository.Update(product);
            return View("Index", Repository.AllProducts());
        }

        public IActionResult ProcessEditReturnPartial(ProductModel product)
        {
            Repository.Update(product);
            return PartialView("_productCard", product);
        }

        public IActionResult ShowOneProduct(int Id)
        {
            return View(Repository.GetProductById(Id));
        }

        public IActionResult ShowOneProductJSON(int Id)
        {
            return Json(Repository.GetProductById(Id));
        }

        public IActionResult Index()
        {
            return View(Repository.AllProducts());
        }

        public IActionResult SearchResults(string searchTerm)
        {
            List<ProductModel> productList = Repository.SearchProducts(searchTerm);
            return View("Index", productList);
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            ViewBag.name = "Marc";
            ViewBag.secretNumber = 13;
            return View();
        }
    }
}
