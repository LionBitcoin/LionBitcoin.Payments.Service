using LionBitcoin.Payments.Service.Application.Domain.Entities;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Application.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public Customer Customer { get; set; }
}