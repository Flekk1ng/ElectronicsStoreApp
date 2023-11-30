using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для ServiceSenterEditWindow.xaml
    /// </summary>
    public partial class ServiceCenterEditWindow : Window
    {
        private string mode;
        private ServiceCenter selectedServiceCenter;
        private ServiceCenterRepository serviceCenterRepo = new ServiceCenterRepository();
        public ServiceCenterEditWindow(string mode, ServiceCenter selectedServiceCenter)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedServiceCenter = selectedServiceCenter;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            if (mode == "update" && selectedServiceCenter != null)
            {
                textBoxName.Text = selectedServiceCenter.Name;
                textBoxAddress.Text = selectedServiceCenter.Address;
                textBoxEmail.Text = selectedServiceCenter.Email;
            }
        }

        public ServiceCenterEditWindow(string mode) : this(mode, null)
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
                    ServiceCenter newServiceCenter = new ServiceCenter(name, address, email);
                    newServiceCenter.Id = serviceCenterRepo.Create(newServiceCenter);
                    MainWindow.ServiceCenters.Add(newServiceCenter);
                }

                if (mode == "update" && selectedServiceCenter != null)
                {
                    selectedServiceCenter.Name = name;
                    selectedServiceCenter.Address = address;
                    selectedServiceCenter.Email = email;
                    serviceCenterRepo.UpdateById(selectedServiceCenter);
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
