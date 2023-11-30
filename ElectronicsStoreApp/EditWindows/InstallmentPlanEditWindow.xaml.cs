using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для InstallmentPlanEditWindow.xaml
    /// </summary>
    public partial class InstallmentPlanEditWindow : Window
    {
        private string mode;
        private InstallmentPlan selectedInstallmentPlan;
        private InstallmentPlanRepository installmentPlanRepo = new InstallmentPlanRepository();
        private PurchaseRepository purchaseRepo = new PurchaseRepository();
        public InstallmentPlanEditWindow(string mode, InstallmentPlan selectedInstallmentPlan)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedInstallmentPlan = selectedInstallmentPlan;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);

            comboBoxPurchase.ItemsSource = from t in purchaseRepo.GetAll()
                                           select new
                                           {
                                               idPurchase = t.Id,
                                               Model = t.Technic.Model,
                                               Buyer = t.DiscountCard.FullName,
                                               DataOfPurchase = t.DateOfPurchase
                                           };

            if (mode == "update" && selectedInstallmentPlan != null)
            {
                var selectedItem = comboBoxPurchase.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.idPurchase == selectedInstallmentPlan.Purchase.Id);
                comboBoxPurchase.SelectedItem = selectedItem;
                textBoxPrice.Text = selectedInstallmentPlan.PaymentAmount.ToString();
                dateTimePickerPayment.Value = selectedInstallmentPlan.DateOfPayment;
            }
        }

        public InstallmentPlanEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedComboBoxItem = (dynamic)comboBoxPurchase.SelectedItem;
            string purchaseText = comboBoxPurchase.Text;
            string price = textBoxPrice.Text;
            DateTime? dateOfPayment = dateTimePickerPayment.Value;

            if (string.IsNullOrEmpty(purchaseText) || 
                !decimal.TryParse(price, out decimal decimalPrice) || 
                dateOfPayment == null)
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Purchase purchase = purchaseRepo.GetById(selectedComboBoxItem.idPurchase);
                if (mode == "insert")
                {
                    InstallmentPlan newInstallmentPlan = new InstallmentPlan(purchase, decimalPrice, (DateTime)dateOfPayment);
                    newInstallmentPlan.Id = installmentPlanRepo.Create(newInstallmentPlan);
                    MainWindow.InstallmentPlans.Add(newInstallmentPlan);
                }

                if (mode == "update" && selectedInstallmentPlan != null)
                {
                    selectedInstallmentPlan.Purchase = purchase;
                    selectedInstallmentPlan.PaymentAmount = decimalPrice;
                    selectedInstallmentPlan.DateOfPayment = (DateTime)dateOfPayment;
                    installmentPlanRepo.UpdateById(selectedInstallmentPlan);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxPurchase.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            dateTimePickerPayment.Value = DateTime.Now;
        }
    }
}
