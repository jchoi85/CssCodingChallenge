using Css.Main.Serializers;
using Css.Shelves.Domains;
using Css.Shelves.Handlers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Css.Main.Services
{
    public interface IShelvesService
    {
        Task<IEnumerable<SerializedShelf>> GetShelves();
    }

    public class ShelvesService : IShelvesService
    {
        private readonly IMediator _mediator;
        public ShelvesService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<SerializedShelf>> GetShelves()
        {
            List<Shelf> shelves = (await _mediator.Send(new GetShelves())).ToList();
            List<SerializedShelf> serializedShelves = new List<SerializedShelf>();

            shelves.ForEach(shelf =>
            {
                serializedShelves.Add(shelf.Serialize());
            });

            return serializedShelves;
        }
    }
}
