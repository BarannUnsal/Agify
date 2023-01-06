using Agify.Domain.Entities;

namespace Agify.BL.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAsync(string[] name);
    }
}
