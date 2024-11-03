
using System.Text;
using WEB_253504_Kolesnikov.UI.Services.Authentication;

namespace WEB_253504_Kolesnikov.UI.Services.ApiFileService
{
    public class ApiFileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenAccessor _tokenAccessor;

        public ApiFileService(IHttpClientFactory httpClientFactory, ITokenAccessor tokenAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiClient");
            _tokenAccessor = tokenAccessor;
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Files/");
            urlString.Append($"{fileName}");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(urlString.ToString())
            };

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            //request object
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress!.AbsoluteUri}Files/")
            };
            //random name for file and extension
            //var extension = Path.GetExtension(formFile.FileName);
            //var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
            //create content that contains stream of downloaded file
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(formFile.OpenReadStream());
            content.Add(streamContent, "file", formFile.FileName);
            //set up content at request
            request.Content = content;

            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

            //send request to API
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) 
            {
                return await response.Content.ReadAsStringAsync();
            }
            return String.Empty;
        }
    }
}
