using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

public class BuyCarInsuranceCommand : IBotCommand
{
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
                update.Message.Chat.Id,
                "Here is your Insurance Policy Document"
            );

        await botClient.SendDocument(
            update.Message.Chat.Id,
            Constants.PolicyDocumentUrl
        );
    }

    public bool ShouldExecute(Update update) => update.Message.Text == "Buy" || update.Message.Text == "Agree";
}
