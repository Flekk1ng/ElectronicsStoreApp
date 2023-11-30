using ElectronicsStoreApp.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Controls;

namespace ElectronicsStoreApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private LogRepository logRepo = new LogRepository();
        private int dataGridRowCount;
        public LogWindow()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            dataGrid1.AutoGenerateColumns = true;
            var logs = logRepo.GetLogs();
            dataGrid1.ItemsSource = logs.DefaultView;
            dataGridRowCount = logs.Rows.Count;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonRestore_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRestoreCount.Text))
            {
                MessageBox.Show("Пустая строка. Введите количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(textBoxRestoreCount.Text, out int count))
            {
                MessageBox.Show("Данные введены некорректно. Введите количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int index = dataGridRowCount - count;
            var id = dataGrid1.Columns[0].GetCellContent(dataGrid1.Items[index]) as TextBlock;
            int lastid = int.Parse(id.Text);
            logRepo.RestorePurchase(lastid);
            var logs = logRepo.GetLogs();
            dataGrid1.ItemsSource = logs.DefaultView;
            dataGridRowCount = logs.Rows.Count;
            MessageBox.Show("Процесс отката данных был успешно завершен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
