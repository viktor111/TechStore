using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Api.Helpers;
using TechStore.Models.Models;

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Cart> _cartRepository;

        public ProductsController
            (
                IRepository<Product> productRepository,
                IMapper mapper,
                ILogger<ProductsController> logger,
                IWebHostEnvironment webHostEnvironment,
                IRepository<Cart> cartRepository
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ProductModel[]>> Get([FromQuery] PagedParameters productParameters)
        {
            try
            {                
                _logger.LogInformation($"Getting Products...");

                var data = await _productRepository.PagedList(productParameters);

                var result = _mapper.Map<ProductModel[]>(data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            try
            {
                _logger.LogInformation("Get single product...");

                var data = await _productRepository.Get(id);

                var result = _mapper.Map<ProductModel>(data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        [Admin]
        public async Task<ActionResult<ProductModel>> Post(ProductModel model)
        {
            try
            {
                _logger.LogInformation("Create new product...");

                var existing = await _productRepository.GetByProperty(p => p.Name == model.Name);
                if(existing is not null)
                {
                    return BadRequest("Product already exists");
                }

                var productMapped = _mapper.Map<Product>(model);

                var result = _productRepository.Add(productMapped);

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut("{id:int}")]
        [Admin]
        public async Task<ActionResult<ProductModel>> Put(int id, ProductModel model)
        {
            try
            {
                var oldProduct = await _productRepository.Get(id);

                if (oldProduct is null)
                {
                    return NotFound($"Could not find product with id {id}");
                }

                _mapper.Map(model, oldProduct);

                if (await _productRepository.SaveChanges())
                {
                    return _mapper.Map<ProductModel>(oldProduct);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        [Admin]
        public async Task<ActionResult<ProductModel>> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting product with id {id}");

                var oldProduct = await _productRepository.Get(id);

                if(oldProduct is null)
                {
                    return NotFound($"Could not find product with id {id}");
                }

                await _productRepository.Delete(oldProduct);

                if (await _productRepository.SaveChanges())
                {
                    return Ok($"Product with id {id} deleted");
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

        [HttpPost("{productId:int}")]
        [Authorize]
        public async Task<ActionResult<ProductModel>> AddToCart(int productId, bool includeCart = false)
        {
            try
            {
                int? cartIdNullable = AuthenticatedUserData.GetCartId(HttpContext);

                if (cartIdNullable is null) return BadRequest("Cart id error");

                int cartId = (int)cartIdNullable;

                _logger.LogInformation($"Adding product with id {productId} to cart with {cartId}");

                var product = await _productRepository.Get(productId, includeCart);

                var cart = await _cartRepository.Get(cartId);

                if(product is null)
                {
                    return NotFound($"Could not find product with id {productId}");
                }
                if(cart is null)
                {
                    return NotFound($"Could not find cart with id {cartId}");
                }

                var result = await _productRepository.AddToCart(product, cart);

                var changesToDatabasePersit = await _productRepository.SaveChanges();

                if (changesToDatabasePersit)
                {
                    return _mapper.Map<ProductModel>(result);
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
    }
}
