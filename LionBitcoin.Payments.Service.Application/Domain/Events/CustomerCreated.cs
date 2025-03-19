using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Events;

public class CustomerCreated : BaseEvent
{
    public Customer Customer { get; set; }
}