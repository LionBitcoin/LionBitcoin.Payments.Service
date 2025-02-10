using LionBitcoin.Payments.Service.Application.Clients;
using LionBitcoin.Payments.Service.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Infrastructure;

public class MemPoolSpaceClient : IMemPoolSpaceClient
{
    private readonly MemPoolSpaceSettings _memPoolSpaceSettings;

    public MemPoolSpaceClient(IOptions<MemPoolSpaceSettings> memPoolSpaceSettings)
    {
        _memPoolSpaceSettings = memPoolSpaceSettings.Value;
    }

    
}