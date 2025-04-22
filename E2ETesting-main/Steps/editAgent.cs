using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class EditAgentSteps
{
    private readonly IPage _page;
    private string _agentId;

    public EditAgentSteps()
    {
        var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();
        var browser = playwright.Chromium.LaunchAsync(new() { Headless = true }).GetAwaiter().GetResult();
        _page = browser.NewPageAsync().GetAwaiter().GetResult();
    }

    [Given(@"I am on the Agents List page")]
    public async Task GivenOnAgentsListPage()
    {
        await _page.GotoAsync("http://localhost:3000/agentslist");
        await _page.WaitForSelectorAsync("table");
    }

    [When(@"I click Edit for agent with ID ""(.*)""")]
    public async Task WhenClickEditForAgent(string agentId)
    {
        _agentId = agentId;
        await _page.ClickAsync($"tr:has(td:first-child:text-is('{_agentId}')) button.edit-button");
        await _page.WaitForSelectorAsync(".EditArea", new() { State = WaitForSelectorState.Visible });
    }

    [When(@"I update first name to ""(.*)""")]
    public async Task WhenUpdateFirstName(string value)
    {
        await _page.FillAsync(".EditArea input[name='firstName']", value);
    }

    [When(@"I update last name to ""(.*)""")]
    public async Task WhenUpdateLastName(string value)
    {
        await _page.FillAsync(".EditArea input[name='lastName']", value);
    }

    [When(@"I update email to ""(.*)""")]
    public async Task WhenUpdateEmail(string value)
    {
        await _page.FillAsync(".EditArea input[name='email']", value);
    }

    [When(@"I click Save")]
    public async Task WhenClickSave()
    {
        await _page.ClickAsync(".EditArea button.save-button");
        await _page.WaitForTimeoutAsync(2000);
        await _page.ReloadAsync();
    }

    [Then(@"I should see first name ""(.*)"" in the agents list")]
    public async Task ThenVerifyFirstName(string value)
    {
        var actual = await _page.TextContentAsync(
            $"tr:has(td:first-child:text-is('{_agentId}')) td:nth-child(2)");
        Assert.Equal(value, actual?.Trim());
    }

    [Then(@"I should see email ""(.*)"" in the agents list")]
    public async Task ThenVerifyEmail(string value)
    {
        var actual = await _page.TextContentAsync(
            $"tr:has(td:first-child:text-is('{_agentId}')) td:nth-child(4)");
        Assert.Equal(value, actual?.Trim());
    }
}
