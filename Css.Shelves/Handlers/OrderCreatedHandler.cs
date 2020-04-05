using Css.Shared.Events;
using Css.Shelves.Domains;
using Css.Shelves.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Shelves.Handlers
{

    public class OrderCreatedHandler : INotificationHandler<OrderCreated>
    {
        private readonly IShelfService _shelfService;
        public OrderCreatedHandler(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        Task INotificationHandler<OrderCreated>.Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            _shelfService.AddOrderToShelves(new Order(
                notification.Id,
                notification.Name,
                (Order.OrderTemp)Enum.Parse(typeof(Order.OrderTemp), notification.Temp.ToString(), true),
                notification.ShelfLifeSeconds,
                notification.DecayRate));

            return Task.CompletedTask;
        }
    }
}
