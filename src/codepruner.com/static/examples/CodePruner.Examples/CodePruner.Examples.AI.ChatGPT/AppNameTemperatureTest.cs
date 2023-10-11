using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class AppNameTemperatureTest
{
    private readonly ITestOutputHelper Console;

    public AppNameTemperatureTest(ITestOutputHelper console)
    {
        Console = console;
    }

    #region application_name_low_temp

    [Fact]
    public async Task application_name_low_temp()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "Can you provide me 3 short fancy names for an application to calculate boring reports?")
        });
        completionsOptions.Temperature = 1.8f;
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // 1. Elysian Analytics
            // 2. Opulent Insights
            // 3. Regal Reports
        }
    }

    #endregion
    
    #region application_name_medium_temp

    [Fact]
    public async Task application_name_medium_temp()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "Can you provide me 3 short fancy names for an application to calculate boring reports?")
        });
        completionsOptions.Temperature = 1;
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // 1. Elysian Analytics
            // 2. LuxeMetrics
            // 3. Opulent Insights
        }
    }

    #endregion
    
    #region application_name_high_temp

    [Fact]
    public async Task application_name_high_temp()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "Can you provide me 3 short fancy names for an application to calculate boring reports?")
        });
        completionsOptions.Temperature = 2;
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // 1. Eoplectify
            // 2. Exancitus Veˈdonderovalesh
            // 3. Spectrofuture Analytics
        }
    }

    #endregion

}
