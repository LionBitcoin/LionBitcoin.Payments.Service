using LionBitcoin.Payments.Service.Application.Services.Enums;

namespace LionBitcoin.Payments.Service.Application.Services.Models;

public record GenerateAddressRequest(long AddressIndex, AddressType AddressType);