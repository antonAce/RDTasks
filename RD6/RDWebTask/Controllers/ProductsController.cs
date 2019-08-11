using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OrderManagerBLL.Interfaces;
using OrderManagerBLL.DTO;

namespace RDWebTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public IProductService ProductService { get; }

        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductDTO> Get()
        {
            return ProductService.ListProducts();
        }

        // POST: api/products

        // Header:
        // Content-Type: application/json

        // Body:
        //{
        //    "gtin": "...",
        //    "name": "...",
        //    "description": "...",
        //    "price": 0.0....
        //}
        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            if (product == null)
                return BadRequest("Product's data is empty!");

            if (ProductService.GetProductByGTIN(product.GTIN) != null)
                return BadRequest($"The product with GTIN '{product.GTIN}' already exists!");
            else
            {
                ProductService.AddProduct(product);
                return Ok($"Product with GTIN '{product.GTIN}' was added successfully!");
            }
        }
    }
}
