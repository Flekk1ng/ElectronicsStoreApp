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
    /// Логика взаимодействия для WarrantyServiceEditWindow.xaml
    /// </summary>
    public partial class WarrantyServiceEditWindow : Window
    {
        private string mode;
        private WarrantyService selectedWarrantyService;
        private WarrantyServiceRepository warrantyServiceRepo = new WarrantyServiceRepository();
        private PurchaseRepository purchaseRepo = new PurchaseRepository();
        private ServiceCenterRepository serviceCenterRepo = new ServiceCenterRepository();
        public WarrantyServiceEditWindow(string mode, WarrantyService selectedWarrantyService)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedWarrantyService = selectedWarrantyService;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);


            comboBoxPurchase.ItemsSource = purchaseRepo.GetAll();
            comboBoxServiceCenter.ItemsSource = serviceCenterRepo.GetAll();

            if (mode == "update" && selectedWarrantyService != null)
            {
                var selectedPurchase = comboBoxPurchase.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedWarrantyService.Purchase.Id);
                comboBoxPurchase.SelectedItem = selectedPurchase;
                var selectedServiceCenter = comboBoxServiceCenter.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedWarrantyService.ServiceCenter.Id);
                comboBoxServiceCenter.SelectedItem = selectedServiceCenter;

                textBoxReason.Text = selectedWarrantyService.ReasonForPetition;
                textBoxResult.Text = selectedWarrantyService.Result;
                dateTimePickerPetition.Value = selectedWarrantyService.DateOfPetition;
            }
        }

        public WarrantyServiceEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedComboBoxPurchase = (dynamic)comboBoxPurchase.SelectedItem;
            string purchaseText = comboBoxPurchase.Text;
            var selectedComboBoxServiceCenter = (dynamic)comboBoxServiceCenter.SelectedItem;
            string serviceCenterText = comboBoxServiceCenter.Text;
            string reason = textBoxReason.Text;
            string result = textBoxResult.Text;
            DateTime? dateOfPetition = dateTimePickerPetition.Value;

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            if (string.IsNullOrEmpty(purchaseText) || string.IsNullOrEmpty(serviceCenterText) || string.IsNullOrEmpty(reason)
                || dateOfPetition == null)
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Purchase purchase = purchaseRepo.GetById(selectedComboBoxPurchase.Id);
                ServiceCenter serviceCenter = serviceCenterRepo.GetById(selectedComboBoxServiceCenter.Id);
                if (mode == "insert")
                {
                    WarrantyService newWarrantyService = new WarrantyService(purchase, serviceCenter, reason, (DateTime)dateOfPetition, result);
                    newWarrantyService.Id = warrantyServiceRepo.Create(newWarrantyService);
                    MainWindow.WarrantyServices.Add(newWarrantyService);
                }

                if (mode == "update" && selectedWarrantyService != null)
                {
                    selectedWarrantyService.Purchase = purchase;
                    selectedWarrantyService.ServiceCenter = serviceCenter;
                    selectedWarrantyService.ReasonForPetition = reason;
                    selectedWarrantyService.DateOfPetition = (DateTime)dateOfPetition;
                    selectedWarrantyService.Result = result;
                    warrantyServiceRepo.UpdateById(selectedWarrantyService);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxPurchase.Text = string.Empty;
            comboBoxServiceCenter.Text = string.Empty;
            textBoxReason.Text = string.Empty;
            textBoxResult.Text = string.Empty;
            dateTimePickerPetition.Value = DateTime.Now;
        }
    }
}
