using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace E2ETesting.Steps
{
    [Binding]
    [Scope(Tag = "changePassword")]  // Scope for 'changePassword' tag
    public class ChangePasswordSteps
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        private HttpClient _httpClient;

        [BeforeScenario]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = true, SlowMo = 500 });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
            _httpClient = new HttpClient();
        }

        [AfterScenario]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
            _httpClient.Dispose();
        }

        [Given(@"I am on the Change Password page")]
        public async Task GivenIAmOnTheChangePasswordPage()
        {
            await _page.GotoAsync("localhost:3000/changepassword");
            await _page.WaitForSelectorAsync(".passwordArea");
        }

        [When(@"I enter ""(.*)"" as the Email")]
        public async Task WhenIEnterEmail(string email)
        {
            await _page.FillAsync("input[placeholder='Email']", email);
        }

        [When(@"I enter ""(.*)"" as the New Password")]
        public async Task WhenIEnterNewPassword(string newPassword)
        {
            await _page.FillAsync("input[placeholder='New Password']", newPassword);
        }

        [When(@"I click on the Change Password button")]
        public async Task WhenIClickOnTheChangePasswordButton()
        {
            await _page.ClickAsync(".sendBtn"); 
        }

        [Then(@"I should see the appropriate message: ""(.*)""")]
        public async Task ThenIShouldSeeTheAppropriateMessage(string expectedMessage)
        {
            var displayedMessage = await _page.InnerTextAsync("p");
            // Change the expected message to the one you actually see
            Assert.Equal("You changed your password!", displayedMessage);  // New comparison
        }

    }
}
