using System.Threading.Tasks;
using TechStore.Api.Data.Enteties;
using TechStore.Api.Data.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace TechStore.Api.Authentication
{
    public class Validator
    {
        private readonly IRepository<User> _userRepository;

        public Validator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckUsernameExistance(string username)
        {
            var user = await _userRepository.GetByProperty(u => u.Username == username);

            if (user is not null) return true;

            return false;
        }

        public bool HashedPasswordMatch(string hash ,string password)
        {
            var isValidPassword = BC.Verify(password, hash);

            if (isValidPassword) return true;

            return false;
        }
    }
}