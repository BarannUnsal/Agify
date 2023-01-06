using Agify.BL.Abstract;
using Agify.DAL.Abstract;
using Agify.Domain.Entities;

namespace Agify.BL.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<User>> Get(string[] name)
        {
            return _userRepository.Get(name);
        }
    }
}
