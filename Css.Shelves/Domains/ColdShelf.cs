using Css.Shelves.Domains.Strategies;

namespace Css.Shelves.Domains
{
    public class ColdShelf : Shelf
    {
        public ColdShelf(int capacity, IDecayStrategy strategy) : base(capacity, strategy)
        {
            Type = "Cold";
        }
    }
}
