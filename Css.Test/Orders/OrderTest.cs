using Css.Orders.Domains;
using Xunit;

namespace Css.Test.Orders
{
    public class OrderTest
    {
        [Fact]
        public void CompleteAndReset()
        {
            Order order = new Order("test", Order.OrderTemp.Cold, 5.0f, 5.0f);
            Assert.False(order.Completed);
            order.Complete();
            Assert.True(order.Completed);
            order.Reset();
            Assert.False(order.Completed);
        }
    }
}
