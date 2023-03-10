using Agify.DAL.Abstract;
using Agify.DAL.Contexts;
using Agify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net.Http.Headers;
using System.Text;

namespace Agify.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AgifyDbContext _context;
        private readonly IDistributedCache _cache;

        public UserRepository(AgifyDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        static string url = "https://api.agify.io?";


        public async Task<object> GetAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "'name' parameter cannot be null");
            }
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var cacheKey = $"user:{name.ToLower()}";
                    var cachedValue = await _cache.GetAsync(cacheKey);
                    User user;
                    if (cachedValue != null)
                    {
                        var cachedUser = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(cachedValue));
                        user = new User
                        {
                            Age = cachedUser.Age,
                            Count = cachedUser.Count
                        };
                        var newUser = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(cachedValue));
                        await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newUser)));
                        int newAge = int.Parse(user.Age);
                        int newCount = int.Parse(user.Count);
                        return new AgeAndCount { Age = newAge.ToString(), Count = newCount.ToString() };
                    }
                    else
                    {
                        using (var response = await httpClient.GetAsync($"{url}name={name.ToLower()}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            user = JsonConvert.DeserializeObject<User>(apiResponse);
                            var isDb = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
                            if (isDb == null)
                                await AddUserAsync(user);
                            else
                            {
                                user.Age += isDb.Age;
                                user.Count += isDb.Count;
                                _context.Users.Update(isDb);
                                await _context.SaveChangesAsync();
                            }
                            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(apiResponse));

                            return user;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User[]> GetArrayAsync(string[] names)
        {
            if (names == null)
            {
                throw new ArgumentNullException("'names' parameter cannot be null or empty");
            }
            try
            {
                var results = new User[names.Length];
                var mark = "";
                foreach (var name in names)
                {
                    mark += $"name[]={System.Net.WebUtility.UrlEncode(name.ToLower())}&";
                }

                using (var httpClient = new HttpClient())
                {

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var requestUrl = $"{url}{mark}";
                    var cacheKey = $"users:{requestUrl}";
                    var cachedValue = await _cache.GetAsync(cacheKey);

                    User[] users;
                    if (cachedValue != null)
                    {
                        var cachedString = Encoding.UTF8.GetString(cachedValue);
                        var _users = JsonConvert.DeserializeObject<User[]>(cachedString);
                        for (int i = 0; i < _users.Length; i++)
                        {
                            var cachedUser = JsonConvert.DeserializeObject<User[]>(cachedString);
                            int newAge = int.Parse(_users[i].Age);
                            int newCount = int.Parse(_users[i].Count);
                            newAge += int.Parse(cachedUser[i].Age);
                            newCount += int.Parse(cachedUser[i].Count);
                            _users[i].Age = newAge.ToString();
                            _users[i].Count = newCount.ToString();
                        }
                        await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_users)));
                        return _users;
                    }
                    else
                    {
                        using (var response = await httpClient.GetAsync(requestUrl))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var _users = JsonConvert.DeserializeObject<User[]>(apiResponse);
                            foreach (var user in _users)
                            {
                                var dbUsers = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
                                if (dbUsers == null)
                                    await _context.Users.AddAsync(user);
                                else
                                {
                                    user.Age += dbUsers.Age;
                                    user.Count += dbUsers.Count;
                                    _context.Users.Update(dbUsers);
                                }
                            }
                            await _context.SaveChangesAsync();
                            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(apiResponse));
                            return _users;
                        }
                    }

                }
            }
            catch (JsonSerializationException)
            {
                return new User[0];
            }
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUserRangeAsync(User[] users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
