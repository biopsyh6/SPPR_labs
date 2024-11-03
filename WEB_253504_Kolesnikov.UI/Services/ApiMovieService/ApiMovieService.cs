using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;
using WEB_253504_Kolesnikov.UI.Services.ApiFileService;
using WEB_253504_Kolesnikov.UI.Services.Authentication;

namespace WEB_253504_Kolesnikov.UI.Services.ApiMovieService
{
    public class ApiMovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiMovieService> _logger;
        private readonly string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IFileService _fileService;
        private readonly ITokenAccessor _tokenAccessor;

        public ApiMovieService(IConfiguration configuration,
            ILogger<ApiMovieService> logger, IHttpClientFactory httpClientFactory, IFileService fileService, ITokenAccessor tokenAccessor) 
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
            _fileService = fileService;
            _tokenAccessor = tokenAccessor;
        }

        public async Task<ResponseData<int>> CreateMovieAsync(Movie product, IFormFile? formFile)
        {
            //default picture
            product.ImagePath = "Images/no-image.jpg";

            if(formFile != null)
            {
                var imageUrl = await _fileService.SaveFileAsync(formFile);
                if (!string.IsNullOrEmpty(imageUrl)) 
                {
                    product.ImagePath = imageUrl;
                }
            }
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Movies/");

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.PostAsJsonAsync(new Uri(urlString.ToString()), product, _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<int>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<int>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Объект не создан. Error: {response.StatusCode.ToString()}");
            return ResponseData<int>
                .Error($"Объект не добавлен. Error: {response.StatusCode.ToString()}");

        }

        public async Task<ResponseData<bool>> DeleteMovieAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Movies/");
            urlString.Append($"{id}");

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.DeleteAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    return ResponseData<bool>.Error($"Error: {ex.Message}");
                }
            }
            return ResponseData<bool>.Error($"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }

        public async Task<ResponseData<Movie>> GetMovieByIdAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Movies/");
            urlString.Append($"{id}");

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<Movie>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    return ResponseData<Movie>.Error($"Error: {ex.Message}");
                }
            }
            return ResponseData<Movie>.Error($"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }

        public async Task<ResponseData<ProductListModel<Movie>>> GetMovieListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            //preparing url request
            var urlString = new StringBuilder($"{_httpClient.BaseAddress}Movies");

            var queryParams = new Dictionary<string, string>();

            //add category in urlstring
            if (categoryNormalizedName != null)
            {
                queryParams.Add("category", categoryNormalizedName);
            }

            queryParams.Add("pageNo", pageNo.ToString());

            //add page size in urlstring
            if(!_pageSize.Equals("3"))
            {
                queryParams.Add("pageSize", _pageSize);
            }

            var fullUrl = QueryHelpers.AddQueryString(urlString.ToString(), queryParams);

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            //send request to api
            var response = await _httpClient.GetAsync(fullUrl);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Movie>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");

                    return ResponseData<ProductListModel<Movie>>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
            return ResponseData<ProductListModel<Movie>>
                .Error($"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }

        public async Task<ResponseData<bool>> UpdateMovieAsync(int id, Movie product, IFormFile? formFile)
        {
            //default picture
            product.ImagePath = "Images/no-image.jpg";

            if (formFile != null)
            {
                var imageUrl = await _fileService.SaveFileAsync(formFile);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    product.ImagePath = imageUrl;
                }
            }

            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Movies/");
            urlString.Append($"{id}");

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.PutAsJsonAsync(new Uri(urlString.ToString()), product, _serializerOptions);

            if (response.IsSuccessStatusCode) 
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                    return ResponseData<bool>.Error($"Error: {ex.Message}");
                }
            }
            _logger.LogError($"Object not updated. Error: {response.StatusCode.ToString()}");
            return ResponseData<bool>
                .Error($"Object not updated. Error: {response.StatusCode.ToString()}");
        }
    }
}
