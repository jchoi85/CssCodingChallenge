namespace Css.Shelves.Domains.Strategies
{
    public class NormalDecayStrategy : IDecayStrategy
    {
        public float CalculateValue(float currentValue, float decayRate, float timeElapsed)
        {
            return currentValue - (decayRate * timeElapsed);
        }
    }
}
