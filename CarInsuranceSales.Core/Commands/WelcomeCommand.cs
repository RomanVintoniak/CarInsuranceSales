using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

public class WelcomeCommand : IBotCommand
{
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            $"Welcome! I'm {botClient.GetMyName().Result.Name}\n\n Send me your documents"
        );
    }

    public bool ShouldExecute(Update update) => update.Message != null && update.Message.Text == "/start";
}
