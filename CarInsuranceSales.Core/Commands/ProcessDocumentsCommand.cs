using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

/// <inheritdoc/>
public class ProcessDocumentsCommand(IDocumentDataProvider dataProvider) : IBotCommand
{
    /// <inheritdoc/>
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var photo = update.Message.Photo.Last();

        var fileStream = await DownloadFile(botClient, photo);

        try
        {
            var response = await dataProvider.GetDocumentData(fileStream);

            await botClient.SendMessage(
                update.Message.Chat.Id,
                response.ToString(),
                replyMarkup: new string[] { "Confirm data", "Resubmit data" }.ToMarkup()
            );

        } catch (ApplicationException appEx)
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                appEx.Message
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
