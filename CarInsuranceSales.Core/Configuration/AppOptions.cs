namespace CarInsuranceSales.Core.Configuration;

public class AppOptions
{
    public string TelegramBotToken { get; set; }
    public string MindeeApiKey { get; set; }
    public string OpenAiApiKey { get; set; }
    public string PolicyDocumentUrl { get; set; }
}
