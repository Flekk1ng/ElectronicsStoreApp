using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Type = ElectronicsStoreApp.Models.Type;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для TechnicEditWindow.xaml
    /// </summary>
    public partial class TechnicEditWindow : Window
    {
        private string mode;
        private Technic selectedTechnic;
        private TechnicRepository technicRepo = new TechnicRepository();
        private SupplierRepository supplierRepo = new SupplierRepository();
        private TypeRepository typeRepo = new TypeRepository();
        public TechnicEditWindow(string mode, Technic selectedTechnic)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedTechnic = selectedTechnic;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);

            comboBoxSupplier.ItemsSource = supplierRepo.GetAll();
            comboBoxType.ItemsSource = typeRepo.GetAll();
            datePickerIssue.SelectedDate = DateTime.Now;

            if (mode == "update" && selectedTechnic != null)
            {
                var selectedSupplier = comboBoxSupplier.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedTechnic.Supplier.Id);
                comboBoxSupplier.SelectedItem = selectedSupplier;
                var selectedType = comboBoxType.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedTechnic.Type.Id);
                comboBoxType.SelectedItem = selectedType;

                textBoxCount.Text = selectedTechnic.Count.ToString();
                datePickerIssue.SelectedDate = selectedTechnic.DateOfIssue;
                textBoxGuaranteePeriod.Text = selectedTechnic.GuaranteePeriod.ToString();
                textBoxModel.Text = selectedTechnic.Model;
                textBoxPrice.Text = selectedTechnic.Price.ToString();
                textBoxTypeOfGuarantee.Text = selectedTechnic.TypeOfGuarantee;
            }
        }

        public TechnicEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = (dynamic)comboBoxSupplier.SelectedItem;
            var selectedType = (dynamic)comboBoxType.SelectedItem;
            string countStr = textBoxCount.Text;
            string guaranteePeriodStr = textBoxGuaranteePeriod.Text;
            string model = textBoxModel.Text;
            string typeOfGuarantee = textBoxTypeOfGuarantee.Text;
            string priceStr = textBoxPrice.Text;
            DateTime? dateOfIssue = datePickerIssue.SelectedDate;

            int count = 0;
            int guaranteePeriod = 0;
            decimal price = 0;

            if (selectedSupplier == null || selectedType == null || 
                !int.TryParse(countStr, out count) ||
                !int.TryParse(guaranteePeriodStr, out guaranteePeriod) ||
                string.IsNullOrEmpty(model) ||
                string.IsNullOrEmpty(typeOfGuarantee) ||
                !decimal.TryParse(priceStr, out price) ||
                dateOfIssue == null)
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Supplier supplier = supplierRepo.GetByName(selectedSupplier.Name);
                Type type = typeRepo.GetByName(selectedType.Name);
                if (mode == "insert")
                {
                    Technic newTechnic = new Technic(supplier, type, model, (DateTime)dateOfIssue, price, count, guaranteePeriod, typeOfGuarantee);
                    newTechnic.Id = technicRepo.Create(newTechnic);
                    MainWindow.Technics.Add(newTechnic);
                }

                if (mode == "update" && selectedTechnic != null)
                {
                    selectedTechnic.Supplier = supplier;
                    selectedTechnic.Type = type;
                    selectedTechnic.Count = count;
                    selectedTechnic.GuaranteePeriod = guaranteePeriod;
                    selectedTechnic.Model = model;
                    selectedTechnic.TypeOfGuarantee = typeOfGuarantee;
                    selectedTechnic.Price = price;
                    selectedTechnic.DateOfIssue = (DateTime)dateOfIssue;
                    technicRepo.UpdateById(selectedTechnic);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxSupplier.Text = string.Empty;
            comboBoxType.Text = string.Empty;
            textBoxCount.Text = string.Empty;
            datePickerIssue.SelectedDate = DateTime.Now;
            textBoxGuaranteePeriod.Text = string.Empty;
            textBoxModel.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            textBoxTypeOfGuarantee.Text = string.Empty;
        }
    }
}
