using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Данные введены некорректно.\nЗаполните полностью поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Authorization.Authorize(textBoxLogin.Text, passwordBox.Password))
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
