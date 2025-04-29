using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using CarInsuranceSales.Interfaces;

namespace CarInsuranceSales;

public class BotEngine(ITelegramBotClient botClient, IReadOnlyList<IBotCommand> commands)
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
        var command = commands.FirstOrDefault(c => c.ShouldExecute(update));

        if (command != null)
        {
            await command.Execute(botClient, update, token);
        }
        else
        {
            await botClient.SendMessage(update.Message.Chat.Id, "Command is not supported");
        }
    }

    private Task HandleError(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }
}
