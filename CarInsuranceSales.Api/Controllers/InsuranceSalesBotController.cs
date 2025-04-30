using CarInsuranceSales.Commands;
using CarInsuranceSales.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    private readonly IReadOnlyList<IBotCommand> _commands;

    public InsuranceSalesBotController(
        ITelegramBotClient botClient, 
        IDocumentDataProvider dataProvider, 
        IPolicyProvider policyProvider
    )
    {
        _botClient = botClient;
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
