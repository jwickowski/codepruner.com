using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class SystemRoleTests
{
    private readonly ITestOutputHelper Console;

    public SystemRoleTests(ITestOutputHelper console)
    {
        Console = console;
    }

    #region default_role_jerzy_wickowski_in_desperation

    [Fact]
    public async Task default_role_jerzy_wickowski_in_desperation()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "Who is Jerzy Wickowski in 'Desperation' book written by Stephen King in the end of 2nd chapter?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Result: 
            // Jerzy Wickowski is a person mentioned briefly in the second chapter of Stephen King's book "Desperation".
            // Jerzy is described as a plumber who was driving on Route 50 in Nevada when he was apprehended by the antagonist of the story, Collie Entragian, a possessed police officer.
            // Jerzy is forced into the Desperation police station, and his fate remains uncertain at the end of the second chapter.
        }
    }

    #endregion
    
    #region honest_role_jerzy_wickowski_in_desperation

    [Fact]
    public async Task honest_role_jerzy_wickowski_in_desperation()
    {
        var aiClient = OpenAIClientFactory.Create();
        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.System, "You will tell only the truth, if you don't know the answer, you will say only 'I_DONT_KNOW"),
            new ChatMessage(ChatRole.User, "Who is Jerzy Wickowski in 'Desperation' book written by Stephen King in the end of 2nd chapter?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Result: 
            // I_DONT_KNOW
        }
    }

    #endregion
}
