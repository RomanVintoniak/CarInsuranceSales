using CarInsuranceSales.Core;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class DisagreePriceCommand(ChatClient chatClient) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ChatCompletion completion = chatClient.CompleteChat(Prompts.GetDisagreePricePrompt());

        await botClient.SendMessage(
            update.Message.Chat.Id,
            completion.Content[0].Text,
            replyMarkup: new string[] { "Buy", "Cancel" }.ToMarkup()
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Disagree";
}
