namespace E2ETesting.Steps
{
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class loginAdmin
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
        
        [Given(@"I am on the login page as an admin")]
        public async Task GivenIAmOnTheLoginPageAsAnAdmin()
        {
            await _page.GotoAsync("http://localhost:3000/login");
        }

        [When(@"I enter ""(.*)"" as my Email for admin")]
        public async Task WhenIEnterMyEmailForAdmin(string email)
        {
            await _page.FillAsync("input[placeholder='Email']", email);
        }

        [When(@"I enter ""(.*)"" as my Password for admin")]
        public async Task WhenIEnterMyPasswordForAdmin(string password)
        {
            await _page.FillAsync("input[placeholder='Password']", password);
        }

        [When(@"I select ""(.*)"" from the dropdown as an admin")]
        public async Task WhenISelectFromTheDropdownAsAdmin(string role)
        {
            await _page.ClickAsync(".postholderSelection");
            await _page.ClickAsync($".dropdown div:text('{role}')");
        }

        [When(@"I click on the login button as admin")]
        public async Task WhenIClickOnTheLoginButtonAsAdmin()
        {
            await _page.ClickAsync("button.sendBtn");
        }
        
        [Then(@"I should see a dashboard displayed for admin")]

        [Then(@"I should see a dashboard displayed for admin")]
        public async Task ThenIShouldSeeADashboardDisplayedForAdmin()
        {
            await _page.WaitForSelectorAsync("h1"); // Wait for the heading
            var header = await _page.InnerTextAsync("h1"); // Extract text from the heading
            Assert.Contains("Welcome to our CRM-System", header, StringComparison.OrdinalIgnoreCase); // Verify the correct text
        }



    }
}
