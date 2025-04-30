using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Interfaces;

/// <summary>
/// An action that bot can execute
/// </summary>
public interface IBotCommand
{
    /// <summary>
    /// Checks if command needs to be executed
    /// </summary>
    /// <param name="update">Telegram event <see cref="Update"/></param>
    bool ShouldExecute(Update update);

    /// <summary>
    /// Executes the telegram bot action
    /// </summary>
    /// <param name="botClient">Initialized botClient object</param>
    /// <param name="update">Telegram event <see cref="Update"/></param>
    /// <param name="token">Cancellation token</param>
    Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token);
}
