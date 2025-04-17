using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

namespace E2ETests
{
    [Binding, Scope(Tag = "formValidation")]  // This scopes the steps to the 'formValidation' feature
    public class FormValidationSteps
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        public FormValidationSteps()
        {
            _playwright = Playwright.CreateAsync().Result;
            _browser = _playwright.Chromium.LaunchAsync(new() { Headless = false }).Result;
            _context = _browser.NewContextAsync().Result;
            _page = _context.NewPageAsync().Result;
        }

        [Given(@"I am on the form page")]
        public async Task GivenIAmOnTheFormPage()
        {
            await _page.GotoAsync("http://localhost:3000");
        }

        [When(@"I enter ""(.*)"" as my Email")]
        public async Task WhenIEnterEmailAsMyEmail(string email)
        {
            await _page.FillAsync("#email", email);
        }

        [When(@"I enter ""(.*)"" as my Product")]
        public async Task WhenIEnterProductAsMyProduct(string product)
        {
            await _page.FillAsync("#product", product);
        }

        [When(@"I enter ""(.*)"" as my Message")]
        public async Task WhenIEnterMessageAsMyMessage(string message)
        {
            await _page.FillAsync("#message", message);
        }

        [When(@"I click on the Send button")]
        public async Task WhenIClickOnTheSendButton()
        {
            await _page.ClickAsync("#sendButton");
        }

        [Then(@"I should see a validation message indicating that all fields are required")]
        public async Task ThenIShouldSeeAValidationMessage()
        {
            var errorMessage = await _page.InnerTextAsync("#errorMessage");
            Assert.Equal("All fields are required", errorMessage);
        }

        // Clean up after each scenario
        [AfterScenario]
        public async Task CleanUp()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
