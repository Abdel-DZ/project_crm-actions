namespace E2ETesting.Steps
{
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    [Scope(Tag = "deleteAgent")] // Scope for 'deleteAgent' tag
    public class DeleteAgentSteps
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

        [When(@"I click the Delete button for an agent with ID ""(.*)""")]
        public async Task WhenIClickTheDeleteButtonForAnAgent(string agentId)
        {
            // Select the row where the first column (ID) matches agentId
            var rowSelector = $"table tbody tr:has(td:first-child:text-is('{agentId}'))";

            // Wait for that row to be visible
            await _page.WaitForSelectorAsync(rowSelector);

            // Click the delete button in the last column of that row
            await _page.ClickAsync($"{rowSelector} td:last-child button.delete-button");
        }

        [Then(@"I should no longer see the agent with ID ""(.*)"" in the list")]
        public async Task ThenIShouldNoLongerSeeTheAgentInTheList(string agentId)
        {
            var rowSelector = $"table tbody tr:has(td:first-child:text-is('{agentId}'))";
            var isVisible = await _page.IsVisibleAsync(rowSelector);
            Assert.False(isVisible);
        }
    }
}
