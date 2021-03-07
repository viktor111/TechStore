using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
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


        public ProductsController
            (
                IRepository<Product> productRepository,
                IMapper mapper,
                ILogger<ProductsController> logger
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ProductModel[]>> Get()
        {
            try
            {                
                _logger.LogInformation($"Getting Products...");

                var data = await _productRepository.All();

                var result = _mapper.Map<ProductModel[]>(data);

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public ActionResult<ProductModel> Post(ProductModel model)
        {
             throw new NotImplementedException();
        }
    }
}
