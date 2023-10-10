using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class ModelsTests
{
    private readonly ITestOutputHelper Console;
    public ModelsTests(ITestOutputHelper console)
    {
        Console = console;
    }

    #region ask_about_capitols_in_europe
    
    [Fact]
    public async Task ask_about_capitols_in_europe()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new []{new ChatMessage(ChatRole.User, "What are 5 the biggest cities in Spain?")});
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);
       
        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
        }
    }
    
    #endregion
}