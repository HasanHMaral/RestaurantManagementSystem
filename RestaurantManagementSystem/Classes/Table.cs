using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Table
    {
        public int TableNumber { get; set; }
        public bool IsOccupied { get; set; }
        public Customer OccupyingCustomer { get; set; }
        public bool OrderTaken { get; set; } // Sipariş alındı mı?
        public bool OrderReadyToServe { get; set; } // Sipariş servis edilmeye hazır mı?
        public bool MealServed { get; set; } // Yemek servis edildi mi?
        public bool MealReadyToCollect { get; set; } // Yemek toplanmaya hazır mı?
        public Table(int tableNumber)
        {
            TableNumber = tableNumber;
            IsOccupied = false;
            OrderReadyToServe = false;
            MealServed = false;
            MealReadyToCollect = false;
        }

        public void Occupy(Customer customer)
        {
            OccupyingCustomer = customer;
            IsOccupied = true;
            MealServed = false; // Masa yeni müşteriyle işgal edildiğinde yemek servis edilmemiş olacak
        }

        // Masa boşaltma işlevi 
        public void Vacate()
        {
            OccupyingCustomer = null;
            IsOccupied = false;
            MealServed = false;
        }

        public void OrderServed()
        {
            // Siparişin servis edildiğini işaretle
            OrderReadyToServe = false;
        }

        public void MealCollected()
        {
            // Yemeğin toplandığını işaretle
            MealReadyToCollect = false;
        }
    }
}
