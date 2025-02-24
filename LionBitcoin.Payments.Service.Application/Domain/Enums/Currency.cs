namespace LionBitcoin.Payments.Service.Application.Domain.Enums;

/// <summary>
/// When interacting with Currency amounts, always use specific Currency's smallest unit.
/// For example:
///     in case of Dollar, use Cent,
///     in case of Bitcoin, use Satoshi,
///     in case of Georgian Lari, use Tetri,
/// </summary>
public enum Currency
{
    Dollar = 1, // Smallest unit = cent

    Bitcoin = 2 // Smallest unit = satoshi
}