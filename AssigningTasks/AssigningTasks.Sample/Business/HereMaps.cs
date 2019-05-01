using AssigningTasks.Sample.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssigningTasks.Sample.Business
{
    public class HereMaps : IHereMaps
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl, _appId, _appCode;

        public HereMaps()
        {
            _httpClient = _httpClient ?? new HttpClient();
            _baseUrl = "https://places.api.here.com/places/v1/";
            _appId = "3fL9rToqJakN7dnNRlAj";
            _appCode = "sH6i9IuHRi5kJdPghNFMHg";
        }

        public async Task<DiscoverSearchModel> DiscoverSearch(string query, double lat, double lng)
        {
            string url = $"{_baseUrl}discover/search?at={lat}%2C{lng}&q={query}&app_id={_appId}&app_code={_appCode}";
            return await DiscoverSearch(url);
        }

        public async Task<DiscoverSearchModel> DiscoverSearch(string url)
        {
            Uri uri = new Uri(url, UriKind.Absolute);
            System.Diagnostics.Debug.WriteLine($"{nameof(DiscoverSearch)}: {uri}");

            var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<DiscoverSearchModel>(result);
            }

            return null;
        }
    }
}