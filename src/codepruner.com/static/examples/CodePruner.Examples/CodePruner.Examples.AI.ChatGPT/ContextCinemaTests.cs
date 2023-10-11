using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class ContextCinemaTests
{
    private readonly ITestOutputHelper Console;

    public ContextCinemaTests(ITestOutputHelper console)
    {
        Console = console;
    }

    #region prompt_without_context

    [Fact]
    public async Task prompt_without_context()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "When can I watch today evening?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // As an AI, I don't have access to current time or location information. However, you can check local TV listings, streaming platforms, or websites for movie theaters in your area to see what options are available for viewing this evening.
        }
    }

    #endregion
    
    #region prompt_with_context

    [Fact]
    public async Task prompt_with_context()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.System, """
                                             You are giving information about cinema and suggest people what to watch.
                                             Context:
                                             Name of the cinema: Big Screens
                                             Address: Main Movie street 17, Sydney
                                             Repertoire for today:
                                             17:30 - "The Lord of the Rings"
                                             18:25 - "Kill Bill"
                                             19:15 - "The Godfather"
                                             20:20 - "Alice in Wonderland"
                                             21:00 - "The Matrix"
                                             22:30 - "Saw: The Final Chapter"
                                             """),
            new ChatMessage(ChatRole.User, "When can I watch today after 7?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            //Example result:
            // After 7 PM today at Big Screens, you can watch "The Godfather" at 19:15 or "Alice in Wonderland" at 20:20.
        }
    }

    #endregion
}