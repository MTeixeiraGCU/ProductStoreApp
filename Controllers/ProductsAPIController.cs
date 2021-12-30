using ProductStoreApp.Models;
using ProductStoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Description;

namespace ProductStoreApp.Controllers
{

    [ApiController]
    [Route("api")]
    public class ProductsAPIController : ControllerBase
    {
        ProductDAO repository;

        public ProductsAPIController()
        {
            repository = new ProductDAO();
        }

        [HttpGet]
        [ResponseType(typeof(List<ProductDTO>))]
        public IEnumerable<ProductDTO> Index()
        {
            List<ProductModel> productList = repository.AllProducts();
            IEnumerable<ProductDTO> productDTOList = from p in productList
                                                     select
                                                     new ProductDTO(p.Id, p.Name, p.Price, p.Description);
            return productDTOList;
        }

        [HttpGet("searchresults/{searchTerm}")]
        public IEnumerable<ProductDTO> SearchResults(string searchTerm)
        {
            List<ProductModel> productList = repository.SearchProducts(searchTerm);
            IEnumerable<ProductDTO> productDTOList = from p in productList
                                                     select
                                                     new ProductDTO(p.Id, p.Name, p.Price, p.Description);
            return productDTOList;
        }

        [HttpGet("showoneproduct/{id}")]
        [ResponseType(typeof(ProductDTO))]
        public ActionResult <ProductDTO> ShowOneProduct(int Id)
        {
            ProductModel product = repository.GetProductById(Id);
            ProductDTO productDTO = new ProductDTO(product.Id, product.Name, product.Price, product.Description);
            return productDTO;
        }

        [HttpPut("processedit")]
        [ResponseType(typeof(List<ProductDTO>))]
        public IEnumerable<ProductDTO> ProcessEdit(ProductModel product)
        {
            repository.Update(product);
            List<ProductModel> productList = repository.AllProducts();
            IEnumerable<ProductDTO> productDTOList = from p in productList
                                                     select
                                                     new ProductDTO(p.Id, p.Name, p.Price, p.Description);
            return productDTOList;
        }

        [HttpPut("processeditreturnoneitem")]
        [ResponseType(typeof(ProductDTO))]
        public ActionResult <ProductDTO> ProcessEditReturnOneItem(ProductModel product)
        {
            repository.Update(product);
            ProductModel updateProduct = repository.GetProductById(product.Id);
            ProductDTO productDTO = new ProductDTO(updateProduct.Id, updateProduct.Name, updateProduct.Price, updateProduct.Description);
            return productDTO;
        }

    }
}
