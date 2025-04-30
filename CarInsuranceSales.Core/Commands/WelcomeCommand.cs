using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class WelcomeCommand : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendMessage(
            update.Message.Chat.Id,
            $"Welcome! I'm {botClient.GetMyName().Result.Name}\n\n Send me your documents"
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message != null && update.Message.Text == "/start";
}
