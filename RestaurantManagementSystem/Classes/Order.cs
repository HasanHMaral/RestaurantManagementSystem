using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderedItems { get; set; } // Öğelerin listesi veya açıklaması
        public Customer Customer { get; set; }

        public Order(int orderId, Customer customer, string orderedItems)
        {
            OrderId = orderId;
            Customer = customer;
            OrderedItems = orderedItems;
        }
    }
}
