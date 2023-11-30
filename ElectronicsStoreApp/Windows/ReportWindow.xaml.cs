
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Models;

namespace ElectronicsStoreApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public ReportWindow()
        {
            InitializeComponent();
            reportViewer1.RefreshReport();
        }

        public void ShowReportTechnic()
        {
            string sqlExpression = "SELECT * FROM ИнформТехника";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dataTable);
            reportViewer1.LocalReport.ReportPath = "../../Reports/ReportTechnic.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }

        public void ShowReportCostByCategory()
        {
            Title = "Категории проданной техники | Окно отчета";
			string sqlExpression = "SELECT * FROM СтоимостьПоКатегориям";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dataTable);
            reportViewer1.LocalReport.ReportPath = "../../Reports/ReportCostByCategory.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }

        public void OpenReportInstallmentPLan()
        {
            Title = "Оплата рассрочки по покупке | Окно отчета";
            stackPanel.Visibility = Visibility.Visible;
            PurchaseRepository purchaseRepo = new PurchaseRepository();
            comboBoxPurchase.ItemsSource = purchaseRepo.GetAll();
        }

        public void ShowReportInstallmentPLan(int id)
        {
            string sqlExpression = "SELECT * FROM ПолучитьОплатыРассрочкиПокупки WHERE idПокупки = @id";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dataTable);
            reportViewer1.LocalReport.ReportPath = "../../Reports/ReportInstallmentPlan.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }

        public void ShowReportCostByYearMonth()
        {
            Title = "Продажи по годам и месяцам | Окно отчета";
			string sqlExpression = "SELECT * FROM ПрожадиПоГодамИМесяцам";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dataTable);
            reportViewer1.LocalReport.ReportPath = "../../Reports/ReportCostByYearMonth.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }

        private void buttonReport_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxPurchase.Text))
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            PurchaseRepository purchaseRepo = new PurchaseRepository();
            var selectedComboBoxItem = (dynamic)comboBoxPurchase.SelectedItem;
            Purchase purchase = purchaseRepo.GetById(selectedComboBoxItem.Id);
            ShowReportInstallmentPLan(purchase.Id);
        }
    }
}
