using Css.Shelves.Domains;
using Css.Shelves.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Shelves.Handlers
{
    public class GetShelves : IRequest<IEnumerable<Shelf>>
    {
    }

    public class GetShelvesHandler : IRequestHandler<GetShelves, IEnumerable<Shelf>>
    {
        private readonly IShelfService _shelfService;
        public GetShelvesHandler(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        public Task<IEnumerable<Shelf>> Handle(GetShelves request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_shelfService.GetShelves());
        }
    }
}
