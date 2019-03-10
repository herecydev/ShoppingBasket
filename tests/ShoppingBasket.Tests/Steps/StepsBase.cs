using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ShoppingBasket.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace ShoppingBasket.Tests.Steps
{
    public abstract class StepsBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly WebApplicationFactory<Startup> _factory;
        protected HttpClient ApiClient { get; }

        public StepsBase(ScenarioContext scenarioContext, WebApplicationFactory<Startup> factory)
        {
            _scenarioContext = scenarioContext;
            _factory = factory;
            ApiClient = _factory.CreateClient();
        }

        public HttpResponseMessage GetResponse()
        {
            if (!_scenarioContext.TryGetValue("Response", out HttpResponseMessage value))
                throw new KeyNotFoundException("Response could not be found");

            return value;
        }

        public async Task<HttpResponseMessage> PostJson(string path, object content)
        {
            var result = await ApiClient.PostAsync(path, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
            return result;
        }

        public async Task<HttpResponseMessage> PostJson(string path)
        {
            var result = await ApiClient.PostAsync(path, null);
            result.EnsureSuccessStatusCode();
            return result;
        }

        public string GetBasketId()
        {
            if (!_scenarioContext.TryGetValue("BasketId", out string value))
                throw new KeyNotFoundException("BasketId could not be found, was the \"creating a shopping basket\" run?");

            return value;
        }

        public async Task<ShoppingBasketResult> GetItemResult()
        {
            if (!_scenarioContext.TryGetValue("Response", out HttpResponseMessage value))
                throw new KeyNotFoundException("Response could not be found, was anything added?");

            value.EnsureSuccessStatusCode();
            var result = await value.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ShoppingBasketResult>(result);
        }
    }
}
