using Css.Shelves.Domains.Strategies;

namespace Css.Shelves.Domains
{
    public class FrozenShelf : Shelf
    {
        public FrozenShelf(int capacity, IDecayStrategy strategy) : base(capacity, strategy)
        {
            Type = "Frozen";
        }
    }
}
