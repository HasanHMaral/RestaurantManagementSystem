using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Waiter
    {
        private readonly object tableLock = new object();
        public int WaiterId { get; set; }
        public string Name { get; set; }
        public List<Table> AssignedTables { get; set; }

        public Waiter(int waiterId, string name)
        {
            WaiterId = waiterId;
            Name = name;
            AssignedTables = new List<Table>();
        }

        public void TakeOrderFromTable(Table table)
        {
            lock (tableLock)
            {
                // Sipariş alma süresini simüle et
                Thread.Sleep(2000); // 2 saniye beklet

                // Sipariş alma işlemleri...
                // Örnek: table.Order = GenerateOrder();
            }
        }

        public async Task TakeOrderAsync(Table table, Button waiterButton)
        {
            // Sipariş alırken buton metnini güncelle
            UpdateButtonUI(waiterButton, "Sipariş Alınıyor");

            // Sipariş alma sürecini simüle et
            await Task.Delay(2000);  // 2 saniye bekle

            // Sipariş alma işlemi tamamlandı, burada siparişi işleyin
            // ...

            // İşlem tamamlandığında buton metnini geri al
            UpdateButtonUI(waiterButton, "Sipariş Alındı");
        }

        private void UpdateButtonUI(Button button, string text)
        {
            if (button.InvokeRequired)
            {
                button.Invoke(new Action(() => button.Text = text));
            }
            else
            {
                button.Text = text;
            }
        }

        public void CollectMealsFromChefs()
        {
            foreach (var table in AssignedTables)
            {
                if (table.IsOccupied && table.MealReadyToCollect)
                {
                    // Hazır olan yemeği toplama işlemi
                    Console.WriteLine($"Garson {Name}, masa {table.TableNumber} numaradan yemeği topluyor.");

                    // Yemeği toplandı olarak işaretle
                    table.MealCollected();

                    // Burada diğer ilgili işlemler (örneğin, yemeğin sıcaklığı, sunum şekli vs.) eklenebilir
                }
            }
        }

        public void ServeMealsToCustomers()
        {
            foreach (var table in AssignedTables)
            {
                if (table.IsOccupied && table.OrderReadyToServe)
                {
                    // Yemeği masaya servis etme işlemi
                    Console.WriteLine($"Garson {Name}, masa {table.TableNumber} numaraya yemek servisi yapıyor.");

                    // Siparişin servis edildiğini işaretle
                    table.OrderServed();

                    // Burada diğer ilgili işlemler (örneğin, müşterinin tepkisi, yemeğin kalitesi vs.) eklenebilir
                }
            }
        }

        // Sipariş alma ve müşteriye hizmet verme işlevleri...
    }
}
