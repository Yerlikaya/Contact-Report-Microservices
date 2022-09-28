using Microsoft.VisualStudio.TestTools.UnitTesting;
using Report.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Report.Service.Services.Tests
{
    [TestClass()]
    public class HttpClientServiceTests
    {
        private readonly HttpClientService _httpClientService;

        public HttpClientServiceTests()
        {
            MyHttpClientFactory _httpClientFactory = new MyHttpClientFactory();
            _httpClientService = new HttpClientService(_httpClientFactory);
        }

        [TestMethod()]
        public void GetAsyncTest()
        {
            var response = _httpClientService.GetAsync("https://httpbin.org/anything").Result;
            Assert.IsTrue(response.IsSuccessful);
        }

        [TestMethod()]
        public void PostPutAsyncTest()
        {
            int[] testRequest = { 1, 2, 3, 4 };
            var jsonReportDto = JsonSerializer.Serialize(testRequest);
            StringContent stringContent = new StringContent(jsonReportDto, Encoding.UTF8, "application/json");
            var response = _httpClientService.PostPutAsync("https://httpbin.org/anything", stringContent).Result;
            Assert.IsTrue(response.IsSuccessful);
        }
    }

    public class MyHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}