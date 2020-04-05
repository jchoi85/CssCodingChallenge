using Accord.Statistics.Distributions.Univariate;
using Css.Orders.Domains;
using Css.Shared.Events;
using Css.Shared.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Orders.Services
{
    public interface IOrderService
    {
        void StartIncomingOrders();
        void StopIncomingOrders();
    }
    public class OrderService : IOrderService
    {
        private readonly IMessagingService _messagingService;

        private readonly List<Order> _orders;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _orderingStarted;

        public OrderService(IMessagingService messagingService)
        {
            _messagingService = messagingService;

            _orders = ParseOrders(JObject.Parse(File.ReadAllText("orders.json")).Value<JArray>("orders")).ToList();
            _orderingStarted = false;
        }

        public void StartIncomingOrders()
        {
            if(!_orderingStarted)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => GenerateOrders(), _cancellationTokenSource.Token);
                _orderingStarted = true;
            }
        }

        public void StopIncomingOrders()
        {
            if(_orderingStarted)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async Task GenerateOrders()
        {
            while (true)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    _orderingStarted = false;
                    break;
                }

                GetOrders().ToList().ForEach(order =>
                {
                    _messagingService.Publish(new OrderCreated(
                        order.Id,
                        order.Name,
                        order.Temp.ToString(),
                        order.ShelfLifeSeconds,
                        order.DecayRate));
                });

                await Task.Delay(1000);
            }
        }

        private IEnumerable<Order> GetOrders()
        {
            int numOrders = GetNumberOfOrders();

            List<Order> orders = new List<Order>();

            List<Order> ordersToFulfill = _orders.Where(x => !x.Completed).ToList();

            for(int i = 0; i < numOrders; i++)
            {
                if(ordersToFulfill.Count - 1 < i) // if out of orders to make, reset all orders and loop back around
                {
                    ResetAllOrders();
                    ordersToFulfill = _orders.Where(x => !x.Completed).ToList();
                    numOrders -= i;
                    i = 0;
                }
                Order order = ordersToFulfill[i];
                orders.Add(order);
                order.Complete();
            }

            return orders;
        }

        private IEnumerable<Order> ParseOrders(JArray arr)
        {
            List<Order> orders = new List<Order>();

            foreach(JObject obj in arr)
            {
                Order.OrderTemp temp = (Order.OrderTemp)Enum.Parse(typeof(Order.OrderTemp), obj.Value<string>("temp"), true);
                orders.Add(new Order(obj.Value<string>("name"), temp, obj.Value<int>("shelfLife"), obj.Value<float>("decayRate")));
            }

            return orders;
        }

        private int GetNumberOfOrders()
        {
            PoissonDistribution pd = new PoissonDistribution(2.7);

            Dictionary<int, int> map = new Dictionary<int, int>();

            for(int i = 0; i < 1000; i++)
            {
                int num = pd.Generate();

                if (!map.ContainsKey(num)) map[num] = 0;
                map[num]++;
            }

            return pd.Generate();
        }

        private void ResetAllOrders()
        {
            _orders.ForEach(x => x.Reset());
        }
    }
}
