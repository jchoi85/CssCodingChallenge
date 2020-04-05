using Css.Shelves.Domains;
using System.Collections.Generic;
using System.Linq;

namespace Css.Main.Serializers
{
    public class SerializedShelf
    {
        public string Type { get; set; }
        public int Capacity { get; set; }
        public List<SerializedShelfOrder> Orders { get; set; }

        public SerializedShelf(string type, int capacity)
        {
            Type = type;
            Capacity = capacity;
            Orders = new List<SerializedShelfOrder>();
        }

        public class SerializedShelfOrder
        {
            public string Name { get; set; }
            public float Value { get; set; }
        }
    }

    public static class SerializedShelfExtensions
    {
        public static SerializedShelf Serialize(this Shelf shelf)
        {
            SerializedShelf serializedShelf = new SerializedShelf(shelf.Type, shelf.Capacity);

            shelf.GetOrders().ToList().ForEach(order =>
            {
                serializedShelf.Orders.Add(new SerializedShelf.SerializedShelfOrder
                {
                    Name = order.Name,
                    Value = order.Value / order.ShelfLifeSeconds
                });
            });

            return serializedShelf;
        }
    }
}
