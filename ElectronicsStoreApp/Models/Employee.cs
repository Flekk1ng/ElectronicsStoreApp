using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class Employee : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public Employee() { }

        public Employee(string fullName, string address, string email)
        {
            FullName = fullName;
            Address = address;
            Email = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
