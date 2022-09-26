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

        public async Task<Response<string>> PostPutAsync(string urlAndPath, StringContent requestData, bool isPut = false)
        {
            try
            {
                HttpResponseMessage rawData;
                if (!isPut)
                {
                    rawData = await _httpClient.PostAsync(urlAndPath, requestData);
                }
                else 
                {
                    rawData = await _httpClient.PutAsync(urlAndPath, requestData);
                }

                if (rawData.IsSuccessStatusCode)
                {
                    return Response<string>.Success(rawData.Content.ToString(), 200);
                }
                return Response<string>.Fail("Data not found!", 404);
            }
            catch (Exception ex)
            {
                return Response<string>.Fail(ex.Message, 500);
            }
        }
    }
}
