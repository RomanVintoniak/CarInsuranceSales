using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Interfaces;

public interface IBotCommand
{
    bool ShouldExecute(Update update);

    Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token);
}
