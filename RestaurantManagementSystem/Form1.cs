namespace RestaurantManagementSystem
{
    public partial class MainForm : Form
    {
        private int currentStep = 1;
        private Restaurant restaurant;

        public MainForm()
        {
            InitializeComponent();
            restaurant = new Restaurant();
            restaurant.OnCustomerAssignedToTable = UpdateTableUI;
            restaurant.OnCustomerAddedToDataGridView = UpdateCustomerDataGridView;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            restaurant.GenerateRandomCustomers();
            // PriorityCustomerQueue nesnesini oluştur
            var customerQueue = new PriorityCustomerQueue();
            var customers = restaurant.WaitingQueue.GetAllCustomers();



            // DataGridView sütunlarını ayarla
            SetupDataGridViewColumns();

            // DataGridView'a veri kaynağını bağla
            dgv_beklemeListesi.DataSource = customers;

        }

        private void SetupDataGridViewColumns()
        {
            dgv_beklemeListesi.AutoGenerateColumns = false; // Otomatik sütun oluşturmayı kapat
            dgv_beklemeListesi.ColumnCount = 2; // İki sütun oluştur

            // MüşteriID sütunu
            dgv_beklemeListesi.Columns[0].Name = "MüşteriID";
            dgv_beklemeListesi.Columns[0].DataPropertyName = "CustomerID";

            // Priority sütunu
            dgv_beklemeListesi.Columns[1].Name = "Öncelik";
            dgv_beklemeListesi.Columns[1].DataPropertyName = "PriorityStatus";

        }

        private async void btnIlerle_Click(object sender, EventArgs e)
        {
            await restaurant.RunRestaurantOperationsAsync();
            var waiterButtons = new List<Button> { garson1, garson2, garson3 }; // Butonların isimlerini uygun şekilde ayarlayın

            await restaurant.TakeOrdersAsync(waiterButtons);

            UpdateUI(); // Arayüzü güncelle
        }

        private void UpdateUI()
        {
            dgv_beklemeListesi.DataSource = null;
            dgv_beklemeListesi.DataSource = restaurant.WaitingQueue.GetAllCustomers();
        }

        public void UpdateTableUI(int tableIndex, Customer customer)
        {
            // Örnek: Masa butonlarını güncelle
            Button tableButton = this.Controls.Find($"btnMasa{tableIndex + 1}", true).FirstOrDefault() as Button;
            if (tableButton != null)
            {
                tableButton.Text = $"Müşteri Sayısı: 1";
            }
        }

        public void UpdateCustomerDataGridView(Customer customer, int tableIndex)
        {
            dgv_oturanlarListesi.AutoGenerateColumns = false; // Otomatik sütun oluşturmayı kapat
            dgv_oturanlarListesi.ColumnCount = 3; // Üç sütun oluştur

            // MüşteriID, MasaID ve Müşteri Durumu sütunlarını oluştur
            dgv_oturanlarListesi.Columns[0].Name = "MüşteriID";
            dgv_oturanlarListesi.Columns[0].DataPropertyName = "CustomerID";
            dgv_oturanlarListesi.Columns[1].Name = "MasaID";
            dgv_oturanlarListesi.Columns[2].Name = "Müşteri Durumu";
            // Örnek: DataGridView'a müşteri bilgilerini ekle
            var row = new DataGridViewRow();
            row.CreateCells(dgv_oturanlarListesi); // dgv_customerStatus, DataGridView'in adı
            row.Cells[0].Value = customer.CustomerID;
            row.Cells[1].Value = tableIndex + 1;
            row.Cells[2].Value = "Oturdu";
            dgv_oturanlarListesi.Rows.Add(row);
        }

    }
}