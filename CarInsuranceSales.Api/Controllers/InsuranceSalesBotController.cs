using CarInsuranceSales.Commands;
using CarInsuranceSales.Core;
using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.DataAccess.Repository;
using CarInsuranceSales.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CarInsuranceSales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsuranceSalesBotController : ControllerBase
{
    private readonly ITelegramBotClient _botClient;
    private readonly IDocumentDataProvider _dataProvider;
    private readonly IPolicyProvider _policyProvider;
    private readonly ChatClient _openAiChatClient;
    private readonly AppOptions _options;

    private readonly IReadOnlyList<IBotCommand> _commands;

    public InsuranceSalesBotController(
        IPolicyProvider policyProvider,
        IDocumentDataProvider dataProvider,
        IClientRepository clientRepository,
        IOptions<AppOptions> options)
    {
        _options = options.Value;
        _botClient = new TelegramBotClient(_options.TelegramBotToken);
        _dataProvider = dataProvider;
        _policyProvider = policyProvider;
        _openAiChatClient = new ChatClient(_options.OpenAiChatModelName, _options.OpenAiApiKey);

        _commands = new List<IBotCommand>
        {
            new WelcomeCommand(_openAiChatClient),
            new ProcessDocumentsCommand(_dataProvider, _openAiChatClient, clientRepository),
            new ConfirmDocumentsDataCommand(_openAiChatClient),
            new ResubmitDocumentsCommand(_openAiChatClient),
            new DisagreePriceCommand(_openAiChatClient),
            new BuyCarInsuranceCommand(_policyProvider, _openAiChatClient),
            new CancelBuyingCarInsuranceCommand(_openAiChatClient)
        };
    }

    [HttpPost]
    public async Task<IActionResult> Post(Update update)
    {
        var cts = new CancellationTokenSource();

        var command = _commands.FirstOrDefault(c => c.ShouldExecute(update));

        if (command != null)
        {
            await command.Execute(_botClient, update, cts.Token);
        }
        else
        {
            ChatCompletion completion = _openAiChatClient.CompleteChat(Prompts.GetUnsuportedCommandPrompt());
           
            await _botClient.SendMessage(update.Message.Chat.Id, completion.Content[0].Text);
        }

        return Ok();
    }
}
