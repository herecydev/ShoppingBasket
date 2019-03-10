// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ShoppingBasket.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ShoppingBasketItemsFeature : Xunit.IClassFixture<ShoppingBasketItemsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ShoppingBasketItems.feature"
#line hidden
        
        public ShoppingBasketItemsFeature(ShoppingBasketItemsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ShoppingBasketItems", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Value",
                        "Description",
                        "ProductType",
                        "IsDiscountable"});
            table1.AddRow(new string[] {
                        "1",
                        "10.50",
                        "Cheap Hat",
                        "Headwear",
                        "true"});
            table1.AddRow(new string[] {
                        "2",
                        "54.65",
                        "Awesome Jumper",
                        "Jumper",
                        "true"});
            table1.AddRow(new string[] {
                        "3",
                        "25.00",
                        "Thermal Hat",
                        "Headwear",
                        "true"});
            table1.AddRow(new string[] {
                        "4",
                        "26.00",
                        "Woven Jumper",
                        "Jumper",
                        "true"});
            table1.AddRow(new string[] {
                        "5",
                        "3.50",
                        "Head Light",
                        "Headgear",
                        "true"});
            table1.AddRow(new string[] {
                        "6",
                        "30.00",
                        "Gift Voucher",
                        "GiftVoucher",
                        "false"});
#line 4
 testRunner.Given("the following shopping basket items:", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Value"});
            table2.AddRow(new string[] {
                        "000-001",
                        "5.00"});
#line 12
 testRunner.And("the following gift vouchers:", ((string)(null)), table2, "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Value",
                        "Threshold",
                        "ProductType"});
            table3.AddRow(new string[] {
                        "000-002",
                        "5.00",
                        "50",
                        "Headgear"});
            table3.AddRow(new string[] {
                        "000-003",
                        "5.00",
                        "5",
                        "Headgear"});
            table3.AddRow(new string[] {
                        "000-004",
                        "5.00",
                        "50",
                        ""});
#line 15
 testRunner.And("the following offer vouchers:", ((string)(null)), table3, "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.TheoryAttribute(DisplayName="Shopping Basket gift voucher")]
        [Xunit.TraitAttribute("FeatureTitle", "ShoppingBasketItems")]
        [Xunit.TraitAttribute("Description", "Shopping Basket gift voucher")]
        [Xunit.InlineDataAttribute("1, 2", "000-001", "60.15", new string[0])]
        [Xunit.InlineDataAttribute("3, 4, 5", "000-002", "51.00", new string[0])]
        [Xunit.InlineDataAttribute("3, 4", "000-001, 000-004", "41.00", new string[0])]
        public virtual void ShoppingBasketGiftVoucher(string productitems, string vouchers, string total, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Shopping Basket gift voucher", null, exampleTags);
#line 21
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 22
 testRunner.Given("a shopping basket is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
 testRunner.And(string.Format("{0} have been added to the basket", productitems), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.When(string.Format("{0} are applied", vouchers), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then(string.Format("the total should be {0}", total), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute(DisplayName="Shopping Basket gift voucher rejections")]
        [Xunit.TraitAttribute("FeatureTitle", "ShoppingBasketItems")]
        [Xunit.TraitAttribute("Description", "Shopping Basket gift voucher rejections")]
        [Xunit.InlineDataAttribute("3, 4", "000-002", "There are no products in your basket applicable to voucher 000-002.", "51.00", new string[0])]
        [Xunit.InlineDataAttribute("3, 4", "000-002, 000-003", "Another offer voucher has already been applied.", "51.00", new string[0])]
        [Xunit.InlineDataAttribute("3, 6", "000-004", "You have not reached the spend threshold for voucher 000-004. Spend another £25.0" +
            "1 to receive £5.00 discount from your basket total.", "55.00", new string[0])]
        public virtual void ShoppingBasketGiftVoucherRejections(string productitems, string vouchers, string message, string total, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Shopping Basket gift voucher rejections", null, exampleTags);
#line 33
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 34
 testRunner.Given("a shopping basket is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
 testRunner.And(string.Format("{0} have been added to the basket", productitems), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.When(string.Format("{0} are applied", vouchers), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 37
 testRunner.Then(string.Format("the total should be {0}", total), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 38
 testRunner.And(string.Format("the basket should have message \'{0}\'", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ShoppingBasketItemsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ShoppingBasketItemsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
