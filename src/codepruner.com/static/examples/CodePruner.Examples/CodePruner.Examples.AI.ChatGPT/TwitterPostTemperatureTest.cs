using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class TwitterPostTemperatureTest
{
    private readonly ITestOutputHelper Console;
    public TwitterPostTemperatureTest(ITestOutputHelper console)
    {
        Console = console;
    }

    private string prompt = """
                            Write a attractive Twitter post about AI in bookkeeping?
                            It should be 5 sentences long and there should be references music bands.
                            Use rhymes.
                            """;

    #region twiter_post_low_temp

    [Fact]
    public async Task twiter_post_low_temp()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, prompt)
        });
        completionsOptions.Temperature = 0;
        completionsOptions.MaxTokens = 50;
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
        }
    }

    #endregion
    
    #region twiter_post_medium_temp

    [Fact]
    public async Task twiter_post_medium_temp()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, prompt)
        });
        completionsOptions.Temperature = 1;
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
           
        }
    }

    #endregion
    
    #region twiter_post_high_temp

    [Fact]
    public async Task twiter_post_high_temp()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, prompt)
        });
        completionsOptions.Temperature = 1.8f;
        completionsOptions.MaxTokens = 50;
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
           
        }
    }

    #endregion
}
