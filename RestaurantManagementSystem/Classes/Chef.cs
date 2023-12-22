using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Chef
    {
        public int ChefId { get; set; }
        public string Name { get; set; }

        private List<Order> ordersToPrepare;

        public Chef(int chefId, string name)
        {
            ChefId = chefId;
            Name = name;
            ordersToPrepare = new List<Order>();
        }

        public bool CanTakeOrder()
        {
            // Burada, aşçının yeni sipariş alıp alamayacağını kontrol eden mantık
            // Örnek olarak, maksimum sipariş sayısını kontrol etme
            return true; // Geçici olarak her zaman true dönüyor
        }

        public void TakeOrder(Order order)
        {
            ordersToPrepare.Add(order);
            // Siparişi al ve işlemeye başla
            // Burada sipariş işleme mantığı
        }

        public void PrepareOrders()
        {
            foreach (var order in ordersToPrepare)
            {
                // Sipariş hazırlama işlemleri burada gerçekleştirilir
                // Örneğin, belirli bir süre bekletme, siparişi hazır olarak işaretleme vs.
            }

            // Siparişler hazırlandıktan sonra listeyi temizle
            ordersToPrepare.Clear();
        }
        // Yemek pişirme işlevleri...
    }
}
