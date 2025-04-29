using CarInsuranceSales;
using CarInsuranceSales.Commands;
using CarInsuranceSales.Factory;
using CarInsuranceSales.Interfaces;
using Telegram.Bot;

var dataProvider = new DocumentDataProviderFactory().GetDocumentDataProvider();

IReadOnlyList<IBotCommand> commands = new List<IBotCommand>
{
    new WelcomeCommand(),
    new ProcessDocumentsCommand(dataProvider),
    new ConfirmDocumentsDataCommand(),
    new ResubmitDataCommand(),
    new DisagreePriceCommand(),
    new BuyCarInsuranceCommand(),
    new CancelBuyingCarInsuranceCommand()
};

TelegramBotClient telegramBotClient = new TelegramBotClient(Constants.TelegramBotToken);

BotEngine myBot = new BotEngine(telegramBotClient, commands);

await myBot.ListenToMessages();