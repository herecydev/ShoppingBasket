using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ShoppingBasket.Tests.Steps
{
    [Binding]
    public class ShoppingBasketCreationSteps : StepsBase
    {
        private readonly ScenarioContext _scenarioContext;

        public ShoppingBasketCreationSteps(ScenarioContext scenarioContext, WebApplicationFactory<Startup> factory) : base(scenarioContext, factory)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a shopping basket is created")]
        [When(@"creating a shopping basket")]
        public async Task WhenCreatingAShoppingBasket()
        {
            var response = await ApiClient.PostAsync("api/basket", null);
            _scenarioContext.Set(response, "Response");
            _scenarioContext.Set(await response.Content.ReadAsStringAsync(), "BasketId");
        }

        [When(@"deleting the shopping basket")]
        public async Task WhenDeletingTheShoppingBasket()
        {
            var id = GetBasketId();
            var response = await ApiClient.DeleteAsync($"api/basket/{id}");
            _scenarioContext.Set(response, "Response");
        }

        [When(@"deleting a shopping basket with id ""(.*)""")]
        public async Task WhenDeletingTheShoppingBasketWithId(string id)
        {
            var response = await ApiClient.DeleteAsync($"api/basket/{id}");
            _scenarioContext.Set(response, "Response");
        }

        [Then(@"an id should be returned")]
        public void ThenAnIdShouldBeReturned()
        {
            GetBasketId().Should().NotBeNull();
        }

        [Then(@"should return successfully")]
        public void ThenShouldReturnSuccessfully()
        {
            var response = GetResponse();
            response.EnsureSuccessStatusCode();
        }
    }
}
