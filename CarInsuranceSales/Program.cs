using CarInsuranceSales;
using CarInsuranceSales.Commands;
using CarInsuranceSales.DocumentDataProviders;
using CarInsuranceSales.Interfaces;
using CarInsuranceSales.PolicyProviders;
using Mindee;
using Telegram.Bot;

IDocumentDataProvider dataProvider = new MindeeDocumentDataProvider(new MindeeClient(Constants.MindeeApiKey));

IPolicyProvider policyProvider = new FakePolicyProvider();

IReadOnlyList<IBotCommand> commands = new List<IBotCommand>
{
    new WelcomeCommand(),
    new ProcessDocumentsCommand(dataProvider),
    new ConfirmDocumentsDataCommand(),
    new ResubmitDocumentsCommand(),
    new DisagreePriceCommand(),
    new BuyCarInsuranceCommand(policyProvider),
    new CancelBuyingCarInsuranceCommand()
};

TelegramBotClient telegramBotClient = new TelegramBotClient(Constants.TelegramBotToken);

BotEngine myBot = new BotEngine(telegramBotClient, commands);

await myBot.ListenToMessages();