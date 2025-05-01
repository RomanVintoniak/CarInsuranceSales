namespace CarInsuranceSales.Core.Configuration;

/// <summary>
/// Application configuration options
/// </summary>
public class AppOptions
{
    public string TelegramBotToken { get; set; }
    public string MindeeApiKey { get; set; }
    public string OpenAiApiKey { get; set; }
    public string OpenAiChatModelName { get; set; }
    public string PolicyDocumentUrl { get; set; }
}
