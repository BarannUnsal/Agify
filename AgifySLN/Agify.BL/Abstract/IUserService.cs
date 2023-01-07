using Agify.Domain.Entities;

namespace Agify.BL.Abstract
{
    public interface IUserService
    {
        Task<User> GetAsync(string name);
        Task<User[]> GetArrayAsync(string[] names);
    }
}
