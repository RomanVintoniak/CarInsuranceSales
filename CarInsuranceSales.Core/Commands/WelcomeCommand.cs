using CarInsuranceSales.Core;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class WelcomeCommand(ChatClient chatClient) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ChatCompletion completion = chatClient.CompleteChat(Prompts.GetWelcomePrompt());

        await botClient.SendMessage(
            update.Message.Chat.Id,
            completion.Content[0].Text
        );
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message != null && update.Message.Text == "/start";

    //$"Welcome! I'm {botClient.GetMyName().Result.Name}\n\n Send me your documents"
}
