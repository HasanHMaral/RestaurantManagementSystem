using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Threads
{
    public class AsyncUIController
    {
        private Restaurant restaurant;

        public AsyncUIController(Restaurant restaurant)
        {
            this.restaurant = restaurant;
        }

        public async void OnStartButtonClicked()
        {
            await restaurant.AddCustomersAsync();
            //await restaurant.TakeOrdersAsync();
            await restaurant.PrepareDishesAsync();
            await restaurant.ProcessPaymentsAsync();
        }

        // UI güncellemeleri ve diğer işlemler...
    }

}
