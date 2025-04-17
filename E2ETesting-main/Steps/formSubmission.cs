using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

namespace E2ETesting.Steps
{
    [Binding, Scope(Tag = "formSubmission")]  // This ensures the steps are scoped to the 'formSubmission' scenario
    public class FormSubmission
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [BeforeScenario]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 500 });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [AfterScenario]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Given(@"I am on the form page")]
        public async Task GivenIAmOnTheFormPage()
        {
            await _page.GotoAsync("http://localhost:3000");
        }

        [When(@"I enter ""(.*)"" as my Email")]
        public async Task WhenIEnterMyEmail(string email)
        {
            await _page.FillAsync("input[placeholder='Email']", email);
        }

        [When(@"I enter ""(.*)"" as my Product")]
        public async Task WhenIEnterAsMyProduct(string product)
        {
            await _page.FillAsync("input[placeholder='Product']", product);
        }

        [When(@"I enter ""(.*)"" as my Message")]
        public async Task WhenIEnterAsMyMessage(string message)
        {
            await _page.FillAsync("textarea[placeholder='Message']", message);
        }

        [When(@"I click on the Send button")]
        public async Task WhenIClickOnTheSendButton()
        {
            await _page.ClickAsync("button.sendBtn");
        }

        [Then(@"I should be redirected to the confirmation page")]
        public async Task ThenIShouldBeRedirectedToTheConfirmationPage()
        {
            await _page.WaitForURLAsync("**/confirmation");
            var title = await _page.InnerTextAsync("h2");
            Assert.Contains("Thank you for reaching out!", title);
        }
    }
}
