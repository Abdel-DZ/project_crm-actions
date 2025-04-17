using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Microsoft.Playwright;

namespace E2ETesting.Steps
{
    [Binding]
    [Scope(Tag = "chat")]
    public class ChatSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IPage _page;
        private IBrowser _browser;

        public ChatSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            _page = await _browser.NewPageAsync();
            _scenarioContext["page"] = _page;
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _page.CloseAsync();
            await _browser.CloseAsync();
        }

        [Given(@"I am on the Chat page with token ""(.*)""")]
        public async Task GivenIAmOnTheChatPageWithToken(string token)
        {
            await _page.GotoAsync($"http://localhost:3000/chat/{token}");
        }

        [When(@"I enter ""(.*)"" as the username")]
        public async Task WhenIEnterAsTheUsername(string username)
        {
            await _page.FillAsync("input[name='username']", username);
        }

        [When(@"I blur the username input")]
        public async Task WhenIBlurTheUsernameInput()
        {
            await _page.FocusAsync("input[name='message']");
        }

        [Then(@"the username input should be disabled")]
        public async Task ThenTheUsernameInputShouldBeDisabled()
        {
            var isDisabled = await _page.IsDisabledAsync("input[name='username']");
            Assert.True(isDisabled);
        }

        [When(@"I type ""(.*)"" as the message")]
        public async Task WhenITypeAsTheMessage(string msg)
        {
            await _page.FillAsync("input[name='message']", msg);
        }

        [When(@"I leave the message empty")]
        public async Task WhenILeaveTheMessageEmpty()
        {
            await _page.FillAsync("input[name='message']", "");
        }

        [When(@"I click on the Send button")]
        public async Task WhenIClickOnTheSendButton()
        {
            await _page.ClickAsync("button");
        }

        [Then(@"I should see the message ""(.*)"" from ""(.*)"" in the chat")]
        public async Task ThenIShouldSeeTheMessageFromInTheChat(string message, string user)
        {
            var expectedText = $"{user}: {message}";
            await _page.WaitForTimeoutAsync(500); // Wait a bit for UI update
            var chatContent = await _page.InnerTextAsync(".chatRuta");
            Console.WriteLine("Chat content:\n" + chatContent); // optional debug
            Assert.Contains(expectedText, chatContent);
        }

        [Then(@"the message should not be sent")]
        public async Task ThenTheMessageShouldNotBeSent()
        {
            var content = await _page.InnerTextAsync(".chatRuta");
            Assert.DoesNotContain("tester:", content);
        }

        [Then(@"I should see the animated background canvas")]
        public async Task ThenIShouldSeeTheAnimatedBackgroundCanvas()
        {
            var canvas = await _page.QuerySelectorAsync("canvas.canvas-bg");
            Assert.NotNull(canvas);
        }
    }
}
