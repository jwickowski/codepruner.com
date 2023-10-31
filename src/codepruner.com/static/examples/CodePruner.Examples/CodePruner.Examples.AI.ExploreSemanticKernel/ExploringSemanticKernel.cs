using CodePruner.Examples.AI.ExploreSemanticKernel.Plugins;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Planners;
using Microsoft.SemanticKernel.TemplateEngine;
using Xunit.Abstractions;

namespace CodePruner.Examples.AI.ExploreSemanticKernel;

public class ExploringSemanticKernel
{
    private readonly ITestOutputHelper Console;

    public ExploringSemanticKernel(ITestOutputHelper console)
    {
        Console = console;
    }

    #region create_simple_semantic_kernel

    [Fact]
    public void create_simple_semantic_kernel()
    {
        var builder = new KernelBuilder();

        var model = "gpt-3.5-turbo";
        var apiKey = "API_KEY";
        builder.WithOpenAIChatCompletionService(model, apiKey);

        IKernel kernel = builder.Build();
        Assert.NotNull(kernel);
    }

    #endregion

    #region create_semantic_kernel_with_console_logging

    [Fact]
    public void create_semantic_kernel_with_console_logging()
    {
        var builder = new KernelBuilder();

        var model = "gpt-3.5-turbo";
        var apiKey = "API_KEY";
        builder.WithOpenAIChatCompletionService(model, apiKey);
        // add nuget: Microsoft.Extensions.Logging.Console
        builder.WithLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));

        IKernel kernel = builder.Build();
        Assert.NotNull(kernel);
    }

    #endregion

    #region semantic_function_bike_joke_inline

    [Fact]
    public async Task semantic_function_bike_joke_inline()
    {
        var kernel = SemanticKernelBuilderFactory.Create().Build();

        string bikeJokePrompt = """
                                Write a joke about cyclists
                                It must be funny
                                It should be sarcastic
                                It should be in style of dad jokes
                                It should be written in simple language
                                It should be about bike and {{$input}}
                                """;

        var bikeJokeRequestSettings = new OpenAIRequestSettings
        {
            MaxTokens = 100,
            Temperature = 0.8,
        };

        var promptConfig = new PromptTemplateConfig();
        promptConfig.ModelSettings.Add(bikeJokeRequestSettings);

        var bikeJokePromptTemplate = new PromptTemplate(
            bikeJokePrompt,
            promptConfig,
            kernel
        );

        var bikeJokeFunction =
            kernel.RegisterSemanticFunction("BikePlugin", "BikeJoke", promptConfig, bikeJokePromptTemplate);

        var driverResponseResult = await kernel.RunAsync("Car driver", bikeJokeFunction);
        var driverResult = driverResponseResult.GetValue<string>();
        Console.WriteLine(driverResult);
        // Example result:
        // Why did the car driver get jealous of the cyclist?
        //     Because the cyclist was always "pedaling" their way to success while the car driver was stuck in traffic, "wheel-y" wishing they could join the fun!

        var devResponseResult = await kernel.RunAsync("Software developer", bikeJokeFunction);
        var devResult = devResponseResult.GetValue<string>();
        Console.WriteLine(devResult);
        // Example result:
        // Why did the software developer become a cyclist?
        //     Because they wanted to experience even more bugs on the road!
    }

    #endregion

    #region semantic_function_bike_joke_files

    [Fact]
    public async Task semantic_function_bike_joke_files()
    {
        var kernel = SemanticKernelBuilderFactory.Create().Build();
        var pluginsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Plugins");
        var bikePluginFunctions = kernel.ImportSemanticFunctionsFromDirectory(pluginsPath, "BikePlugin");
        var driverResponseResult = await kernel.RunAsync("UX Designer", bikePluginFunctions["BikeJoke"]);

        var driverResult = driverResponseResult.GetValue<string>();
        Console.WriteLine(driverResult);
        // Example result:
        // Why did the cyclist become a UX Designer?
        //     Because they wanted to experience the thrill of going in circles and getting nowhere, just like riding a bike!
    }

    #endregion

    #region native_function_bike_size

    [Fact]
    public async Task native_function_bike_size()
    {
        var kernel = SemanticKernelBuilderFactory.Create().Build();
        var pluginFunctions = kernel.ImportFunctions(new BikeApiPlugin(), "BikeSizePlugin");
        var responseResult = await kernel.RunAsync("175", pluginFunctions["CalculateBikeSize"]);

        var result = responseResult.GetValue<string>();
        Assert.Equal("M", result);
    }

    #endregion

    #region manual_pipeline

    [Fact]
    public async Task manual_pipeline()
    {
        var kernel = SemanticKernelBuilderFactory.Create().Build();

        var getTobTitleFunction = kernel.CreateSemanticFunction(
            """
            What is my Job, based on description?
            Result should max two words like: Software Developer, Football Player, etc.
            DESCRIPTION:
            {{$input}}
            """,
            new OpenAIRequestSettings() {Temperature = 0.0});

        var pluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        kernel.ImportSemanticFunctionsFromDirectory(pluginsPath, "BikePlugin");

        var responseResult = await kernel.RunAsync("I mostly create a code and sometimes I do a deploy. I do also some research in AI",
            getTobTitleFunction,
            kernel.Functions.GetFunction("BikePlugin", "BikeJoke"));

        var result = responseResult.GetValue<string>();
        Console.WriteLine(result);
        // Example result:
        // Why did the cyclist become an AI researcher?
        //     Because they wanted to pedal their way to the future, but realized they could just program a bike to do it for them! Talk about taking the easy route!
        
        
        
        var responseResult2 = await kernel.RunAsync("I try to find a bad guys and put them in jail. I also like to drive a car and shoot a gun.",
            getTobTitleFunction,
            kernel.Functions.GetFunction("BikePlugin", "BikeJoke"));

        var result2 = responseResult2.GetValue<string>();
        Console.WriteLine(result2);
        //Example result:
        // Why did the Law Enforcement Officer give the cyclist a ticket?
        //     Because the cyclist was going too fast... for a snail! Talk about breaking the sound barrier on two wheels! 🚴‍♂️💨
        //     But hey, at least the snail didn't need a helmet! Safety first, right? 😄
    }


    #endregion


    #region sequential_planner

    [Fact]
    public async Task sequential_planner()
    {
        var kernel = SemanticKernelBuilderFactory.Create().Build();
        var pluginsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Plugins");
        kernel.ImportSemanticFunctionsFromDirectory(pluginsPath, "BikePlugin");
        kernel.ImportFunctions(new BikeApiPlugin(), "BikeSizePlugin");

        var planner = new SequentialPlanner(kernel);
        var plan = await planner.CreatePlanAsync(
            "What bike size should I buy if I am 175cm tall? I also like riding in the forest. Mostly in hilly areas.");

        Console.WriteLine(string.Join(",", plan.Steps.Select(x => $"{x.PluginName}.{x.Name}")));
        // Example plan:
        // BikeSizePlugin.CalculateBikeSize,BikeSizePlugin.BikeForMe
        var responseResult = await kernel.RunAsync(plan);

        var result = responseResult.GetValue<string>();
        Console.WriteLine(result);
        // Example result:
        // You should get a M Mountain bike
    }

    #endregion
}