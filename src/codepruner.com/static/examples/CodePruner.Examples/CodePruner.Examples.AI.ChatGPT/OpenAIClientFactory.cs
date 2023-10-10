namespace CodePruner.Examples.AI.ChatGPT;

public static class OpenAIClientFactory
{
    private const string apiKey = "OPEN_AI_API_KEY";
    public static OpenAIClient Create()
    {
        return new OpenAIClient(apiKey);
    }
}