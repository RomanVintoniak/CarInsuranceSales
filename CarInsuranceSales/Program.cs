using CarInsuranceSales;
using Telegram.Bot;

TelegramBotClient telegramBotClient = new TelegramBotClient(AccessTokens.TelegramBotToken);

BotEngine myBot = new BotEngine(telegramBotClient);

await myBot.ListenToMessages();