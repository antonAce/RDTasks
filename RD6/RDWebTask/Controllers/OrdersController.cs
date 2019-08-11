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
    public class OrdersController : ControllerBase
    {
        public IOrderService OrderService { get; }

        public OrdersController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<OrderDTO> Get()
        {
            return OrderService.ListOrders();
        }

        // POST: api/orders
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO order)
        {
            if (order == null)
                return BadRequest("Order's data is empty!");
            else
            {
                OrderService.MakeOrder(order);
                return Ok($"Order was made successfully");
            }
        }

        // GET: api/orders/1
        [HttpGet("{id}", Name = "Get")]
        public OrderDTO Get(int id)
        {
            return OrderService.WatchOrder(id);
        }

        // GET: api/orders/1/products
        [HttpGet("{id}/products")]
        public IEnumerable<ProductDTO> GetProducts(int id)
        {
            return OrderService.WatchProductsOfOrder(id);
        }

        // PUT: api/orders/1/products
        [HttpPut("{id}/products")]
        public IActionResult Put(int id, [FromBody] ProductDTO product)
        {
            if (product == null)
                return BadRequest("Product's data is empty!");
            else
            {
                if (OrderService.WatchOrder(id) == null)
                    return BadRequest($"Order #{id} doesn't exist!");

                OrderService.AddProductToOrder(id, product);
                return Ok($"Product was added successfully!");
            }
        }
    }
}
