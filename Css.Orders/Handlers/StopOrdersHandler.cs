using Css.Orders.Services;
using Css.Shared.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Orders.Handlers
{
    public class StopOrdersHandler : INotificationHandler<StopOrders>
    {
        private readonly IOrderService _orderService;
        public StopOrdersHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        Task INotificationHandler<StopOrders>.Handle(StopOrders notification, CancellationToken cancellationToken)
        {
            _orderService.StopIncomingOrders();

            return Task.CompletedTask;
        }
    }
}
