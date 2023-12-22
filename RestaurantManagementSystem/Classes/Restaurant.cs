using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantManagementSystem
{
    public class Restaurant
    {
        public Action<int, Customer> OnCustomerAssignedToTable;
        public Action<Customer, int> OnCustomerAddedToDataGridView;

        public List<Customer> Customers { get; private set; }
        public List<Table> Tables { get; private set; }
        public List<Waiter> Waiters { get; private set; }
        public List<Chef> Chefs { get; private set; }
        public Cashier Cashier { get; private set; }
        public PriorityCustomerQueue WaitingQueue { get; private set; }

        private List<Order> collectedOrders = new List<Order>();

        public Restaurant()
        {
            // Restoran bileşenlerini başlatma
            Customers = InitializeCustomers(50);
            Tables = new List<Table>();
            Waiters = new List<Waiter>();
            Chefs = new List<Chef>();
            Cashier = new Cashier("Kasa Görevlisi");
            WaitingQueue = new PriorityCustomerQueue();

            InitializeComponents();
        }

        private List<Customer> InitializeCustomers(int count)
        {
            var customers = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                customers.Add(new Customer($"Müşteri {i + 1}",false));
            }
            return customers;
        }

        private void InitializeComponents()
        {
            // Örnek bileşenlerin eklenmesi
            // Örneğin, masalar, garsonlar ve aşçılar eklenebilir
            for (int i = 0; i < 6; i++) // 6 masa varsayalım
            {
                Tables.Add(new Table(i + 1));
            }

            for (int i = 0; i < 3; i++) // 3 garson varsayalım
            {
                Waiters.Add(new Waiter(i + 1, $"Garson {i + 1}"));
            }

            for (int i = 0; i < 2; i++) // 2 aşçı varsayalım
            {
                Chefs.Add(new Chef(i + 1, $"Aşçı {i + 1}"));
            }
        }

        private bool DeterminePriority(int customerIndex)
        {
            // Öncelik belirleme mantığı
            // Örneğin, rastgele bazı müşterilere öncelik vermek:
            return new Random().Next(0, 2) == 1; // %50 şansla öncelikli
        }

        public void GenerateRandomCustomers()
        {
            var random = new Random();
            int customerCount = random.Next(5, 11); // 5 ile 10 arasında rastgele sayıda müşteri

            HashSet<int> selectedIndices = new HashSet<int>(); // Seçilen müşterilerin indekslerini saklar

            while (selectedIndices.Count < customerCount)
            {
                int randomIndex = random.Next(Customers.Count);
                if (selectedIndices.Add(randomIndex)) // Eğer bu indeks daha önce seçilmediyse
                {
                    var customer = Customers[randomIndex];
                    bool isPriority = random.Next(2) == 1; // %50 şansla öncelikli
                    customer.IsPriority = isPriority;
                    WaitingQueue.Enqueue(customer);
                }
            }

            // Müşterileri önceliğe göre sırala
            WaitingQueue.SortByPriority();
        }

        public async Task RunRestaurantOperationsAsync()
        {
            // 1. Müşterileri masalara yerleştir
            AssignCustomersToTables();

            // 2. Garsonların sipariş alması
           // await TakeOrdersAsync();

            // 3. Garsonların siparişi aşçıya iletmeleri
            PassOrdersToChefs();

            // 4. Aşçıların siparişleri hazırlaması
            PrepareMeals();

            // 5. Hazır olan siparişleri garsonların alması
            CollectMeals();

            // 6. Garsonların siparişleri servis etmesi
            ServeMeals();

            // 7. Müşterilerin yemek yemesi
            // Bu adımı müşteri thread'leri içinde simüle edebilirsiniz

            // 8. Müşterilerin hesabı ödemesi
            ProcessPayments();

            // 9. Müşterilerin restorandan ayrılması
            DepartCustomers();
        }


        public void AssignCustomersToTables()
        {
            int assignedCustomerCount = 0;

            foreach (var table in Tables)
            {
                if (!table.IsOccupied && WaitingQueue.HasCustomers() && assignedCustomerCount < 6)
                {
                    Customer customer = WaitingQueue.Dequeue();
                    table.Occupy(customer);

                    OnCustomerAssignedToTable?.Invoke(table.TableNumber - 1, customer);
                    OnCustomerAddedToDataGridView?.Invoke(customer, table.TableNumber - 1);

                    assignedCustomerCount++;
                }
            }
        }

        public void TakeOrders()
        {
            foreach (var waiter in Waiters)
            {
                foreach (var table in Tables)
                {
                    if (table.IsOccupied && !table.OrderTaken)
                    {
                        waiter.TakeOrderFromTable(table);
                        table.OrderTaken = true; // Sipariş alındı olarak işaretle
                    }
                }
            }
        }

        public void PassOrdersToChefs()
        {
            foreach (var order in collectedOrders)
            {
                AssignOrderToChef(order);
            }
        }

        private void AssignOrderToChef(Order order)
        {
            // Bu metod, siparişi uygun bir aşçıya atar
            // Örneğin, en az iş yüküne sahip olan aşçıyı bulma
            var availableChef = Chefs.FirstOrDefault(chef => chef.CanTakeOrder());
            if (availableChef != null)
            {
                availableChef.TakeOrder(order);
            }
        }

        public void PrepareMeals()
        {
            foreach (var chef in Chefs)
            {
                chef.PrepareOrders(); // Her aşçının siparişleri hazırlaması
            }
        }

        public void CollectMeals()
        {
            foreach (var waiter in Waiters)
            {
                // Hazır olan yemekleri topla ve müşterilere servis etmek üzere hazırlan
                waiter.CollectMealsFromChefs();
            }
        }

        public void ServeMeals()
        {
            foreach (var waiter in Waiters)
            {
                // Hazırlanan yemekleri müşterilere servis et
                waiter.ServeMealsToCustomers();
            }
        }

        public void ProcessPayments()
        {
            foreach (var table in Tables)
            {
                if (table.IsOccupied && table.MealServed)
                {
                    // Müşterinin ödeme yapması
                    Cashier.ProcessPayment(table.OccupyingCustomer);
                    table.Vacate(); // Masa boşaltma
                }
            }
        }

        public void DepartCustomers()
        {
            // Bu adım genellikle müşterilerin ödeme yapmasını takip eder ve 
            // ödeme işlemi tamamlandıktan sonra müşterilerin ayrılmasını simüle eder.
            // Ödeme işlemi ve masa boşaltma işlemleri önceki adımlarda gerçekleştirilmiştir.
        }
        public void CreateCustomerThread(Customer customer)
        {
            Thread customerThread = new Thread(() => CustomerActivities(customer));
            customerThread.Start();
        }

        private void CustomerActivities(Customer customer)
        {
            // Müşteri aktivitelerini simüle et
            // Örnek: Masaya oturma, sipariş verme, yemek yeme
            // Bu aktivitelerin zamanlamasını ve sıralamasını burada kontrol edebilirsiniz.
        }

        public async Task AddCustomersAsync()
        {
            foreach (var customer in this.Customers)
            {
                // Asenkron müşteri ekleme işlemi
                await Task.Run(() => customer.EnterRestaurant());
            }
        }

        public async Task TakeOrdersAsync(List<Button> waiterButtons)
        {
            var orderTasks = new List<Task>();
            for (int i = 0; i < Waiters.Count; i++)
            {
                var waiter = Waiters[i];
                var waiterButton = waiterButtons[i];

                foreach (var table in Tables.Where(t => t.IsOccupied && !t.OrderTaken))
                {
                    var orderTask = waiter.TakeOrderAsync(table, waiterButton);
                    orderTasks.Add(orderTask);
                }
            }

            await Task.WhenAll(orderTasks);
        }

        public async Task PrepareDishesAsync()
        {
            // Asenkron yemek hazırlama işlemi
        }

        public async Task ProcessPaymentsAsync()
        {
            // Asenkron ödeme işleme işlemi
        }

        // Diğer asenkron metodlar...

        // Restoranla ilgili diğer işlevsel metodlar
    }
}
