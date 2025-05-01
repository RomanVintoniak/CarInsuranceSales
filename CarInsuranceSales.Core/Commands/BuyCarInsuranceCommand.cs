using CarInsuranceSales.Core;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class BuyCarInsuranceCommand(IPolicyProvider policyProvider, ChatClient chatClient) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ChatCompletion completion = chatClient.CompleteChat(Prompts.GetBuyingInsurancePrompt());

        await botClient.SendMessage(
            update.Message.Chat.Id,
            completion.Content[0].Text
        );

        string policyUri = await policyProvider.GetInsurencePolicy();

        await botClient.SendDocument(update.Message.Chat.Id, policyUri);
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Buy" || update.Message.Text == "Agree";
}
