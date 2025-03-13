using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using System.ClientModel;

var azureAiSecret = Environment.GetEnvironmentVariable("AZURE_AI_SECRET") ?? throw new InvalidOperationException("Missing AZURE_AI_SECRET environment variable.");
var azureAiEndpoint = Environment.GetEnvironmentVariable("AZURE_AI_ENDPOINT") ?? throw new InvalidOperationException("Missing AZURE_AI_ENDPOINT environment variable.");
var azureAiModel = Environment.GetEnvironmentVariable("AZURE_AI_MODEL") ?? throw new InvalidOperationException("Missing AZURE_AI_MODEL environment variable.");

var endpoint = new Uri(azureAiEndpoint); // e.g. "https://< your hub name >.openai.azure.com/"
var apiKey = new ApiKeyCredential(azureAiSecret);

IChatClient client = new AzureOpenAIClient(
    endpoint,
    apiKey)
.AsChatClient(azureAiModel);

var response = await client.GetResponseAsync("What is AI? Answer in one sentence.");

Console.WriteLine(response.Message);