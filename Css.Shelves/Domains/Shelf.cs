using Css.Shelves.Domains.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Css.Shelves.Domains
{
    public class Shelf
    {
        public string Type { get; protected set; }
        public int Capacity { get; }

        private readonly IDecayStrategy _strategy;
        private readonly List<Order> _orders;
        
        public Shelf(int capacity, IDecayStrategy strategy)
        {
            Capacity = capacity;
            _orders = new List<Order>();
            _strategy = strategy;
        }

        public void UpdateValueAllOrders(float timeStepSeconds)
        {
            lock(_orders)
            {
                _orders.ForEach(order =>
                {
                    order.Value = _strategy.CalculateValue(order.Value, order.DecayRate, timeStepSeconds);

                    if (order.Value <= 0)
                        RemoveOrder(order.Id);
                });
            }
        }

        public bool AddOrder(Order order)
        {
            lock(_orders)
            {
                if (_orders.Count < Capacity)
                {
                    _orders.Add(order);
                    return true;
                }

                return false;
            }
        }

        public void RemoveOrder(Guid orderId)
        {
            lock(_orders)
            {
                Order order = _orders.Find(x => x.Id == orderId);
                if (order != null)
                    _orders.Remove(order);
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orders;
        }

        public bool ContainsOrder(Guid orderId)
        {
            lock (_orders)
            {
                return _orders.Select(x => x.Id).Contains(orderId);
            }   
        }

        public float GetValue(Guid orderId)
        {
            Order order = _orders.Find(x => x.Id == orderId);

            return order?.Value ?? 0;
        }
    }
}
