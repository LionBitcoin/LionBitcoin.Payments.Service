using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LionBitcoin.Payments.Service.Controllers.V1;

[ApiController]
[Route("/api/v1/payments")]
public class PaymentsController : ControllerBase
{
    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit()
    {
        return Ok();
    }
}