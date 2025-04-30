using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

public class ResubmitDocumentsCommand : IBotCommand
{
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            "Please send your documents again"
        );
    }

    public bool ShouldExecute(Update update) => update.Message.Text == "Resubmit data";
}
