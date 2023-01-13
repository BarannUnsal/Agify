using Agify.Domain.Entities;

namespace Agify.DAL.Abstract
{
    public interface IUserRepository
    {
        Task<object> GetAsync(string name);
        Task<User[]> GetArrayAsync(string[] names);
        Task<bool> AddUserAsync(User user);
        Task<bool> AddUserRangeAsync(User[] users);
    }
}
