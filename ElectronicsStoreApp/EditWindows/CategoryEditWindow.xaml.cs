using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using ElectronicsStoreApp.Windows;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStoreApp.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для CategoryEditWindow.xaml
    /// </summary>
    public partial class CategoryEditWindow : Window
    {
        private string mode;
        private Category selectedCategory;
        private CategoryRepository categoryRepo = new CategoryRepository();
        public CategoryEditWindow(string mode, Category selectedCategory)
        {
            InitializeComponent();
            this.mode = mode;
            this.selectedCategory = selectedCategory;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);

            if (mode == "update" && selectedCategory != null)
            {
                textBoxName.Text = selectedCategory.Name;
            }
        }

        public CategoryEditWindow(string mode) : this(mode, null)
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
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Данные введены некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (mode == "insert")
                {
                    Category newCategory = new Category(name);
                    newCategory.Id = categoryRepo.Create(newCategory);
                    MainWindow.Categories.Add(newCategory);
                }

                if (mode == "update" && selectedCategory != null)
                {
                    selectedCategory.Name = name;
                    categoryRepo.UpdateById(selectedCategory);
                }

                Close();
                Owner.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = string.Empty;
        }
    }
}
