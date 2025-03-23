
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

interface IOpenAIProxy
{
       Task<ChatCompletionMessage[]> SendChatMessage(string message);
}