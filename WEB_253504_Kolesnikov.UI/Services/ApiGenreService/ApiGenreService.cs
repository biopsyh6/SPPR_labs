using System.Text;
using System.Text.Json;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;

namespace WEB_253504_Kolesnikov.UI.Services.ApiGenreService
{
    public class ApiGenreService : IGenreService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiGenreService> _logger;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiGenreService(IConfiguration configuration, ILogger<ApiGenreService> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _pageSize = _configuration.GetSection("ItemsPerPage").Value!;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<ResponseData<List<Genre>>> GetGenreListAsync()
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Genres/");
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var data = await response.Content.ReadFromJsonAsync<List<Genre>>(_serializerOptions);
                    return ResponseData<List<Genre>>.Success(data!);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<List<Genre>>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
            return ResponseData<List<Genre>>
                .Error($"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }
    }
}
