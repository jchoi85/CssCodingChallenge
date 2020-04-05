using MediatR;
using System;

namespace Css.Shared.Events
{
    public class OrderCreated : INotification
    {
        public Guid Id { get; }
        public string Name { get; }
        public OrderTemp Temp { get; }
        public float ShelfLifeSeconds { get; }
        public float DecayRate { get; }

        public enum OrderTemp
        {
            Frozen = 1,
            Cold = 2,
            Hot = 3
        }

        public OrderCreated(Guid id, string name, string temp, float shelfLifeSeconds, float decayRate)
        {
            Id = id;
            Name = name;
            Temp = (OrderTemp)Enum.Parse(typeof(OrderTemp), temp, true);
            ShelfLifeSeconds = shelfLifeSeconds;
            DecayRate = decayRate;
        }
    }
}
