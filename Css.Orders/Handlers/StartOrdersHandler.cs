using Css.Orders.Services;
using Css.Shared.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Orders.Handlers
{
    public class StartOrdersHandler : INotificationHandler<StartOrders>
    {
        private readonly IOrderService _orderService;
        public StartOrdersHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        Task INotificationHandler<StartOrders>.Handle(StartOrders notification, CancellationToken cancellationToken)
        {
            _orderService.StartIncomingOrders();

            return Task.CompletedTask;
        }
    }
}
