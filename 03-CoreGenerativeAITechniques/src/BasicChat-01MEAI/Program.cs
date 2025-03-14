using System.Text;
using Azure;
using Azure.AI.OpenAI;
using System.ClientModel;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using OpenAI.Chat;


var azureAiSecret = Environment.GetEnvironmentVariable("AZURE_AI_SECRET") ?? throw new ArgumentNullException("AZURE_AI_SECRET");
var azureAiEndpoint = Environment.GetEnvironmentVariable("AZURE_AI_ENDPOINT") ?? throw new ArgumentNullException("AZURE_AI_ENDPOINT");
var azureAiModel =  Environment.GetEnvironmentVariable("AZURE_AI_MODEL") ?? throw new ArgumentNullException("AZURE_AI_MODEL");

var credential = new AzureKeyCredential(azureAiSecret);
var endpoint = new Uri(azureAiEndpoint);

// here we're building the prompt
StringBuilder prompt = new StringBuilder();
prompt.AppendLine("You will analyze the sentiment of the following product reviews. Each line is its own review. Output the sentiment of each review in a bulleted list and then provide a generate sentiment of all reviews. ");
prompt.AppendLine("I bought this product and it's amazing. I love it!");
prompt.AppendLine("This product is terrible. I hate it.");
prompt.AppendLine("I'm not sure about this product. It's okay.");
prompt.AppendLine("I found this product based on the other reviews. It worked for a bit, and then it didn't.");

// here we're calling the API using the Azure.AI.OpenAI namespace
Console.WriteLine("===============================");
ChatClient client = new AzureOpenAIClient(endpoint, credential)
                        .GetChatClient(azureAiModel);
ClientResult<ChatCompletion> response = await client.CompleteChatAsync(prompt.ToString());
foreach (var contentPart in response.Value.Content)
{
    Console.WriteLine(contentPart.Text);
}

// here we're calling the API using the Azure.AI.Inference namespace
Console.WriteLine("===============================");
IChatClient inferenceClient = new ChatCompletionsClient(endpoint, credential).AsChatClient(azureAiModel);
ChatResponse inferenceResponse = await inferenceClient.GetResponseAsync(prompt.ToString());
Console.WriteLine(inferenceResponse.Message);

Console.WriteLine("===============================");
