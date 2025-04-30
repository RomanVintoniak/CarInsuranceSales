using CarInsuranceSales.Commands;
using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    private readonly AppOptions _options;

    private readonly IReadOnlyList<IBotCommand> _commands;

    public InsuranceSalesBotController(IPolicyProvider policyProvider, IDocumentDataProvider dataProvider, IOptions<AppOptions> options)
    {
        _options = options.Value;
        _botClient = new TelegramBotClient(_options.TelegramBotToken);
        _dataProvider = dataProvider;
        _policyProvider = policyProvider;

        _commands = new List<IBotCommand>
        {
            new WelcomeCommand(),
            new ProcessDocumentsCommand(_dataProvider),
            new ConfirmDocumentsDataCommand(),
            new ResubmitDocumentsCommand(),
            new DisagreePriceCommand(),
            new BuyCarInsuranceCommand(_policyProvider),
            new CancelBuyingCarInsuranceCommand()
        };
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Api Works!" + _options.PolicyDocumentUrl);
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
            await _botClient.SendMessage(update.Message.Chat.Id, "Command is not supported");
        }

        return Ok();
    }
}
