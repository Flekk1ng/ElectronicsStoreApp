using System;
using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class WarrantyService : INotifyPropertyChanged
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

        private ServiceCenter serviceCenter;
        public ServiceCenter ServiceCenter
        {
            get { return serviceCenter; }
            set
            {
                if (serviceCenter != value)
                {
                    serviceCenter = value;
                    OnPropertyChanged("ServiceCenter");
                }
            }
        }

        private string reasonForPetition;
        public string ReasonForPetition
        {
            get { return reasonForPetition; }
            set
            {
                if (reasonForPetition != value)
                {
                    reasonForPetition = value;
                    OnPropertyChanged("ReasonForPetition");
                }
            }
        }

        private DateTime dateOfPetition;
        public DateTime DateOfPetition
        {
            get { return dateOfPetition; }
            set
            {
                if (dateOfPetition != value)
                {
                    dateOfPetition = value;
                    OnPropertyChanged("DateOfPetition");
                }
            }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged("Result");
                }
            }
        }

        public WarrantyService() { }

        public WarrantyService(Purchase purchase, ServiceCenter serviceCenter, string reasonForPetition, 
            DateTime dateOfPetition, string result)
        {
            Purchase = purchase;
            ServiceCenter = serviceCenter;
            ReasonForPetition = reasonForPetition;
            DateOfPetition = dateOfPetition;
            Result = result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
