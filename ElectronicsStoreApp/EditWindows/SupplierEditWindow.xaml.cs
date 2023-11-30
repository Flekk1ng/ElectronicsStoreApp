using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для SupplierEditWindow.xaml
    /// </summary>
    public partial class SupplierEditWindow : Window
    {
        private string mode;
        private Supplier selectedSupplier;
        private SupplierRepository supplierRepo = new SupplierRepository();
        public SupplierEditWindow(string mode, Supplier selectedSupplier)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedSupplier = selectedSupplier;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            if (mode == "update" && selectedSupplier != null)
            {
                textBoxName.Text = selectedSupplier.Name;
                textBoxAddress.Text = selectedSupplier.Address;
                textBoxEmail.Text = selectedSupplier.Email;
            }
        }

        public SupplierEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            string address = textBoxAddress.Text;
            string email = textBoxEmail.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (mode == "insert")
                {
                    Supplier newSupplier = new Supplier(name, address, email);
                    newSupplier.Id = supplierRepo.Create(newSupplier);
                    MainWindow.Suppliers.Add(newSupplier);
                }

                if (mode == "update" && selectedSupplier != null)
                {
                    selectedSupplier.Name = name;
                    selectedSupplier.Address = address;
                    selectedSupplier.Email = email;
                    supplierRepo.UpdateById(selectedSupplier);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = string.Empty;
            textBoxAddress.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
        }
    }
}
