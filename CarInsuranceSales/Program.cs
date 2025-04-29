using CarInsuranceSales;
using CarInsuranceSales.Factory;
using Telegram.Bot;

var dataProvider = new DocumentDataProviderFactory().GetDocumentDataProvider();

TelegramBotClient telegramBotClient = new TelegramBotClient(Constants.TelegramBotToken);

BotEngine myBot = new BotEngine(telegramBotClient, dataProvider);

await myBot.ListenToMessages();