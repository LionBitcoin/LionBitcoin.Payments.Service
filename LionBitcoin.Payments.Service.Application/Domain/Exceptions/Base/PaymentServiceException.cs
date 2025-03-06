using System;
using LionBitcoin.Payments.Service.Application.Domain.Enums;

namespace LionBitcoin.Payments.Service.Application.Domain.Exceptions.Base;

public class PaymentServiceException : Exception
{
    public ExceptionType ExceptionType { get; set; }
    public PaymentServiceException(ExceptionType type, string? message = null) : base(message ?? type.ToString())
    {
        ExceptionType = type;
    }
}