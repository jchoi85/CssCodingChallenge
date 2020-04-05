using System;

namespace Css.Orders.Domains
{
    public class Order
    {
        public Guid Id { get; }
        public string Name { get; }
        public OrderTemp Temp { get; }
        public float ShelfLifeSeconds { get; } 
        public float DecayRate { get; }
        public bool Completed { get; protected set; }

        public Order(string name, OrderTemp temp, float shelfLifeSeconds, float decayRate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Temp = temp;
            ShelfLifeSeconds = shelfLifeSeconds;
            DecayRate = decayRate;
            Completed = false;
        }
        
        public enum OrderTemp
        {
            Frozen = 1,
            Cold = 2,
            Hot = 3
        }

        public void Complete()
        {
            Completed = true;
        }

        public void Reset()
        {
            Completed = false;
        }
    }
}