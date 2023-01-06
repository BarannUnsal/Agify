using Agify.Domain.Entities;

namespace Agify.DAL.Abstract
{
    public interface IUserRepository
    {
        Task<IEnumerable<User?>> Get(string[] name);
    }
}
