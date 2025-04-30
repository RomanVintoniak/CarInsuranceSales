using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class DisagreePriceCommand : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            "I'm so sorry, but 100 USD is the only available price",
            replyMarkup: new string[] { "Buy", "Cancel" }.ToMarkup()
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Disagree";
}
