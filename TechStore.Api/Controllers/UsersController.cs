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
    public class UsersController : ControllerBase
    {
        private ILogger _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UsersController
            (
                IRepository<User> userRepository,
                IMapper mapper,
                ILogger<UsersController> logger
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> Get(bool includeCart = false)
        {
            try
            {
                _logger.LogInformation("Getting User...");

                var data = await _userRepository.Get(1, true);

                var result = _mapper.Map<UserModel>(data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
