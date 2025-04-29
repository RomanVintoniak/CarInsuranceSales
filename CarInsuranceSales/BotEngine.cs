using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace CarInsuranceSales;

public class BotEngine(ITelegramBotClient botClient)
{
    public async Task ListenToMessages()
    {
        using var cts = new CancellationTokenSource();

        botClient.StartReceiving(
            updateHandler: HandleUpdate,
            errorHandler: HandleError,
            cancellationToken: cts.Token
        );

        var me = await botClient.GetMe();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
    }

    private async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        if (update.Message!.Text == "/start")
        {
            await botClient.SendMessage(update.Message.Chat.Id, "Welcome!");
        }
    }

    private Task HandleError(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }
}
