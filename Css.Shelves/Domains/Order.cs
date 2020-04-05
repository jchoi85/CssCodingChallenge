using System;

namespace Css.Shelves.Domains
{
    public class Order
    {
        public Guid Id { get; }
        public string Name { get; }
        public OrderTemp Temp { get; }
        public float ShelfLifeSeconds { get; } 
        public float DecayRate { get; }
        public float Value { get; set; }
        public DateTime AddedAt { get; }

        public Order(Guid id, string name, OrderTemp temp, float shelfLifeSeconds, float decayRate)
        {
            Id = id;
            Name = name;
            Temp = temp;
            ShelfLifeSeconds = shelfLifeSeconds;
            DecayRate = decayRate;
            AddedAt = DateTime.Now;
            Value = shelfLifeSeconds;
        }
        
        public enum OrderTemp
        {
            Frozen = 1,
            Cold = 2,
            Hot = 3
        }
    }
}