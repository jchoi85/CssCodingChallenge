using MediatR;
using System.Threading.Tasks;

namespace Css.Shared.Services
{
    public interface IMessagingService
    {
        Task Publish(INotification request);
        Task Send(IRequest request);
    }
    public class MessagingService : IMessagingService
    {
        private readonly IMediator _mediator;

        public MessagingService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Publish(INotification request)
        {
            return _mediator.Publish(request);
        }

        public Task Send(IRequest request)
        {
            return _mediator.Send(request);
        }
    }
}
