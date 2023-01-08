using Agify.DAL.Abstract;
using Agify.Domain.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net.Http.Headers;

namespace Agify.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {

        static string url = "https://api.agify.io?";
        public async Task<User> GetAsync(string name)
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

                    var redis = ConnectionMultiplexer.Connect("redis");
                    var db = redis.GetDatabase();
                    var cacheKey = $"user:{name.ToLower()}";
                    var cachedValue = db.StringGet(cacheKey);


                    User user;
                    if (cachedValue.HasValue)
                    {
                        user = JsonConvert.DeserializeObject<User>(cachedValue);
                    }
                    else
                    {
                        using (var response = await httpClient.GetAsync($"{url}name={name.ToLower()}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            user = JsonConvert.DeserializeObject<User>(apiResponse);
                            db.StringSet(cacheKey, apiResponse);
                            return user;
                        }
                    }
                    return user;
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
                    var redis = ConnectionMultiplexer.Connect("redis");
                    var db = redis.GetDatabase();
                    var cachedValue = db.StringGet(cacheKey);

                    User[] users;
                    if (cachedValue.HasValue)
                    {
                        // If the data is cached, deserialize it and return it
                        users = JsonConvert.DeserializeObject<User[]>(cachedValue);
                    }
                    else
                    {
                        using (var response = await httpClient.GetAsync(requestUrl))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            users = JsonConvert.DeserializeObject<User[]>(apiResponse);
                            db.StringSet(cacheKey, apiResponse);
                        }
                    }
                    return users;
                }
            }
            catch (JsonSerializationException)
            {
                return new User[0];
            }
        }
    }
}
