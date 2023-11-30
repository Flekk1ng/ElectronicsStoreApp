using System;
using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class InstallmentPlan : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private Purchase purchase;
        public Purchase Purchase
        {
            get { return purchase; }
            set
            {
                if (purchase != value)
                {
                    purchase = value;
                    OnPropertyChanged("Purchase");
                }
            }
        }

        private decimal paymentAmount;
        public decimal PaymentAmount
        {
            get { return paymentAmount; }
            set
            {
                if (paymentAmount != value)
                {
                    paymentAmount = value;
                    OnPropertyChanged("PaymentAmount");
                }
            }
        }

        private DateTime dateOfPayment;
        public DateTime DateOfPayment
        {
            get { return dateOfPayment; }
            set
            {
                if (dateOfPayment != value)
                {
                    dateOfPayment = value;
                    OnPropertyChanged("DateOfPayment");
                }
            }
        }

        public InstallmentPlan() { }

        public InstallmentPlan(Purchase purchase, decimal paymentAmount, DateTime dateOfPayment)
        {
            Purchase = purchase;
            PaymentAmount = paymentAmount;
            DateOfPayment = dateOfPayment;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
