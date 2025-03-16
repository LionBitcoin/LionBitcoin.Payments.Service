using LionBitcoin.Payments.Service.Application.Domain.Entities.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Entities;

public class BlockExplorerMetadata : BaseEntity<int>
{
    public string Key { get; set; }

    public string Value { get; set; }
}