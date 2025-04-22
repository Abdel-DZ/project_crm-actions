namespace E2ETesting.Steps
{
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    [Scope(Tag = "stateUpdate")] // Scope for 'stateUpdate' tag
    public class StateUpdateSteps
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

        [Given(@"I am on the Agents List page")]
        public async Task GivenIAmOnTheAgentsListPage()
        {
            await _page.GotoAsync("http://localhost:3000/agentslist");
        }

        [When(@"I add an agent with the name ""(.*)"" and email ""(.*)""")]
        public async Task WhenIAddAnAgent(string firstName, string email)
        {
            await _page.FillAsync("input[placeholder='Enter agent firstname']", firstName);
            await _page.FillAsync("input[placeholder='Enter agent email']", email);
            await _page.ClickAsync("button.add-button");
        }

        [Then(@"the agent list should include an agent with the name ""(.*)"" and email ""(.*)""")]
        public async Task ThenTheAgentListShouldIncludeAnAgent(string firstName, string email)
        {
            var agentRow = await _page.IsVisibleAsync($"tr:has(td:has-text('{firstName}')):has(td:has-text('{email}'))");
            Assert.True(agentRow);
        }
    }
}
