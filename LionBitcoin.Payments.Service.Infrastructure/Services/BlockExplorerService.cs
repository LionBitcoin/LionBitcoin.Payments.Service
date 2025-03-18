using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Infrastructure.Services;

public class BlockExplorerService : IBlockExplorerService
{
    private readonly MempoolSpaceSettings _mempoolSpaceSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public BlockExplorerService(
        IOptions<MempoolSpaceSettings> mempoolSpaceSettings, 
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _mempoolSpaceSettings = mempoolSpaceSettings.Value;
    }

    public async Task<string> GetBlockHash(long blockHeight, CancellationToken cancellationToken = default)
    {
        string url = $"/api/block-height/{blockHeight}";

        using HttpClient client = _httpClientFactory.CreateClient(_mempoolSpaceSettings.ClientName);
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        using HttpResponseMessage response = await client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}