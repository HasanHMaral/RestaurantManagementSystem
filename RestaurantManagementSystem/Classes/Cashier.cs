using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Cashier
    {
        public string Name { get; set; }

        public Cashier(string name)
        {
            Name = name;
        }

        public void ProcessPayment(Customer customer)
        {
            // Müşterinin ödeme işlemini simüle et
            Console.WriteLine($"{Name} is processing payment for {customer.CustomerID}.");

            // Burada ödeme işleminin detayları (örneğin, ödeme miktarı) eklenebilir

            // Ödeme tamamlandı olarak işaretle
            // Ödeme işlemi tamamlandığında yapılacak işlemler
        }
                
    }
}
