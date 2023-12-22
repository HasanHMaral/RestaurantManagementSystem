using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RestaurantManagementSystem
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public bool IsPriority { get; set; }
        public string PriorityStatus // Öncelik durumunu metin olarak döndüren özellik
        {
            get { return IsPriority ? "65 yaş üstü" : "normal"; }
        }

        public Customer(string customerID, bool isPriority)
        {
            CustomerID = customerID;
            IsPriority = isPriority;
        }

        public void EnterRestaurant()
        {
            // Restorana girişle ilgili işlemler burada gerçekleştirilir
            Console.WriteLine($"{CustomerID} restorana girdi.");
        }
    }
}
