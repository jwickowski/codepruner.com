using Microsoft.SemanticKernel;

namespace CodePruner.Examples.AI.ExploreSemanticKernel;

public class ExploringSemanticKernel
{
    #region create_semantic_kernel

    [Fact]
    public void HowToCreateSemanticKernel()
    {
        var builder = new KernelBuilder();

        var model = "gpt-3.5-turbo";
        var apiKey = "API_KEY";
        builder.WithOpenAIChatCompletionService(model, apiKey);

        IKernel kernel = builder.Build();
        Assert.NotNull(kernel);
    }

    #endregion
}