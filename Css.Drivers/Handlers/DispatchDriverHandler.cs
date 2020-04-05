using Css.Shared.Events;
using Css.Shared.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Drivers.Handlers
{
    public class DispatchDriver : IRequest<Unit> 
    { 
        public Guid OrderId { get; }
        public DispatchDriver(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public class DispatchDriverHandler : IRequestHandler<DispatchDriver, Unit>
    {
        private readonly IMessagingService _messagingService;
        public DispatchDriverHandler(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public async Task<Unit> Handle(DispatchDriver request, CancellationToken cancellationToken)
        {
            await Task.Delay(new Random().Next(2, 11) * 1000);

            await _messagingService.Publish(new DriverArrived(request.OrderId));

            return Unit.Value;
        }
    }
}
