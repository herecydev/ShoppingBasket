using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ShoppingBasket.Services;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ShoppingBasket.Tests.Steps
{
    [Binding]
    public class ShoppingBasketItemsSteps : StepsBase
    {
        private readonly ScenarioContext _scenarioContext;

        public ShoppingBasketItemsSteps(ScenarioContext scenarioContext, WebApplicationFactory<Startup> factory) : base(scenarioContext, factory)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the following shopping basket items:")]
        public async Task GivenTheFollowingShoppingBasketItems(Table table)
        {
            var productItems = table.CreateSet<ProductItem>();

            foreach (var productItem in productItems)
            {
                await PostJson("/api/items/productitem", productItem);
            }
        }

        [Given(@"the following gift vouchers:")]
        public async Task GivenTheFollowingGiftVouchers(Table table)
        {
            var giftVouchers = table.CreateSet<GiftVoucher>();

            foreach (var giftVoucher in giftVouchers)
            {
                await PostJson("/api/items/giftvoucher", giftVoucher);
            }
        }

        [When(@"(.*) is applied")]
        public async Task WhenIsApplied(string id)
        {
            var basket = GetBasketId();
            var response = await PostJson($"/api/basket/{basket}/{id}");
            _scenarioContext.Set(response, "Response");
        }

        [Given(@"(.*) have been added to the basket")]
        public async Task GivenHaveBeenAddedToTheBasket(string items)
        {
            var basket = GetBasketId();
            foreach (var item in items.Split(","))
            {
                await PostJson($"/api/basket/{basket}/{item.Trim()}");
            }
        }

        [Then(@"it should return successfully")]
        public async Task ThenItShouldReturnSuccessfully()
        {
            var itemResult = await GetItemResult();
            itemResult.ItemResultAction.Should().Be(ItemResultAction.Success);
        }

        [Then(@"the total should be (.*)")]
        public async Task ThenTheTotalShouldBe(decimal total)
        {
            var basket = GetBasketId();
            var response = await ApiClient.GetAsync($"/api/basket/{basket}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var actualTotal = JsonConvert.DeserializeObject<decimal>(result);
            actualTotal.Should().Be(total);
        }
    }
}
