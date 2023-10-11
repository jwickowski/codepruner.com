using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ChatGPT;

public class ModelsTests
{
    private readonly ITestOutputHelper Console;
    public ModelsTests(ITestOutputHelper console)
    {
        Console = console;
    }

    #region ask_about_capitols_in_europe_1
    
    [Fact]
    public async Task ask_about_capitols_in_europe()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new []
        {
            new ChatMessage(ChatRole.User, "What are 5 the biggest cities in Polish?")
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);
       
        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // 1. Warsaw - The capital city of Poland, with a population of approximately 1.8 million.
            // 2. Kraków - The second-largest city, renowned for its historical significance and vibrant culture, with a population of approximately 780,000.
            // 3. Łódź - The third-largest city, known for its textile industry and thriving arts scene, with a population of approximately 690,000.
            // 4. Wrocław - The fourth-largest city and the cultural capital of Lower Silesia, with a population of approximately 640,000.
            // 5. Poznań - The fifth-largest city, famous for its trade fairs and historical landmarks, with a population of approximately 540,000.
        }
    }
    
    #endregion
    
    #region ask_about_capitols_in_europe_json
    
    [Fact]
    public async Task ask_about_capitols_in_europe_json()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new []
        {
            new ChatMessage(ChatRole.User, """
                                           What are 5 the biggest cities in Spain?. 
                                           Return the data in json format with fields: 'CityName', 'Population', 'Area'.
                                           """)
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);
       
        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // {
            //     "cities": [
            //     {
            //         "CityName": "Madrid",
            //         "Population": 3.2,
            //         "Area": 604.3
            //     },
            //     {
            //         "CityName": "Barcelona",
            //         "Population": 1.6,
            //         "Area": 101.9
            //     },
            //     {
            //         "CityName": "Valencia",
            //         "Population": 0.8,
            //         "Area": 134.6
            //     },
            //     {
            //         "CityName": "Seville",
            //         "Population": 0.7,
            //         "Area": 140.8
            //     },
            //     {
            //         "CityName": "Zaragoza",
            //         "Population": 0.7,
            //         "Area": 973.8
            //     }
            //     ]
            // }
        }
    }
    
    #endregion
    
    
    
    #region ask_about_capitols_in_europe_with_strict_units_json
    
    [Fact]
    public async Task ask_about_capitols_in_europe_with_strict_units_json()
    {
        var aiClient = OpenAIClientFactory.Create();

        var completionsOptions = new ChatCompletionsOptions(new []
        {
            new ChatMessage(ChatRole.User, """
                                           What are 3 the biggest cities in Sweden?.
                                           Return the data in json format with fields: 'CityName', 'Population', 'Area'.
                                           Area should be in km2.
                                           Population should not be rounded.
                                           """)
        });
        var result = await aiClient.GetChatCompletionsAsync("gpt-3.5-turbo", completionsOptions);
       
        if (result.HasValue)
        {
            Console.WriteLine(result.Value.Choices[0].Message.Content);
            // Example result:
            // {
            //     "cities": [
            //     {
            //         "CityName": "Stockholm",
            //         "Population": 975904,
            //         "Area": 188.00
            //     },
            //     {
            //         "CityName": "Gothenburg",
            //         "Population": 583056,
            //         "Area": 447.00
            //     },
            //     {
            //         "CityName": "Malmö",
            //         "Population": 319666,
            //         "Area": 77.06
            //     }
            //     ]
            // }
        }
    }
    
    #endregion
}
