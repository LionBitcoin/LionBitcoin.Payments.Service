using LionBitcoin.Payments.Service.Application.Services;
using LionBitcoin.Payments.Service.Application.Services.Enums;
using LionBitcoin.Payments.Service.Application.Services.Models;
using LionBitcoin.Payments.Service.Application.Shared;
using LionBitcoin.Payments.Service.Application.Tests.Services.ClassDatas;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace LionBitcoin.Payments.Service.Application.Tests.Services;

public class HdWalletServiceTests
{
    private readonly HdWalletService _hdWalletService;
    private readonly IOptions<PaymentServiceSettings> _paymentServiceSettingsOptions;

    public HdWalletServiceTests()
    {
        _paymentServiceSettingsOptions = Substitute.For<IOptions<PaymentServiceSettings>>();
        _hdWalletService = new HdWalletService(_paymentServiceSettingsOptions);
    }

    [Theory]
    [ClassData(typeof(GenerateAddress_TestsIfAddressGenerationEvaluatesCorrectly))]
    public void GenerateAddress_TestsIfAddressGenerationEvaluatesCorrectly(Network network, long addressIndex, AddressType addressType, string expectedAddress, string xPub)
    {
        // Arrange
        #region Arrange
        _paymentServiceSettingsOptions.Value.Returns(new PaymentServiceSettings()
        {
            AccountExtendedPublicKey = xPub,
            Network = network
        });
        #endregion

        // Act
        GenerateAddressResponse evaluatedAddressResponse = _hdWalletService.GenerateAddress(new GenerateAddressRequest(addressIndex, addressType));

        //Assert
        Assert.Equal(expectedAddress, evaluatedAddressResponse.Address);
    }
}