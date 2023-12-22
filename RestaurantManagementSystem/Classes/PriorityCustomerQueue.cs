using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class PriorityCustomerQueue
    {
        private List<Customer> customers = new List<Customer>();

        public void Enqueue(Customer customer)
        {
            // Kuyruğa müşteri ekleme...
            // Örneğin, müşteriyi öncelik durumuna göre sıralama
            customers.Add(customer);
            // Müşterileri önceliğe göre sıralama
            
        }

        public Customer Dequeue()
        {
            // Kuyruk boşsa, null döndür
            if (customers.Count == 0)
            {
                return null;
            }

            // Kuyruktan ilk müşteriyi al ve listeden çıkar
            var customer = customers[0];
            customers.RemoveAt(0);
            return customer;
        }

        public bool HasCustomers()
        {
            return customers.Count > 0;
        }

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

        public void SortByPriority()
        {
            customers = customers.OrderByDescending(c => c.IsPriority).ToList();
        }

        public int Count()
        {
            return customers.Count;
        }
        // Diğer yardımcı metodlar...
    }

}
