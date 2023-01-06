using Agify.DAL.Abstract;
using Agify.DAL.Contexts;
using Agify.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Agify.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AgifyDbContext _context;

        public UserRepository(AgifyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>?> GetAsync(string[] name)
        {
            try
            {
                string.Join(" ", name);
                string lowerName = "";
                string url = "https://api.agify.io/";
                string[] nameArr = new string[name.Length];
                for (int i = 0; i < name.Count(); i++)
                {
                    lowerName = name[i].ToLower();
                }
                if (name.Count() == 1)
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        using (var response = await httpClient.GetAsync($"{url}?name[]={lowerName}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            List<User> user = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                            return user;
                        }
                    }
                }
                else if (name.Count() > 1)
                {
                    ICollection<User> users = null;
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    for (int i = 0; i < name.Count(); i++)
                    {
                        foreach (var item in name)
                        {
                            var response = await httpClient.GetAsync($"{url}?name[[]]={item.ToLower()}");
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                            return users;
                        }
                    }
                }
                return null;

            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
