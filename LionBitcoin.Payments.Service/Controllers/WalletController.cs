using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Features.Wallet.GenerateHdWallet;
using LionBitcoin.Payments.Service.Application.Shared.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Controllers;

[ApiController]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly PaymentsProcessorSettings _paymentProcessorSettings;

    public WalletController(IOptions<PaymentsProcessorSettings> paymentProcessorOptions, IMediator mediator)
    {
        _mediator = mediator;
        _paymentProcessorSettings = paymentProcessorOptions.Value;
    }

    [HttpGet("generate")]
    public async Task<IActionResult> GenerateHdWallet([FromQuery] GenerateHdWalletQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
}