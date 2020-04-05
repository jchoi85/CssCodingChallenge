using Css.Shared.Events;
using Css.Shelves.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Shelves.Handlers
{

    public class DriverArrivedHandler : INotificationHandler<DriverArrived>
    {
        private readonly IShelfService _shelfService;
        public DriverArrivedHandler(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        Task INotificationHandler<DriverArrived>.Handle(DriverArrived notification, CancellationToken cancellationToken)
        {
            _shelfService.RemoveOrderFromShelves(notification.OrderId);

            return Task.CompletedTask;
        }
    }
}
