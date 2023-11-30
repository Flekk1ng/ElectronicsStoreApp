using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Type = ElectronicsStoreApp.Models.Type;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для TypeEditWindow.xaml
    /// </summary>
    public partial class TypeEditWindow : Window
    {
        private string mode;
        private Type selectedType;
        private TypeRepository typeRepo = new TypeRepository();
        private CategoryRepository categoryRepo = new CategoryRepository();
        public TypeEditWindow(string mode, Type selectedType)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedType = selectedType;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);

            comboBoxCategory.ItemsSource = categoryRepo.GetAll();

            if (mode == "update" && selectedType != null)
            {
                var selectedItem = comboBoxCategory.ItemsSource
                    .Cast<dynamic>()
                    .FirstOrDefault(i => i.Id == selectedType.Category.Id);
                comboBoxCategory.SelectedItem = selectedItem;
                textBoxName.Text = selectedType.Name;
            }
        }

        public TypeEditWindow(string mode) : this(mode, null)
        {
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedComboBoxItem = (dynamic)comboBoxCategory.SelectedItem;
            string categoryText = comboBoxCategory.Text;
            string name = textBoxName.Text;

            if (string.IsNullOrEmpty(categoryText) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Category category = categoryRepo.GetById(selectedComboBoxItem.Id);
                if (mode == "insert")
                {
                    Type newType = new Type(category, name);
                    newType.Id = typeRepo.Create(newType);
                    MainWindow.Types.Add(newType);
                }

                if (mode == "update" && selectedType != null)
                {
                    selectedType.Category = category;
                    selectedType.Name = name;
                    typeRepo.UpdateById(selectedType);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxCategory.Text = string.Empty;
            textBoxName.Text = string.Empty;
        }
    }
}
