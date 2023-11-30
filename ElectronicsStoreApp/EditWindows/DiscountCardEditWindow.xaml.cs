using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для DiscountCardEditWindow.xaml
    /// </summary>
    public partial class DiscountCardEditWindow : Window
    {
        private string mode;
        private DiscountCard selectedDiscountCard;
        private DiscountCardRepository discountCardRepo = new DiscountCardRepository();
        public DiscountCardEditWindow(string mode, DiscountCard selectedDiscountCard)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedDiscountCard = selectedDiscountCard;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            if (mode == "update" && selectedDiscountCard != null)
            {
                textBoxFullName.Text = selectedDiscountCard.FullName;
                textBoxEmail.Text = selectedDiscountCard.Email;
            }
        }

        public DiscountCardEditWindow(string mode) : this(mode, null)
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
            string email = textBoxEmail.Text;
            if (string.IsNullOrEmpty(email))
            {
                email = null;
            }
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (mode == "insert")
                {
                    DiscountCard newDiscountCard = new DiscountCard(fullName, email);
                    newDiscountCard.Id = discountCardRepo.Create(newDiscountCard);
                    MainWindow.DiscountCards.Add(newDiscountCard);
                }

                if (mode == "update" && selectedDiscountCard != null)
                {
                    selectedDiscountCard.FullName = fullName;
                    selectedDiscountCard.Email = email;
                    discountCardRepo.UpdateById(selectedDiscountCard);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxFullName.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
        }
    }
}
