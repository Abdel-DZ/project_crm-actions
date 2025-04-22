namespace E2ETesting.Steps
{
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class loginAgents
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        
        
        [BeforeScenario]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = true, SlowMo = 500 });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [AfterScenario]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Given(@"I am on the login page as an agent")]
        public async Task GivenIAmOnTheLoginPageAsAgent()
        {
            await _page.GotoAsync("http://localhost:3000/login");
        }

        [When(@"I enter ""(.*)"" as my Email for agent")]
        public async Task WhenIEnterMyEmailForAgent(string email)
        {
            await _page.FillAsync("input[placeholder='Email']", email);
        }

        [When(@"I enter ""(.*)"" as my Password for agent")]
        public async Task WhenIEnterMyPasswordForAgent(string password)
        {
            await _page.FillAsync("input[placeholder='Password']", password);
        }

        [When(@"I select ""(.*)"" from the dropdown as an agent")]
        public async Task WhenISelectFromTheDropdownAsAgent(string role)
        {
            await _page.ClickAsync(".postholderSelection");
            await _page.ClickAsync($".dropdown div:text('{role}')");
        }

        [When(@"I click on the login button as agent")]
        public async Task WhenIClickOnTheLoginButtonAsAgent()
        {
            await _page.ClickAsync("button.sendBtn");
        }
        
        [Then(@"I should see a dashboard displayed for agent")]
        public async Task ThenIShouldSeeADashboardDisplayedForAgent()
        {
            await _page.WaitForSelectorAsync("h1");
            var header = await _page.InnerTextAsync("h1");
            Assert.Contains("Welcome to our CRM-System", header, StringComparison.OrdinalIgnoreCase); // Check if text is part of the header
        }

    }
}
