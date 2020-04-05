using Css.Shelves.Domains;
using Css.Shelves.Domains.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Css.Shelves.Services
{
    public interface IShelfService
    {
        void StartShelfService();
        void StopShelfService();
        void AddOrderToShelves(Order order);
        void RemoveOrderFromShelves(Guid guid);
        IEnumerable<Shelf> GetShelves();
    }
    public class ShelfService : IShelfService
    {
        private readonly Dictionary<string, Shelf> _shelves;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _shelfServiceStarted;

        public ShelfService()
        {
            _shelves = new Dictionary<string, Shelf>
            {
                { Order.OrderTemp.Frozen.ToString(), new FrozenShelf(15, new NormalDecayStrategy()) },
                { Order.OrderTemp.Cold.ToString(), new ColdShelf(15, new NormalDecayStrategy()) },
                { Order.OrderTemp.Hot.ToString(), new HotShelf(15, new NormalDecayStrategy()) },
                { "Overflow", new OverflowShelf(20, new OverflowDecayStrategy()) },
            };

            _shelfServiceStarted = false;
        }

        public void StartShelfService()
        {
            if (!_shelfServiceStarted)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => MonitorShelves(), _cancellationTokenSource.Token);
                _shelfServiceStarted = true;
            }
        }

        public void StopShelfService()
        {
            if (_shelfServiceStarted)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        public void AddOrderToShelves(Order order)
        {
            if (!_shelves[order.Temp.ToString()].AddOrder(order))
            {
                _shelves["Overflow"].AddOrder(order);
            }
        }

        public void RemoveOrderFromShelves(Guid guid)
        {
            _shelves.Values.ToList().ForEach(shelf =>
            {
                if (shelf.ContainsOrder(guid))
                {
                    shelf.RemoveOrder(guid);
                }
            });
        }

        public IEnumerable<Shelf> GetShelves()
        {
            return _shelves.Values;
        }

        private async Task MonitorShelves()
        {
            const float timeStepSeconds = 1;

            while (true)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    _shelfServiceStarted = false;
                    break;
                }

                _shelves.Values.ToList().ForEach(shelf =>
                {
                    shelf.UpdateValueAllOrders(timeStepSeconds);
                });

                _shelves["Overflow"].GetOrders().ToList().ForEach(order =>
                {
                    TransferOrderFromOverflow(order);
                });

                await Task.Delay((int)(timeStepSeconds * 1000));
            }
        }

        private void TransferOrderFromOverflow(Order order)
        {
            if(_shelves[order.Temp.ToString()].AddOrder(order))
            {
                _shelves["Overflow"].RemoveOrder(order.Id);
            }
        }
    }
}
