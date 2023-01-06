using Agify.DAL.Abstract;
using Agify.DAL.Contexts;
using Agify.Domain.Entities;
using Newtonsoft.Json;

namespace Agify.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AgifyDbContext _context;

        public UserRepository(AgifyDbContext context)
        {
            _context = context;
        }

        private string key = "b349603a39mshb3066b2704534e8p11bbb6jsn7003bdfaaaf5";

        public async Task<IEnumerable<User>?> Get(string[] name)
        {
            string.Join(" ", name);
            string lowerName = "";
            for (int i = 0; i < name.Count(); i++)
            {
                lowerName = name[i].ToLower();
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.agify.io/?name[]={lowerName}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<List<User>>(apiResponse).ToList();
                    return user;
                }
            }
        }
    }
}
