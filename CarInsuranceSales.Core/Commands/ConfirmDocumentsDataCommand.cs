using CarInsuranceSales.Core;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class ConfirmDocumentsDataCommand(ChatClient chatClient) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ChatCompletion completion = chatClient.CompleteChat(Prompts.GetInsurancePricePrompt());

        await botClient.SendMessage(
            update.Message.Chat.Id,
            completion.Content[0].Text
        );

        completion = chatClient.CompleteChat(Prompts.GetAgreementAboutPricePrompt());

        await botClient.SendMessage(
            update.Message.Chat.Id,
            completion.Content[0].Text,
            replyMarkup: new string[] { "Agree", "Disagree" }.ToMarkup()
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Confirm data";
}
