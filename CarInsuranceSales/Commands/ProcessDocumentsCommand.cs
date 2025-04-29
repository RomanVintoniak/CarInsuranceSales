using CarInsuranceSales.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Commands;

public class ProcessDocumentsCommand(IDocumentDataProvider dataProvider) : IBotCommand
{
    public async Task Execute(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var photo = update.Message.Photo.Last();

        string base64Photo = await DownloadFileAsBase64(botClient, photo);

        var response = await dataProvider.GetDocumentData(base64Photo);

        await botClient.SendMessage(
            update.Message.Chat.Id,
            response.ToString(),
            replyMarkup: new string[] { "Confirm data", "Resubmit data" }.ToMarkup()
        );
    }

    public bool ShouldExecute(Update update) => update.Message != null && update.Message.Photo != null;

    private static async Task<string> DownloadFileAsBase64(ITelegramBotClient botClient, PhotoSize photo)
    {
        using var memoryStream = new MemoryStream();

        var file = await botClient.GetInfoAndDownloadFile(photo.FileId, memoryStream);

        Console.WriteLine($"File size: {file.FileSize} | File Path: {file.FilePath}");

        return Convert.ToBase64String(memoryStream.ToArray());
    }
}
