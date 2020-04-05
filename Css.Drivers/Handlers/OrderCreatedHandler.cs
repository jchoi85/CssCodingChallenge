using Css.Shared.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Drivers.Handlers
{
    public class OrderCreatedHandler : INotificationHandler<OrderCreated>
    {
        private readonly IMediator _mediator;
        public OrderCreatedHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        Task INotificationHandler<OrderCreated>.Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DispatchDriver(notification.Id));
        }
    }
}
