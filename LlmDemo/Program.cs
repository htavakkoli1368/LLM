using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


internal class Program
{
    private static async Task Main(string[] args)
    {

        string apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY")
     ?? throw new Exception("GROQ_API_KEY is not set");

        using HttpClient client = new();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        var body = new
        {
            model = "openai/gpt-oss-20b",
            messages = new[]
            {
        new
        {
            role = "user",
            content = "what is Dependency injection in asp.net core"
        }
    }
        };

        var json = JsonSerializer.Serialize(body);

        var response = await client.PostAsync(
            "https://api.groq.com/openai/v1/chat/completions",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var result = await response.Content.ReadAsStringAsync();

        using JsonDocument document = JsonDocument.Parse(result);

        string content = document
            .RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "";

        Console.WriteLine("\n[LLM Response]");
        Console.WriteLine(content);
    }
}