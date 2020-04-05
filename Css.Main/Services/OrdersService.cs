using Css.Orders.Domains;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Css.Main.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetOrders();
    }

    public class OrdersService : IOrdersService
    {
        private readonly IMediator _mediator;
        public OrdersService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<Order>> GetOrders()
        {
            throw new System.Exception();
            //return _mediator.Send(new RequestOrders());
        }
    }
}
