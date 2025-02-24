using System;

namespace LionBitcoin.Payments.Service.Application.Domain.Entities.Base;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }

    public DateTime CreateTimestamp { get; set; }

    public DateTime UpdateTimestamp { get; set; }
}