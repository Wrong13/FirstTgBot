using Telegram.Bot;

var bot = new TelegramBotClient("");
var me = await bot.GetMe();
Console.WriteLine($"Привет я юзер {me.Id} а имя {me.FirstName}");