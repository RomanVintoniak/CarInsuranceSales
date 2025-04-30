using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class ConfirmDocumentsDataCommand : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            "The fixed price for the insurance is 100 USD."
        );

        await botClient.SendMessage(
            update.Message.Chat.Id,
            "Do you agree with the price ?",
            replyMarkup: new string[] { "Agree", "Disagree" }.ToMarkup()
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Confirm data";
}
