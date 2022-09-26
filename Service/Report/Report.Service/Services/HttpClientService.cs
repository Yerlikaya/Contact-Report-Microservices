using Shared.Dtos;
using System.Text.Json;

namespace Report.Service.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Response<string>> GetAsync(string urlAndPath)
        {
            try
            {
                var rawData = await _httpClient.GetStringAsync(urlAndPath);
                if (string.IsNullOrEmpty(rawData))
                {
                    return Response<string>.Fail("Data not found!", 404);
                }
                return Response<string>.Success(rawData, 200);
            }
            catch (Exception ex)
            {
                return Response<string>.Fail(ex.Message, 500);
            }


        }
    }
}
