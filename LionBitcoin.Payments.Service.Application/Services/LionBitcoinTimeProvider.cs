using System;
using LionBitcoin.Payments.Service.Application.Services.Abstractions;

namespace LionBitcoin.Payments.Service.Application.Services;

public class LionBitcoinTimeProvider : ITimeProviderService
{
    public DateTime GetUtcNow => DateTime.UtcNow;
}