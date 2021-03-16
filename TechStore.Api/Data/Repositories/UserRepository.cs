using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TechStore.Api.Data.Enteties;
using TechStore.Models.Models;
using BC = BCrypt.Net.BCrypt;

namespace TechStore.Api.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly IMapper _mapper;


        public UserRepository
            (
                TechStoreDbContext _dbContext,
                IMapper mapper
            )
            : base(_dbContext)
        {
            _mapper = mapper;
        }

        public async override Task<User> Get(int id, bool include)
        {
            if (include)
            {
                var reslutWithInclude = await _dbContext.Users
                    .Where(u => u.Id == id)
                    .Include(x => x.Cart)
                    .FirstOrDefaultAsync();

                return reslutWithInclude;
            }
            var result = await _dbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async override Task<AuthenticateResponse> Authenticate(User model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user is null) return null;

            bool passwordVeryfied = BC.Verify(model.Password, user.Password);

            if (passwordVeryfied)
            {
                var token = GenerateJwtToken(user);

                return new AuthenticateResponse(_mapper.Map<UserModel>(model), token);
            }

            return null;
        }
        
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("acdc11434jjkk34311acdasdwjkdnovjfnbcacacasdadc11434jjkk314311acdc");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
