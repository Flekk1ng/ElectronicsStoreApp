using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        private string mode;
        private Employee selectedEmployee;
        private EmployeeRepository employeeRepo = new EmployeeRepository();
        public EmployeeEditWindow(string mode, Employee selectedEmployee)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedEmployee = selectedEmployee;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            if (mode == "update" && selectedEmployee != null)
            {
                textBoxFullName.Text = selectedEmployee.FullName;
                textBoxAddress.Text = selectedEmployee.Address;
                textBoxEmail.Text = selectedEmployee.Email;
            }
        }

        public EmployeeEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            string fullName = textBoxFullName.Text;
            string address = textBoxAddress.Text;
            string email = textBoxEmail.Text;
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (mode == "insert")
                {
                    Employee newEmployee = new Employee(fullName, address, email);
                    newEmployee.Id = employeeRepo.Create(newEmployee);
                    MainWindow.Employees.Add(newEmployee);
                }

                if (mode == "update" && selectedEmployee != null)
                {
                    selectedEmployee.FullName = fullName;
                    selectedEmployee.Address = address;
                    selectedEmployee.Email = email;
                    employeeRepo.UpdateById(selectedEmployee);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxFullName.Text = string.Empty;
            textBoxAddress.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
        }
    }
}
