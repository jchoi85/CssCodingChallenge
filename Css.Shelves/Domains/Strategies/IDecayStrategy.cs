using System;

namespace Css.Shelves.Domains.Strategies
{
    public interface IDecayStrategy
    {
        float CalculateValue(float currentValue, float decayRate, float timeElapsed);
    }
}
