using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Features.Orders.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LionBitcoin.Payments.Service.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}