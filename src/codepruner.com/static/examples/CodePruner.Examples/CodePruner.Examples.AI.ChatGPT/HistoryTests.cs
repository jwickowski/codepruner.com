using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class HistoryTests
{
    private readonly ITestOutputHelper Console;

    public HistoryTests(ITestOutputHelper console)
    {
        Console = console;
    }

    #region lose_the_context

    [Fact]
    public async Task lose_the_context()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "What are 3 the biggest cities in Polish?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // The three biggest cities in Poland are:
            //
            // 1. Warsaw - The capital and largest city of Poland, with a population of over 1.8 million people.
            // 2. Kraków - The second-largest city in Poland, known for its rich history, culture, and architecture, with a population of around 786,000 people.
            // 3. Łódź - The third-largest city in Poland, famous for its textile industry and vibrant cultural scene, with a population of approximately 685,000 people.
        }

        completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "Return these values as json")
        });

        var result2 = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result2.HasValue)
        {
            Console.WriteLine(result2.Value.Choices[0].Message.Content);
            // Example result:
            // {"name": "John", "age": 25, "city": "New York"}
        }
    }

    #endregion

    #region keep_the_context

    [Fact]
    public async Task keep_the_context()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "What are 3 the biggest cities in Germany?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        completionsOptions.Messages.Add(result.Value.Choices[0].Message);
        completionsOptions.Messages.Add(new ChatMessage(ChatRole.User, "Return these values as json with fields: 'name', 'population', 'establishYear' "));
        var result2 = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);

        if (result2.HasValue)
        {
            Console.WriteLine(result2.Value.Choices[0].Message.Content);
            // Example result:
            // {
            //     [
            //     {
            //         "name": "Berlin",
            //         "population": "3,769,495",
            //         "establishYear": "1237"
            //     },
            //     {
            //         "name": "Hamburg",
            //         "population": "1,899,764",
            //         "establishYear": "808"
            //     },
            //     {
            //         "name": "Munich",
            //         "population": "1,558,395",
            //         "establishYear": "1158"
            //     }
            //     ]
            // }
        }
    }

    #endregion
    
    #region prepare_the_context

    [Fact]
    public async Task prepare_the_context()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new[]
        {
            new ChatMessage(ChatRole.User, "What are 3 the biggest cities in Poland?"),
            new ChatMessage(ChatRole.Assistant, "Warsaw, Jasło, Mediolan"),
            new ChatMessage(ChatRole.User, "Where they are located?"),
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);
        Console.WriteLine(result.Value.Choices[0].Message.Content);
        // Example result:
        // Warsaw is located in east-central Poland, Jasło is located in southeastern Poland, and Mediolan is actually a city in Italy, not in Poland.
    }

    #endregion
}