using System.Threading;
using System.Threading.Tasks;
using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Domain.Enums;
using LionBitcoin.Payments.Service.Application.Domain.Events;
using LionBitcoin.Payments.Service.Application.Domain.Exceptions.Base;
using LionBitcoin.Payments.Service.Application.Repositories;
using LionBitcoin.Payments.Service.Application.Repositories.Base;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;
using LionBitcoin.Payments.Service.Application.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LionBitcoin.Payments.Service.Application.Features.Payments.Commands.SyncPayments;

public class SyncPaymentsCommandHandler : IRequestHandler<SyncPaymentsCommand, SyncPaymentsResponse>
{
    private readonly IBlockExplorerService _blockExplorerService;
    private readonly IBlockExplorerMetadataRepository _blockExplorerMetadataRepository;
    private readonly ILogger<SyncPaymentsCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PaymentServiceSettings _paymentsServiceSettings;

    public SyncPaymentsCommandHandler(
        IOptions<PaymentServiceSettings> paymentsServiceOptions,
        IBlockExplorerService blockExplorerService,
        IBlockExplorerMetadataRepository blockExplorerMetadataRepository,
        ILogger<SyncPaymentsCommandHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _blockExplorerService = blockExplorerService;
        _blockExplorerMetadataRepository = blockExplorerMetadataRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _paymentsServiceSettings = paymentsServiceOptions.Value;
    }
    public async Task<SyncPaymentsResponse> Handle(SyncPaymentsCommand request, CancellationToken cancellationToken)
    {
        long currentBlockIndexToCheck = await GetCurrentBlockIndexToCheck(cancellationToken);
        string[] transactionIds = await _blockExplorerService.GetBlockTransactionsIds(currentBlockIndexToCheck, cancellationToken);

        using IUnitOfWorkTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        await PublishTransactions(transactionIds, transaction, cancellationToken);

        await _blockExplorerMetadataRepository.UpdateMetadataByKey(
            key: nameof(BlockExplorerMetadataCode.BlockIndexThreshold),
            value: (currentBlockIndexToCheck + 1).ToString(),
            cancellationToken: cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new SyncPaymentsResponse();
    }

    private async Task PublishTransactions(string[] transactionIds, IUnitOfWorkTransaction transaction, CancellationToken cancellationToken)
    {
        foreach (string transactionId in transactionIds)
        {
            await transaction.Publish(new BitcoinTransactionOccuredEvent()
            {
                OriginalProducer = nameof(SyncPaymentsCommandHandler),
                TransactionId = transactionId,
            }, cancellationToken);
        }
    }

    private async Task<long> GetCurrentBlockIndexToCheck(CancellationToken cancellationToken)
    {
        BlockExplorerMetadata? currentBlockIndexMetadata = await _blockExplorerMetadataRepository.GetMetadataByKey(
            nameof(BlockExplorerMetadataCode.BlockIndexThreshold), cancellationToken);

        if (currentBlockIndexMetadata == null)
        {
            _logger.LogError("Block explorer metadata not found. requested key: {key}", nameof(BlockExplorerMetadataCode.BlockIndexThreshold));

            throw new PaymentServiceException(ExceptionType.GeneralError);
        }

        return long.Parse(currentBlockIndexMetadata.Value);
    }
}