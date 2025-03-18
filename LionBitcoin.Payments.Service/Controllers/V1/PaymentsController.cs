using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Features.Payments.Commands.SyncPayments;
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

    [HttpPost]
    public async Task<IActionResult> SyncPayments([FromBody] SyncPaymentsCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}