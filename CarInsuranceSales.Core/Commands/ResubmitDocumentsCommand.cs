using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class ResubmitDocumentsCommand : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            "Please send your documents again"
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Resubmit data";
}
