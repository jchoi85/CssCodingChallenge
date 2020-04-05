using Css.Shelves.Domains.Strategies;

namespace Css.Shelves.Domains
{
    public class OverflowShelf : Shelf
    {
        public OverflowShelf(int capacity, IDecayStrategy strategy) : base(capacity, strategy)
        {
            Type = "Overflow";
        }
    }
}
