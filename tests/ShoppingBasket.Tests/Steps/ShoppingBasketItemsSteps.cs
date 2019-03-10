using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ShoppingBasket.Services;
using System.Linq;
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

        [Given(@"the following offer vouchers:")]
        public async Task GivenTheFollowingOfferVouchers(Table table)
        {
            var offerVouchers = table.CreateSet<OfferVoucher>();

            foreach (var offerVoucher in offerVouchers)
            {
                await PostJson("/api/items/offervoucher", offerVoucher);
            }
        }


        [When(@"(.*) are applied")]
        public async Task WhenAreApplied(string ids)
        {
            var basket = GetBasketId();
            foreach (var id in ids.Split(","))
            {
                var response = await PostJson($"/api/basket/{basket}/{id.Trim()}");
            }
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

        [Then(@"the total should be (.*)")]
        public async Task ThenTheTotalShouldBe(decimal total)
        {
            var basket = GetBasketId();
            var response = await ApiClient.GetAsync($"/api/basket/{basket}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var shoppingBasketResult = JsonConvert.DeserializeObject<ShoppingBasketResult>(result);
            shoppingBasketResult.Total.Should().Be(total);
            _scenarioContext.Set(shoppingBasketResult, "BasketResult");
        }

        [Then(@"the basket should have message '(.*)'")]
        public void ThenTheBasketShouldHaveMessage(string message)
        {
            var shoppingBasketResult = _scenarioContext.Get<ShoppingBasketResult>("BasketResult");
            var messages = shoppingBasketResult.ItemResults.Select(x => x.Message);
            message.Should().Contain(message);
        }
    }
}
