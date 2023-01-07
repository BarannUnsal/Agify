using Agify.DAL.Abstract;
using Agify.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Agify.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        static string url = "https://api.agify.io?";
        public async Task<User> GetAsync(string name)
        {
            try
            {
                if (name != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        using (var response = await httpClient.GetAsync($"{url}name={name.ToLower()}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            User user = JsonConvert.DeserializeObject<User>(apiResponse);
                            return user;
                        }
                    }
                }
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<User[]> GetArrayAsync(string[] names)
        {
            try
            {
                if (names == null)
                {
                    throw new ArgumentException("The 'names' parameter cannot be null or empty", nameof(names));
                }

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
                    using (var response = await httpClient.GetAsync(requestUrl))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException("Failed to retrieve data from Agify.io");
                        }

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var users = JsonConvert.DeserializeObject<User[]>(apiResponse);
                        return users;
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                _logger.LogError(ex, "ERROR!");
                return new User[0];
            }
        }
    }
}
