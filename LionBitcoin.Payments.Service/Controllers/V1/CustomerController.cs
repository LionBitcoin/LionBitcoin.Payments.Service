using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Features.Customers.Commands.RegisterCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LionBitcoin.Payments.Service.Controllers.V1;

[ApiController]
[Route("/api/v1/customer")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}