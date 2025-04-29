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
        if (update.Message != null && update.Message.Text == "/start")
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                $"Welcome! I'm {botClient.GetMyName().Result.Name}\n\n Send me your documents"
            );
        }

        if (update.Message != null && update.Message.Photo != null)
        {
            var photo = update.Message.Photo.Last();

            string base64Photo = await DownloadFileAsBase64(botClient, photo);

            var response = await dataProvider.GetDocumentData(base64Photo);

            await botClient.SendMessage(
                update.Message.Chat.Id,
                response.ToString(),
                replyMarkup: new string[] {"Confirm data", "Resubmit data"}.ToMarkup()
            );
        }

        if (update.Message.Text == "Confirm data")
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                "The fixed price for the insurance is 100 USD."
            );

            await botClient.SendMessage(
                update.Message.Chat.Id,
                "Do you agree with the price ?",
                replyMarkup: new string[] { "Agree", "Disagree" }.ToMarkup()
            );
        }

        if (update.Message.Text == "Resubmit data")
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                "Please send your documents again"
            );
        }

        if (update.Message.Text == "Disagree")
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                "I'm so sorry, but 100 USD is the only available price",
                replyMarkup: new string[] { "Buy", "Cancel" }.ToMarkup()
            );
        }

        if (update.Message.Text == "Buy" || update.Message.Text == "Agree")
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                "Here is your Insurance Policy Document"
            );

            await botClient.SendDocument(
                update.Message.Chat.Id, 
                Constants.PolicyDocumentUrl
            );
        }

        if (update.Message.Text == "Cancel")
        {
            await botClient.SendMessage(
                update.Message.Chat.Id,
                "Car insurance purchase successfully canceled"
            );
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
