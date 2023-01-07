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

        public Task<User[]> GetArrayAsync(string[] names)
        {
           return _userRepository.GetArrayAsync(names);           
        }

        public Task<User> GetAsync(string name)
        {
            return _userRepository.GetAsync(name);
        }
    }
}
