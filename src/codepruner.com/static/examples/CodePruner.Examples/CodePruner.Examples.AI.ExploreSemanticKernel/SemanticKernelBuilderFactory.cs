using Microsoft.SemanticKernel;

namespace CodePruner.Examples.AI.ExploreSemanticKernel;

public class SemanticKernelBuilderFactory
{
    private  const string  ApiKey = "API_KEY";
        
    public KernelBuilder Create(string model = "gpt-3.5-turbo")
    {
        var builder = new KernelBuilder();
        builder.WithOpenAIChatCompletionService(model, ApiKey);

        return builder;
    }
}