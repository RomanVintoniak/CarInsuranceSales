using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

public class CancelBuyingCarInsuranceCommand : IBotCommand

{
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            "Car insurance purchase successfully canceled"
        );
    }

    public bool ShouldExecute(Update update) => update.Message.Text == "Cancel";

}
