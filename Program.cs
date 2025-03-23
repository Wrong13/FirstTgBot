using System.Net.Http.Json;
using ConsoleAppOpenAI;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("",cancellationToken: cts.Token);
var me = await bot.GetMe();

bot.OnMessage += OnMessage;
bot.OnError += OnError;
bot.OnUpdate += OnUpdate;


Console.WriteLine($"{me.Username} запущен");
Console.ReadLine();
cts.Cancel();

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;
    Console.WriteLine($"Полученно {type} '{msg.Text}' в {msg.Chat}");
    await bot.SendMessage(msg.Chat, $"{msg.From} сказал: {msg.Text}");

    IOpenAIProxy chatOpenAI = new OpenAIProxy(
    apiKey: "",
    organizationId: "YOUR-ORGANIZATION-ID");   
    
        var results = await chatOpenAI.SendChatMessage(msg.Text);

        foreach (var item in results)
        {
            Console.WriteLine($"{item.Role}: {item.Content}");
        }
        Console.WriteLine("Ответ от него:");

}

async Task OnError(Exception ex, HandleErrorSource source)
{
    Console.WriteLine(ex);
}

async Task OnUpdate(Update update)
{
    if (update is {CallbackQuery: {} query})
    {
        await bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
        await bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
    }
}