using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using TechStore.Api.Helpers;
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
        private readonly Validator _validator;

        public UsersController
            (
                IRepository<User> userRepository,
                IMapper mapper,
                ILogger<UsersController> logger,
                IWebHostEnvironment webHostEnvironment,
                IRepository<Cart> cartRepository,
                Validator validator
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _cartRepository = cartRepository;
            _validator = validator;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        [Admin]
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
        public async Task<ActionResult<UserModel>> Post(UserModel model, string admin = null)
        {
            try
            {
                _logger.LogInformation("Creating cart for user and adding to database...");

                var cartCreated = await _cartRepository.Add(new Cart());
                model.CartId = cartCreated.Id;

                var passwordHash = BC.HashPassword(model.Password);
                model.Password = passwordHash;

                if (admin == Constants.Secret) model.IsAdmin = true;

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
         
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<UserModel>> Login(string username, string password, int daysUntilTokenExpires = 1)
        {
            try
            {
                _logger.LogInformation("a");
                var usernameValid = await _validator.CheckUsernameExistance(username);

                if (usernameValid is false) return BadRequest("Username does not exist");

                var user = await _userRepository.GetByProperty(user => user.Username == username);

                var passwordHash = user.Password;

                var passwordValid = _validator.HashedPasswordMatch(passwordHash, password);

                if (passwordValid is false) return BadRequest("Invalid password");

                var userClaimsGenerator = new AdminClaims();

                var userClaims = userClaimsGenerator.GenerateClaims(user);

                var token = Authenticator.GenerateToken(userClaims, daysUntilTokenExpires);

                return Ok(token);
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
