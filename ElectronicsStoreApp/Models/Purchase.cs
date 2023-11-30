using System;
using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class Purchase : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private Technic technic;
        public Technic Technic
        {
            get { return technic; }
            set
            {
                if (technic != value)
                {
                    technic = value;
                    OnPropertyChanged("Technic");
                }
            }
        }

        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set
            {
                if (employee != value)
                {
                    employee = value;
                    OnPropertyChanged("Employee");
                }
            }
        }

        private DiscountCard discountCard;
        public DiscountCard DiscountCard
        {
            get { return discountCard; }
            set
            {
                if (discountCard != value)
                {
                    discountCard = value;
                    OnPropertyChanged("DiscountCard");
                }
            }
        }

        private DateTime dateOfPurchase;
        public DateTime DateOfPurchase
        {
            get { return dateOfPurchase; }
            set
            {
                if (dateOfPurchase != value)
                {
                    dateOfPurchase = value;
                    OnPropertyChanged("DateOfPurchase");
                }
            }
        }

        public Purchase() { }

        public Purchase(Technic technic, Employee employee, DiscountCard discountCard, DateTime dateOfPurchase)
        {
            Technic = technic;
            Employee = employee;
            DiscountCard = discountCard;
            DateOfPurchase = dateOfPurchase;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
