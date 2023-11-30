using System;
using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class Technic : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set
            {
                if (supplier != value)
                {
                    supplier = value;
                    OnPropertyChanged("Supplier");
                }
            }
        }
        private Type type;
        public Type Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged("Model");
                }
            }
        }
        private DateTime dateOfIssue;
        public DateTime DateOfIssue
        {
            get { return dateOfIssue; }
            set
            {
                if (dateOfIssue != value)
                {
                    dateOfIssue = value;
                    OnPropertyChanged("DateOfIssue");
                }
            }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                if (count != value)
                {
                    count = value;
                    OnPropertyChanged("Count");
                }
            }
        }
        private int guaranteePeriod;
        public int GuaranteePeriod
        {
            get { return guaranteePeriod; }
            set
            {
                if (guaranteePeriod != value)
                {
                    guaranteePeriod = value;
                    OnPropertyChanged("GuaranteePeriod");
                }
            }
        }
        private string typeOfGuarantee;
        public string TypeOfGuarantee
        {
            get { return typeOfGuarantee; }
            set
            {
                if (typeOfGuarantee != value)
                {
                    typeOfGuarantee = value;
                    OnPropertyChanged("TypeOfGuarantee");
                }
            }
        }

        public Technic() { }

        public Technic(Supplier supplier, Type type, string model, DateTime dateOfIssue, decimal price, 
            int count, int guaranteePeriod, string typeOfGuarantee)
        {
            Supplier = supplier;
            Type = type;
            Model = model;
            DateOfIssue = dateOfIssue;
            Price = price;
            Count = count;
            GuaranteePeriod = guaranteePeriod;
            TypeOfGuarantee = typeOfGuarantee;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
