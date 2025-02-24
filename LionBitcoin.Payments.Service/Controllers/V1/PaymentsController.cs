using System;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Features.Payments.Commands.Deposit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LionBitcoin.Payments.Service.Controllers.V1;

[ApiController]
[Route("/api/v1/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit(DepositCommand depositCommand)
    {
        return Ok(await _mediator.Send(depositCommand));
    }
}