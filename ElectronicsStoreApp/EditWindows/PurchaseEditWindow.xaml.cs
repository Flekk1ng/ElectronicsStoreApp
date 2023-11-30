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
    /// Логика взаимодействия для PurchaseEditWindow.xaml
    /// </summary>
    public partial class PurchaseEditWindow : Window
    {
        private string mode;
        private Purchase selectedPurchase;
        private PurchaseRepository purchaseRepo = new PurchaseRepository();
        private TechnicRepository technicRepo = new TechnicRepository();
        private EmployeeRepository employeeRepo = new EmployeeRepository();
        private DiscountCardRepository discountCardRepo = new DiscountCardRepository();
        public PurchaseEditWindow(string mode, Purchase selectedPurchase)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedPurchase = selectedPurchase;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);

            comboBoxTechnic.ItemsSource = technicRepo.GetAllModels();
            comboBoxEmployee.ItemsSource = employeeRepo.GetAll();
            comboBoxDiscountCard.ItemsSource = discountCardRepo.GetAll(); ;
             
            if (mode == "update" && selectedPurchase != null)
            {
                comboBoxTechnic.Text = technicRepo.GetModelById(selectedPurchase.Technic.Id);
                var selectedEmployee = comboBoxEmployee.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedPurchase.Employee.Id);
                comboBoxEmployee.SelectedItem = selectedEmployee;
                var selectedDiscountCard = comboBoxDiscountCard.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedPurchase.DiscountCard.Id);
                comboBoxDiscountCard.SelectedItem = selectedDiscountCard;

                dateTimePickerPurchase.Value = selectedPurchase.DateOfPurchase;
            }
        }

        public PurchaseEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedTechnic = (dynamic)comboBoxTechnic.SelectedItem;
            var selectedEmployee = (dynamic)comboBoxEmployee.SelectedItem;
            var selectedDiscountCard = (dynamic)comboBoxDiscountCard.SelectedItem;
            DateTime? dateOfPurchase = dateTimePickerPurchase.Value;

            if (selectedTechnic == null || selectedEmployee == null || selectedDiscountCard == null ||
                dateOfPurchase == null)
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Technic technic = technicRepo.GetByModel(selectedTechnic);
                Employee employee = employeeRepo.GetById(selectedEmployee.Id);
                DiscountCard discountCard = discountCardRepo.GetById(selectedDiscountCard.Id);
                if (mode == "insert")
                {
                    Purchase newPurchase = new Purchase(technic, employee, discountCard, (DateTime)dateOfPurchase);
                    newPurchase.Id = purchaseRepo.Create(newPurchase);
                    MainWindow.Purchases.Add(newPurchase);
                }

                if (mode == "update" && selectedPurchase != null)
                {
                    selectedPurchase.Technic = technic;
                    selectedPurchase.Employee = employee;
                    selectedPurchase.DiscountCard = discountCard;
                    selectedPurchase.DateOfPurchase = (DateTime)dateOfPurchase;
                    purchaseRepo.UpdateById(selectedPurchase);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxTechnic.Text = string.Empty;
            comboBoxEmployee.Text = string.Empty;
            comboBoxDiscountCard.Text = string.Empty;
            dateTimePickerPurchase.Value = DateTime.Now;
        }
    }
}
