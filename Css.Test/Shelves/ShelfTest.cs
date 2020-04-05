using Css.Shelves.Domains;
using Css.Shelves.Domains.Strategies;
using System;
using System.Linq;
using Xunit;

namespace Css.Test.Shelves
{
    public class ShelfTest
    {
        [Fact]
        public void AddOrder()
        {
            Order order = new Order(Guid.NewGuid(), "test", Order.OrderTemp.Cold, 5.0f, 5.0f);
            ColdShelf shelf = new ColdShelf(15, new NormalDecayStrategy());

            Assert.Empty(shelf.GetOrders());

            shelf.AddOrder(order);

            Assert.NotEmpty(shelf.GetOrders());
        }

        [Fact]
        public void RemoveOrder()
        {
            Order order = new Order(Guid.NewGuid(), "test", Order.OrderTemp.Cold, 5.0f, 5.0f);
            ColdShelf shelf = new ColdShelf(15, new NormalDecayStrategy());

            shelf.AddOrder(order);

            Assert.NotEmpty(shelf.GetOrders());

            shelf.RemoveOrder(order.Id);

            Assert.Empty(shelf.GetOrders());
        }

        [Fact]
        public void ContainsOrderReturnsTrueIfShelfContainsOrder()
        {
            Order order = new Order(Guid.NewGuid(), "test", Order.OrderTemp.Cold, 5.0f, 5.0f);
            ColdShelf shelf = new ColdShelf(15, new NormalDecayStrategy());

            shelf.AddOrder(order);

            Assert.True(shelf.ContainsOrder(order.Id));
        }

        [Fact]
        public void ContainsOrderReturnsFalseIfShelfDoesNotContainOrder()
        {
            Order order = new Order(Guid.NewGuid(), "test", Order.OrderTemp.Cold, 5.0f, 5.0f);
            ColdShelf shelf = new ColdShelf(15, new NormalDecayStrategy());

            shelf.AddOrder(order);

            Assert.False(shelf.ContainsOrder(Guid.NewGuid()));
        }

        [Fact]
        public void GetValue()
        {
            Order order = new Order(Guid.NewGuid(), "test", Order.OrderTemp.Cold, 5.0f, 5.0f);
            ColdShelf shelf = new ColdShelf(15, new NormalDecayStrategy());

            shelf.AddOrder(order);

            Assert.Equal(shelf.GetValue(order.Id), order.Value);
        }

        [Fact]
        public void UpdateValueAllOrders()
        {
            Order order = new Order(Guid.NewGuid(), "test", Order.OrderTemp.Cold, 5.0f, 1.0f);
            ColdShelf shelf = new ColdShelf(15, new NormalDecayStrategy());
            shelf.AddOrder(order);

            Assert.Single(shelf.GetOrders().ToList());

            int timeStep = 1;

            float newValue = new NormalDecayStrategy().CalculateValue(order.Value, order.DecayRate, timeStep);

            shelf.UpdateValueAllOrders(timeStep);

            Assert.Equal(order.Value, newValue);
        }
    }
}
