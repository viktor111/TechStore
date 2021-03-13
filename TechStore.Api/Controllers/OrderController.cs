using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Models.Models;

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CartProduct> _cartProductRepository;

        public OrderController
            (
                IRepository<Order> orderRepository,
                IRepository<CartProduct> productRepository
            )
        {
            _orderRepository = orderRepository;
            _cartProductRepository = productRepository;
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post(OrderModel model)
        {
            int cartId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "cartId").Value);
            
            List<CartProduct> products = await _cartProductRepository.GetListByProperty(p => p.CartId == cartId);

            List<Product> p = products.Select(cp => cp.Product).ToList();


            return Ok();
        }
    }
}
