using CarInsuranceSales.Core;
using CarInsuranceSales.DataAccess.Repository;
using CarInsuranceSales.Interfaces;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class ProcessDocumentsCommand(IDocumentDataProvider dataProvider, 
    ChatClient chatClient, 
    IClientRepository clientRepository) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var photo = update.Message.Photo.Last();

        var fileStream = await DownloadFile(botClient, photo);

        try
        {
            ChatCompletion completion = chatClient.CompleteChat(Prompts.GetProccesingDocumentsPrompt());

            await botClient.SendMessage(
                update.Message.Chat.Id,
                completion.Content[0].Text
            );

            var response = await dataProvider.GetDocumentData(fileStream);

            var clientDto = response.ToClientDto(update.Message.Chat.Id);
            await clientRepository.Add(clientDto);

            completion = chatClient.CompleteChat(Prompts.GetProccessedDataConfirmationPrompt(response.ToString()));

            await botClient.SendMessage(
                update.Message.Chat.Id,
                completion.Content[0].Text,
                replyMarkup: new string[] { "Confirm data", "Resubmit data" }.ToMarkup()
            );

        } catch (Exception ex)
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                ex.Message
            );
        }
    }

    /// <inheritdoc/>
    public bool ShouldExecute(Update update) => update.Message != null && update.Message.Photo != null;

    /// <summary>
    /// Downloads the file from Telegram that the user sent
    /// </summary>
    /// <param name="botClient">Initialized botClient object</param>
    /// <param name="photo">Photo</param>
    /// <returns>MemoryStream containing the file</returns>
    private static async Task<MemoryStream> DownloadFile(ITelegramBotClient botClient, PhotoSize photo)
    {
        using var memoryStream = new MemoryStream();

        var file = await botClient.GetInfoAndDownloadFile(photo.FileId, memoryStream);

        return memoryStream;
    }
}
