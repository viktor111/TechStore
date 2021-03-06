using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Models.Models;

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;


        public ProductController(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ProductModel[]> Get()
        {
            try
            {
                var data = _productRepository.All();

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
