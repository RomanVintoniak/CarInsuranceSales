using CarInsuranceSales.Core;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class ResubmitDocumentsCommand(ChatClient chatClient) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ChatCompletion completion = chatClient.CompleteChat(Prompts.GetResubmitDocumentsPrompt());

        await botClient.SendMessage(
            update.Message.Chat.Id,
            completion.Content[0].Text
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Resubmit data";
}
