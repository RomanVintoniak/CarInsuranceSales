using CarInsuranceSales.Core;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

        string policy = await policyProvider.GetInsurencePolicy(update.Message.Chat.Id);

        await botClient.SendMessage(
            update.Message.Chat.Id,
            policy,
            ParseMode.Markdown
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message.Text == "Buy" || update.Message.Text == "Agree";
}
