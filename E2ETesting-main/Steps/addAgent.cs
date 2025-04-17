namespace E2ETesting.Steps
{
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    [Scope(Tag = "addAgent")] // Scope for 'addAgent' tag
    public class AddAgentSteps
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        private string _emailToCheck; // Store email for validation

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

        [Given(@"I am on the Agents List page")]
        public async Task GivenIAmOnTheAgentsListPage()
        {
            await _page.GotoAsync("http://localhost:3000/agentslist");
        }

        [When(@"I enter ""(.*)"" as the First Name")]
        public async Task WhenIEnterFirstName(string firstName)
        {
            await _page.FillAsync("input[placeholder='Enter agent firstname']", firstName);
        }

        [When(@"I enter ""(.*)"" as the Last Name")]
        public async Task WhenIEnterLastName(string lastName)
        {
            await _page.FillAsync("input[placeholder='Enter agent lastname']", lastName);
        }

        [When(@"I enter ""(.*)"" as the Email")]
        public async Task WhenIEnterEmail(string email)
        {
            await _page.FillAsync("input[placeholder='Enter agent email']", email);
            _emailToCheck = email; // Save the email for later validation
        }

        [When(@"I enter ""(.*)"" as the Password")]
        public async Task WhenIEnterPassword(string password)
        {
            await _page.FillAsync("input[placeholder='Enter agent password']", password);
        }

        [When(@"I click on the Add button")]
        public async Task WhenIClickOnTheAddButton()
        {
            await _page.ClickAsync("button.add-button");

            // Wait for the page to reload (refresh) to show the new agent data
            await _page.ReloadAsync();
        }
        
        [Then(@"I should see the new agent in the agents list")]
        public async Task ThenIShouldSeeTheNewAgentInTheAgentsList()
        {
            // Wait for the table rows to load after page refresh
            await _page.WaitForSelectorAsync("table tbody tr");

            // Get the emails of all agents listed (assuming email is in the 4th column)
            var agentEmails = await _page.EvaluateAsync<string[]>(@"
            Array.from(document.querySelectorAll('table tbody tr td:nth-child(4)')) // Assuming email is in the 4th column
            .map(td => td.innerText)");

            // Assert that the new agent email is in the list
            Assert.Contains(_emailToCheck, agentEmails);
        }
    }
}
