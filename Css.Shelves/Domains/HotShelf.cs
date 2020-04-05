using Css.Shelves.Domains.Strategies;

namespace Css.Shelves.Domains
{
    public class HotShelf : Shelf
    {
        public HotShelf(int capacity, IDecayStrategy strategy) : base(capacity, strategy)
        {
            Type = "Hot";
        }
    }
}
