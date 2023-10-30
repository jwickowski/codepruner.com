using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace CodePruner.Examples.AI.ExploreSemanticKernel;

public class ExploringSemanticKernel
{
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
}