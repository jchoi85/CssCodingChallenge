using MediatR;
using System;

namespace Css.Shared.Events
{
    public class DriverArrived : INotification
    {
        public Guid OrderId { get; }

        public DriverArrived(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
