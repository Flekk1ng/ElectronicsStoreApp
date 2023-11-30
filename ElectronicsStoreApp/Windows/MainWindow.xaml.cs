using ElectronicsStoreApp.EditWindows;
using ElectronicsStoreApp.Models;
using ElectronicsStoreApp.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Binding = System.Windows.Data.Binding;
using DataGridCell = System.Windows.Controls.DataGridCell;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using MessageBox = System.Windows.MessageBox;
using Type = ElectronicsStoreApp.Models.Type;

namespace ElectronicsStoreApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Category> Categories { get; set; }
        public static ObservableCollection<DiscountCard> DiscountCards { get; set; }
        public static ObservableCollection<Employee> Employees { get; set; }
        public static ObservableCollection<InstallmentPlan> InstallmentPlans { get; set; }
        public static ObservableCollection<Purchase> Purchases { get; set; }
        public static ObservableCollection<ServiceCenter> ServiceCenters { get; set; }
        public static ObservableCollection<Supplier> Suppliers { get; set; }
        public static ObservableCollection<Technic> Technics { get; set; }
        public static ObservableCollection<Type> Types { get; set; }
        public static ObservableCollection<WarrantyService> WarrantyServices { get; set; }
        private string table;

        public MainWindow()
        {
            InitializeComponent();
            ShowTablePurchase();
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            SetButtonsVisible();

            DiscountCardRepository discountCardRepo = new DiscountCardRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();

            comboBoxDiscountCard.ItemsSource = discountCardRepo.GetAll();
            comboBoxEmployee.ItemsSource = employeeRepo.GetAll();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void SetFocusDataGrid()
        {
            if (dataGrid1.Items.Count > 0)
            {
				dataGrid1.Focus();
				dataGrid1.SelectedIndex = 0;
				dataGrid1.CurrentCell = new DataGridCellInfo(dataGrid1.Items[0], dataGrid1.Columns[0]);
				dataGrid1.BeginEdit();
			}
		}

        private void MenuItem_ShowTableCategory(object sender, RoutedEventArgs e)
        {
            ShowTableCategory();
            SetFocusDataGrid();
		}

        private void MenuItem_ShowTableType(object sender, RoutedEventArgs e)
        {
            ShowTableType();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableSupplier(object sender, RoutedEventArgs e)
        {
            ShowTableSupplier();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableServiceCenter(object sender, RoutedEventArgs e)
        {
            ShowTableServiceCenter();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableEmployee(object sender, RoutedEventArgs e)
        {
            ShowTableEmployee();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableTechnic(object sender, RoutedEventArgs e)
        {
            ShowTableTechnic();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableDiscountCard(object sender, RoutedEventArgs e)
        {
            ShowTableDiscountCard();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTablePurchase(object sender, RoutedEventArgs e)
        {
            ShowTablePurchase();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableWarrantyService(object sender, RoutedEventArgs e)
        {
            ShowTableWarrantyService();
			SetFocusDataGrid();
		}

        private void MenuItem_ShowTableInstallmentPlan(object sender, RoutedEventArgs e)
        {
            ShowTableInstallmentPlan();
			SetFocusDataGrid();
		}

        private void ShowTableCategory()
        {
            table = "Category";

            textTableNameRun2.Text = "Категория техники";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Название";
            column1.Binding = new Binding("Name");
            dataGrid1.Columns.Add(column1);

            CategoryRepository categoryRepo = new CategoryRepository();
            Categories = new ObservableCollection<Category>(categoryRepo.GetAll());
            dataGrid1.ItemsSource = Categories;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableDiscountCard()
        {
            table = "DiscountCard";

            textTableNameRun2.Text = "Дисконтная карта";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "ФИО";
            column1.Binding = new Binding("FullName");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Электронная почта";
            column2.Binding = new Binding("Email") { TargetNullValue = "Эл. почта не задана" };
            column2.CellStyle = new Style(typeof(DataGridCell));
            var trigger = new DataTrigger { Binding = new Binding("Email"), Value = null };
            trigger.Setters.Add(new Setter(TextBlock.FontStyleProperty, FontStyles.Italic));
            trigger.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.Gray));
            column2.CellStyle.Triggers.Add(trigger);
            dataGrid1.Columns.Add(column2);

            DiscountCardRepository discountCardRepo = new DiscountCardRepository();
            DiscountCards = new ObservableCollection<DiscountCard>(discountCardRepo.GetAll());
            dataGrid1.ItemsSource = DiscountCards;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableEmployee()
        {
            table = "Employee";

            textTableNameRun2.Text = "Сотрудник";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "ФИО";
            column1.Binding = new Binding("FullName");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Адрес";
            column2.Binding = new Binding("Address");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "Электронная почта";
            column3.Binding = new Binding("Email");
            dataGrid1.Columns.Add(column3);

            EmployeeRepository employeeRepo = new EmployeeRepository();
            Employees = new ObservableCollection<Employee>(employeeRepo.GetAll());
            dataGrid1.ItemsSource = Employees;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableInstallmentPlan()
        {
            table = "InstallmentPlan";

            textTableNameRun2.Text = "Рассрочка";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Купленная техника";
            column1.Binding = new Binding("Purchase.Technic.Model");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "ФИО покупателя";
            column2.Binding = new Binding("Purchase.DiscountCard.FullName");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "Сумма оплаты";
            column3.Binding = new Binding("PaymentAmount");
            dataGrid1.Columns.Add(column3);

            var column4 = new DataGridTextColumn();
            column4.Header = "Дата оплаты";
            column4.Binding = new Binding("DateOfPayment");
            dataGrid1.Columns.Add(column4);

            InstallmentPlanRepository installmentPlanRepo = new InstallmentPlanRepository();
            InstallmentPlans = new ObservableCollection<InstallmentPlan>(installmentPlanRepo.GetAll());
            dataGrid1.ItemsSource = InstallmentPlans;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTablePurchase()
        {
            table = "Purchase";

            textTableNameRun2.Text = "Покупка";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Модель техники";
            column1.Binding = new Binding("Technic.Model");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "ФИО сотрудника";
            column2.Binding = new Binding("Employee.FullName");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "ФИО покупателя";
            column3.Binding = new Binding("DiscountCard.FullName");
            dataGrid1.Columns.Add(column3);

            var column4 = new DataGridTextColumn();
            column4.Header = "Дата и время покупки";
            column4.Binding = new Binding("DateOfPurchase");
            dataGrid1.Columns.Add(column4);

            PurchaseRepository purchaseRepo = new PurchaseRepository();
            Purchases = new ObservableCollection<Purchase>(purchaseRepo.GetAll());
            dataGrid1.ItemsSource = Purchases;

            groupBoxPurchase.Visibility = Visibility.Visible;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableServiceCenter()
        {
            table = "ServiceCenter";

            textTableNameRun2.Text = "Сервисный центр";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Название";
            column1.Binding = new Binding("Name");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Адрес";
            column2.Binding = new Binding("Address");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "Электронная почта";
            column3.Binding = new Binding("Email");
            dataGrid1.Columns.Add(column3);

            ServiceCenterRepository serviceCenterRepo = new ServiceCenterRepository();
            ServiceCenters = new ObservableCollection<ServiceCenter>(serviceCenterRepo.GetAll());
            dataGrid1.ItemsSource = ServiceCenters;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableTechnic()
        {
            table = "Technic";

            textTableNameRun2.Text = "Техника";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Поставщик";
            column1.Binding = new Binding("Supplier.Name");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Вид";
            column2.Binding = new Binding("Type.Name");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "Модель";
            column3.Binding = new Binding("Model");
            dataGrid1.Columns.Add(column3);

            var column4 = new DataGridTextColumn();
            column4.Header = "Дата выпуска";
            column4.Binding = new Binding("DateOfIssue");
            dataGrid1.Columns.Add(column4);

            var column5 = new DataGridTextColumn();
            column5.Header = "Цена";
            column5.Binding = new Binding("Price");
            dataGrid1.Columns.Add(column5);

            var column6 = new DataGridTextColumn();
            column6.Header = "Количество";
            column6.Binding = new Binding("Count");
            dataGrid1.Columns.Add(column6);

            var column7 = new DataGridTextColumn();
            column7.Header = "Срок гарантии (мес.)";
            column7.Binding = new Binding("GuaranteePeriod");
            dataGrid1.Columns.Add(column7);

            var column8 = new DataGridTextColumn();
            column8.Header = "Тип гарантии";
            column8.Binding = new Binding("TypeOfGuarantee");
            dataGrid1.Columns.Add(column8);

            TechnicRepository technicRepo = new TechnicRepository();
            Technics = new ObservableCollection<Technic>(technicRepo.GetAll());
            dataGrid1.ItemsSource = Technics;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableType()
        {
            table = "Type";

            textTableNameRun2.Text = "Вид техники";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Название";
            column1.Binding = new Binding("Name");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Категория";
            column2.Binding = new Binding("Category.Name");
            dataGrid1.Columns.Add(column2);

            TypeRepository typeRepo = new TypeRepository();
            Types = new ObservableCollection<Type>(typeRepo.GetAll());
            dataGrid1.ItemsSource = Types;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableSupplier()
        {
            table = "Supplier";

            textTableNameRun2.Text = "Поставщик";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Название";
            column1.Binding = new Binding("Name");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Адрес";
            column2.Binding = new Binding("Address");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "Электронная почта";
            column3.Binding = new Binding("Email");
            dataGrid1.Columns.Add(column3);

            SupplierRepository supplierRepo = new SupplierRepository();
            Suppliers = new ObservableCollection<Supplier>(supplierRepo.GetAll());
            dataGrid1.ItemsSource = Suppliers;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Collapsed;
            SetButtonsVisible();
        }

        private void ShowTableWarrantyService()
        {
            table = "WarrantyService";

            textTableNameRun2.Text = "Гарантийное обслуживание";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = false;

            var column1 = new DataGridTextColumn();
            column1.Header = "Модель купленной техники";
            column1.Binding = new Binding("Purchase.Technic.Model");
            dataGrid1.Columns.Add(column1);

            var column2 = new DataGridTextColumn();
            column2.Header = "Название сервисного центра";
            column2.Binding = new Binding("ServiceCenter.Name");
            dataGrid1.Columns.Add(column2);

            var column3 = new DataGridTextColumn();
            column3.Header = "Причина обращения";
            column3.Binding = new Binding("ReasonForPetition");
            dataGrid1.Columns.Add(column3);

            var column4 = new DataGridTextColumn();
            column4.Header = "Дата обращения";
            column4.Binding = new Binding("DateOfPetition");
            dataGrid1.Columns.Add(column4);

            var column5 = new DataGridTextColumn();
            column5.Header = "Результат";
            column5.Binding = new Binding("Result") { TargetNullValue = "Результата обслуживания пока что нет" };
            column5.CellStyle = new Style(typeof(DataGridCell));
            var trigger = new DataTrigger { Binding = new Binding("Result"), Value = null };
            trigger.Setters.Add(new Setter(TextBlock.FontStyleProperty, FontStyles.Italic));
            trigger.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.Gray));
            column5.CellStyle.Triggers.Add(trigger);
            dataGrid1.Columns.Add(column5);

            WarrantyServiceRepository warrantyServiceRepo = new WarrantyServiceRepository();
            WarrantyServices = new ObservableCollection<WarrantyService>(warrantyServiceRepo.GetAll());
            dataGrid1.ItemsSource = WarrantyServices;

            groupBoxPurchase.Visibility = Visibility.Collapsed;
            groupBoxWarrantyService.Visibility = Visibility.Visible;
            SetButtonsVisible();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Window window = null;
            switch (table)
            {
                case "Category":
                    window = new CategoryEditWindow("insert");
                    break;
                case "DiscountCard":
                    window = new DiscountCardEditWindow("insert");
                    break;
                case "Employee":
                    window = new EmployeeEditWindow("insert");
                    break;
                case "InstallmentPlan":
                    window = new InstallmentPlanEditWindow("insert");
                    break;
                case "Purchase":
                    window = new PurchaseEditWindow("insert");
                    break;
                case "ServiceCenter":
                    window = new ServiceCenterEditWindow("insert");
                    break;
                case "Supplier":
                    window = new SupplierEditWindow("insert");
                    break;
                case "Technic":
                    window = new TechnicEditWindow("insert");
                    break;
                case "Type":
                    window = new TypeEditWindow("insert");
                    break;
                case "WarrantyService":
                    window = new WarrantyServiceEditWindow("insert");
                    break;
            }
            if (window != null)
            {
                window.Owner = this;
                window.ShowDialog();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null) 
            {
                MessageBox.Show("Не выбрана строка для изменения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Window window = null;
            var selectedItem = dataGrid1.SelectedItem;
            switch (table)
            {
                case "Category":
                    window = new CategoryEditWindow("update", (Category)selectedItem);
                    break;
                case "DiscountCard":
                    window = new DiscountCardEditWindow("update", (DiscountCard)selectedItem);
                    break;
                case "Employee":
                    window = new EmployeeEditWindow("update", (Employee)selectedItem);
                    break;
                case "InstallmentPlan":
                    window = new InstallmentPlanEditWindow("update", (InstallmentPlan)selectedItem);
                    break;
                case "Purchase":
                    window = new PurchaseEditWindow("update", (Purchase)selectedItem);
                    break;
                case "ServiceCenter":
                    window = new ServiceCenterEditWindow("update", (ServiceCenter)selectedItem);
                    break;
                case "Supplier":
                    window = new SupplierEditWindow("update", (Supplier)selectedItem);
                    break;
                case "Technic":
                    window = new TechnicEditWindow("update", (Technic)selectedItem);
                    break;
                case "Type":
                    window = new TypeEditWindow("update", (Type)selectedItem);
                    break;
                case "WarrantyService":
                    window = new WarrantyServiceEditWindow("update", (WarrantyService)selectedItem);
                    break;
            }
            if (window != null)
            {
                window.Owner = this;
                window.ShowDialog();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана строка для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                switch (table)
                {
                    case "Category":
                        CategoryRepository categoryRepo = new CategoryRepository();
                        var selectedCategory = (Category)dataGrid1.SelectedItem;
                        categoryRepo.Delete(selectedCategory.Id);
                        Categories.Remove(selectedCategory);
                        break;
                    case "DiscountCard":
                        DiscountCardRepository discountCardRepo = new DiscountCardRepository();
                        var selectedDiscountCard = (DiscountCard)dataGrid1.SelectedItem;
                        discountCardRepo.Delete(selectedDiscountCard.Id);
                        DiscountCards.Remove(selectedDiscountCard);
                        break;
                    case "Employee":
                        EmployeeRepository employeeRepo = new EmployeeRepository();
                        var selectedEmployee = (Employee)dataGrid1.SelectedItem;
                        employeeRepo.Delete(selectedEmployee.Id);
                        Employees.Remove(selectedEmployee);
                        break;
                    case "InstallmentPlan":
                        InstallmentPlanRepository installmentPlanRepo = new InstallmentPlanRepository();
                        var selectedInstallmentPlan = (InstallmentPlan)dataGrid1.SelectedItem;
                        installmentPlanRepo.Delete(selectedInstallmentPlan.Id);
                        InstallmentPlans.Remove(selectedInstallmentPlan);
                        break;
                    case "Purchase":
                        PurchaseRepository PurchaseRepo = new PurchaseRepository();
                        var selectedPurchase = (Purchase)dataGrid1.SelectedItem;
                        PurchaseRepo.Delete(selectedPurchase.Id);
                        Purchases.Remove(selectedPurchase);
                        break;
                    case "ServiceCenter":
                        ServiceCenterRepository serviceCenterRepo = new ServiceCenterRepository();
                        var selectedServiceCenter = (ServiceCenter)dataGrid1.SelectedItem;
                        serviceCenterRepo.Delete(selectedServiceCenter.Id);
                        ServiceCenters.Remove(selectedServiceCenter);
                        break;
                    case "Supplier":
                        SupplierRepository supplierRepo = new SupplierRepository();
                        var selectedSupplier = (Supplier)dataGrid1.SelectedItem;
                        supplierRepo.Delete(selectedSupplier.Id);
                        Suppliers.Remove(selectedSupplier);
                        break;
                    case "Technic":
                        TechnicRepository technicRepo = new TechnicRepository();
                        var selectedTechnic = (Technic)dataGrid1.SelectedItem;
                        technicRepo.Delete(selectedTechnic.Id);
                        Technics.Remove(selectedTechnic);
                        break;
                    case "Type":
                        TypeRepository typeRepo = new TypeRepository();
                        var selectedType = (Type)dataGrid1.SelectedItem;
                        typeRepo.Delete(selectedType.Id);
                        Types.Remove(selectedType);
                        break;
                    case "WarrantyService":
                        WarrantyServiceRepository warrantyServiceRepo = new WarrantyServiceRepository();
                        var selectedWarrantyService = (WarrantyService)dataGrid1.SelectedItem;
                        warrantyServiceRepo.Delete(selectedWarrantyService.Id);
                        WarrantyServices.Remove(selectedWarrantyService);
                        break;
                }
            }
        }

        private void MenuItem_ShowReportTechnic(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow();
            reportWindow.ShowReportTechnic();
            reportWindow.ShowDialog();
        }

        private void MenuItem_ShowReportCostByCategory(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow();
            reportWindow.ShowReportCostByCategory();
            reportWindow.ShowDialog();
        }

        private void MenuItem_ShowReportInstallmentPLan(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow();
            reportWindow.OpenReportInstallmentPLan();
            reportWindow.ShowDialog();
        }

        private void MenuItem_ShowReportCostByYearMonth(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow();
            reportWindow.ShowReportCostByYearMonth();
            reportWindow.ShowDialog();
        }

        private void MenuItem_ShowLogWindow(object sender, RoutedEventArgs e)
        {
            var logWindow = new LogWindow();
            logWindow.ShowDialog();
            if (table == "Purchase")
            {
                ShowTablePurchase();
            }
        }

        private void ButtonSort_Click(object sender, RoutedEventArgs e)
        {
            WarrantyServiceRepository warrantyServiceRepo = new WarrantyServiceRepository();
            if (radioButtonDate.IsChecked == true)
            {
                var sortWarrantyServices = warrantyServiceRepo.GetAllSortDate();
                dataGrid1.ItemsSource = sortWarrantyServices;
                return;
            }
            if (radioButtonResult.IsChecked == true)
            {
                var sortWarrantyServices = warrantyServiceRepo.GetAllSortResult();
                dataGrid1.ItemsSource = sortWarrantyServices;
                return;
            }
            MessageBox.Show("Выберите поле для сортировки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.SelectedItem = null;
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new Setter(DataGridRow.BackgroundProperty, null));
            dataGrid1.RowStyle = rowStyle;
            Color softGreen = Color.FromRgb(221, 255, 187);

            if (string.IsNullOrEmpty(textBoxReason.Text))
            {
                MessageBox.Show("Заполните строку \"Причина обращения\"!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            WarrantyServiceRepository warrantyServiceRepo = new WarrantyServiceRepository();
            WarrantyService warrantyService = warrantyServiceRepo.FindByReason(textBoxReason.Text);

            if (warrantyService == null)
            {
                MessageBox.Show("Запись содержащая причину обращения: \"" + textBoxReason.Text + "\" не найдена.", "Запись не найдена", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            warrantyService = WarrantyServices.Where(w => w.Id == warrantyService.Id).FirstOrDefault();
            dataGrid1.ScrollIntoView(warrantyService);
            DataGridRow row = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromItem(warrantyService);
            if (row != null)
            {
                row.Focus();
                row.Background = new SolidColorBrush(softGreen);
            }
        }
        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            PurchaseRepository purchaseRepo = new PurchaseRepository();

            int? employeeId = null;
            int? discountCardId = null;
            Employee employee = (Employee)comboBoxEmployee.SelectedItem;
            if (employee != null) 
            {
                employeeId = employee.Id;
            }
            DiscountCard discountCard = (DiscountCard)comboBoxDiscountCard.SelectedItem;
            if (discountCard != null)
            {
                discountCardId = discountCard.Id;
            }
            dataGrid1.ItemsSource = purchaseRepo.FilterPurchaseByEmployeeAndBuyer(employeeId, discountCardId);
        }

        private void MenuItem_ShowViewPurchase(object sender, RoutedEventArgs e)
        {
            table = "null";

            PurchaseRepository purchaseRepo = new PurchaseRepository();
            textTableNameRun2.Text = "Покупки (расширенный)";
            dataGrid1.Columns.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.AutoGenerateColumns = true;
            dataGrid1.ItemsSource = purchaseRepo.FullInformationPurchase().DefaultView;
            SetButtonsVisible();
        }

        private void SetButtonsVisible()
        {
            if (table == "null")
            {
                buttonAdd.IsEnabled = false;
                buttonUpdate.IsEnabled = false;
                buttonDelete.IsEnabled = false;
                groupBoxPurchase.Visibility = Visibility.Collapsed;
                groupBoxWarrantyService.Visibility = Visibility.Collapsed;
                editLabel.Content = "Редактирование\n(недоступно)";
                editLabel.FontStyle = FontStyles.Italic;
            }

            if (table != "null" && Authorization.Role == "admin")
            {
                buttonAdd.IsEnabled = true;
                buttonUpdate.IsEnabled = true;
                buttonDelete.IsEnabled = true;
                editLabel.Content = "Редактирование";
                editLabel.FontStyle = FontStyles.Normal;
                
                if (dataGrid1.Items.Count == 0)
                {
					buttonUpdate.IsEnabled = false;
					buttonDelete.IsEnabled = false;
				}
            }
            
            if (table != "null" && Authorization.Role == "user")
            {
                buttonAdd.IsEnabled = false;
                buttonUpdate.IsEnabled = false;
                buttonDelete.IsEnabled = false;
                editLabel.Content = "Редактирование\n(недоступно)";
                editLabel.FontStyle = FontStyles.Italic;
                logsButton.IsEnabled = false;
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxEmployee.Text = "";
            comboBoxDiscountCard.Text = "";
            textBoxReason.Text = "";
            radioButtonDate.IsChecked = false;
            radioButtonResult.IsChecked = false;
        }

    }
}
