using System;

namespace LionBitcoin.Payments.Service.Application.Services.Abstractions;

public interface ITimeProviderService
{
    DateTime GetUtcNow { get; }
}