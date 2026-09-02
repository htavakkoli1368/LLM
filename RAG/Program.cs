using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY")
            ?? throw new Exception("GROQ_API_KEY is not set");

        // 1. Read the document
       string document = await File.ReadAllTextAsync("company-policy.txt");

        // 2. User question
        string question =
            "what is DI";

        // 3. Retrieve relevant information
        string context =  FindRelevantText(document, question);    

        // 4. Send context + question to the LLM
        using HttpClient client = new();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                apiKey);

        var body = new
        {
            model = "openai/gpt-oss-20b",
            messages = new[]
            {
            new
            {
                role = "system",
                content = """
                          Answer the user's question using only
                          the provided context.

                          If the answer is not in the context,
                          say you don't know.
                          """
            },
            new
            {
                role = "user",
                content = $"""
                          Context:
                          {context}

                          Question:
                          {question}
                          """
            }
        }
        };

        string json =
            JsonSerializer.Serialize(body);

        var response = await client.PostAsync(
            "https://api.groq.com/openai/v1/chat/completions",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"));

        response.EnsureSuccessStatusCode();

        // 5. Read the response
        string result =
            await response.Content.ReadAsStringAsync();

        using JsonDocument documentJson =
            JsonDocument.Parse(result);

        string answer = documentJson
            .RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "";

        Console.WriteLine("\nLLM Answer:");
        Console.WriteLine(answer);
    }

  private static string FindRelevantText(string document,string question)
    {
        var keywords = question
            .ToLower()
            .Split(' ',StringSplitOptions.RemoveEmptyEntries);

        var lines = document
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim());

        var relevantLines = lines
            .Where(line =>
                keywords.Any(keyword => line.ToLower().Contains(keyword)))
            .ToList();

        return string.Join( Environment.NewLine,relevantLines);
    }
}