using System.Text.Json;
using LionBitcoin.Payments.Service.Application.Domain.Enums;
using LionBitcoin.Payments.Service.Application.Domain.Exceptions.Base;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Infrastructure.Services;

public class BlockExplorerService : IBlockExplorerService
{
    private readonly MempoolSpaceSettings _mempoolSpaceSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<BlockExplorerService> _logger;

    public BlockExplorerService(
        IOptions<MempoolSpaceSettings> mempoolSpaceSettings, 
        IHttpClientFactory httpClientFactory,
        ILogger<BlockExplorerService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _mempoolSpaceSettings = mempoolSpaceSettings.Value;
    }

    public async Task<string[]> GetBlockTransactionsIds(long blockHeight, CancellationToken cancellationToken = default)
    {
        string url = $"/api/block-height/{blockHeight}";

        using HttpClient client = _httpClientFactory.CreateClient(_mempoolSpaceSettings.ClientName);

        string blockHash = await GetBlockHash(url, client, cancellationToken);
        string[] transactionIds = await GetBlockTransactionIds(blockHash, client, cancellationToken);

        return transactionIds;
    }

    private async Task<string[]> GetBlockTransactionIds(string blockHash, HttpClient client, CancellationToken cancellationToken)
    {
        string url = $"api/block/{blockHash}/txids";
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        using HttpResponseMessage response = await client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        string[]? txids = JsonSerializer.Deserialize<string[]>(content);

        if (txids == null)
        {
            _logger.LogError("transaction deserialization returned null. response content: {content}", content);

            throw new PaymentServiceException(ExceptionType.GeneralError);
        }

        return txids;
    }

    private static async Task<string> GetBlockHash(string url, HttpClient client, CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        using HttpResponseMessage response = await client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}