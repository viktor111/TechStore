using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Models.Models;
using BC = BCrypt.Net.BCrypt;

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Cart> _cartRepository;

        public UsersController
            (
                IRepository<User> userRepository,
                IMapper mapper,
                ILogger<UsersController> logger,
                IWebHostEnvironment webHostEnvironment,
                IRepository<Cart> cartRepository
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _cartRepository = cartRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> Get(int id, bool includeCart = false)
        {
            try
            {
                _logger.LogInformation("Getting User...");

                var data = await _userRepository.Get(id, includeCart);

                var result = _mapper.Map<UserModel>(data);

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
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            try
            {
                _logger.LogInformation("Creating cart for user and adding to database...");

                var cartCreated = await _cartRepository.Add(new Cart());
                model.CartId = cartCreated.Id;

                var passwordHash = BC.HashPassword(model.Password);
                model.Password = passwordHash;

                var modelToUser = _mapper.Map<User>(model);

                _logger.LogInformation("Adding user to database...");

                var user = await _userRepository.Add(modelToUser);

                return _mapper.Map<UserModel>(modelToUser);

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
    }
}
