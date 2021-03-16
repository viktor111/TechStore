using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Api.Helpers;
using TechStore.Models.Models;

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CartProduct> _cartProductRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        //private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrderController
            (
                IRepository<Order> orderRepository,
                IRepository<CartProduct> cartProductRepository,
                //ILogger logger,
                IWebHostEnvironment webHostEnvironment,
                IRepository<Product> productRepository
            )
        {
            _orderRepository = orderRepository;
            _cartProductRepository = cartProductRepository;
            //_logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
        }

        [HttpDelete("{id:int}")]
        [Admin]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            try
            {
                var oldOrder = await _orderRepository.Get(id);

                if (oldOrder is null)
                {
                    return NotFound($"Could not find Order with id {id}");
                }

                await _orderRepository.Delete(oldOrder);

                if (await _productRepository.SaveChanges())
                {
                    return Ok($"Order with id {id} deleted");
                }
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest();
        }

        [HttpGet]
        [Admin]
        public async Task<ActionResult<Order[]>> Get([FromQuery] PagedParameters orderParameters)
        {
            try
            {
                var orders = await _orderRepository.PagedList(orderParameters);

                if (orders is null) return NoContent();

                return orders.ToArray();
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> Post()
        {
            try
            {
                int? cartId = AuthenticatedUserData.GetCartId(HttpContext);

                if (cartId is null) return BadRequest("Could not find cart for user");

                var productsCarts = await _cartProductRepository.GetListByProperty(p => p.CartId == cartId);

                var productIds = productsCarts.Select(cp => cp.ProductId).ToList();

                var products = new List<Product>();

                foreach (var id in productIds)
                {
                    var product = await _productRepository.Get(id);

                    products.Add(product);
                }

                var orderPrice = products.Select(p => p.Price).Sum();

                if (products.Count is 0) return BadRequest("Order cant be empty");

                if (orderPrice < 0) return BadRequest("Order price can't be less than 0");

                var order = new Order
                {
                     Name = AuthenticatedUserData.GetUserName(HttpContext),
                     Adress = AuthenticatedUserData.GetUserAdress(HttpContext),
                     PhoneNumber = AuthenticatedUserData.GetUserPhone(HttpContext),
                     UserId = (int)AuthenticatedUserData.GetUserId(HttpContext),
                     ProductCount = products.Count,
                     Price = orderPrice

                };

                var result = await _orderRepository.Add(order);

                return result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
