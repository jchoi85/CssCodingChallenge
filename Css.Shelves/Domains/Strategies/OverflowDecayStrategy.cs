namespace Css.Shelves.Domains.Strategies
{
    public class OverflowDecayStrategy : IDecayStrategy
    {
        public float CalculateValue(float currentValue, float decayRate, float timeElapsed)
        {
            return currentValue - (2 * decayRate * timeElapsed);
        }
    }
}
