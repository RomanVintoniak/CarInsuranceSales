using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using CarInsuranceSales.Interfaces;

namespace CarInsuranceSales;

public class BotEngine(ITelegramBotClient botClient, IDocumentDataProvider dataProvider)
{
    public async Task ListenToMessages()
    {
        using var cts = new CancellationTokenSource();

        botClient.StartReceiving(
            updateHandler: HandleUpdate,
            errorHandler: HandleError,
            cancellationToken: cts.Token
        );

        var me = await botClient.GetMe();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
    }

    private async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        if (update.Message!.Text == "/start")
        {
            await botClient.SendMessage(update.Message.Chat.Id, "Welcome!");
        }

        if (update.Message.Photo != null)
        {
            var photo = update.Message.Photo.Last();

            string base64Photo = await DownloadFileAsBase64(botClient, photo);

            var response = await dataProvider.GetDocumentData(base64Photo);

            await botClient.SendMessage(update.Message.Chat.Id, response.ToString());
        }
    }

    private static async Task<string> DownloadFileAsBase64(ITelegramBotClient botClient, PhotoSize photo)
    {
        using var memoryStream = new MemoryStream();

        var file = await botClient.GetInfoAndDownloadFile(photo.FileId, memoryStream);

        Console.WriteLine($"File size: {file.FileSize} | File Path: {file.FilePath}");

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    private Task HandleError(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }
}
