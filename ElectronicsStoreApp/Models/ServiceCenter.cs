using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class ServiceCenter : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
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

        public ServiceCenter() { }

        public ServiceCenter(string name, string address, string email)
        {
            Name = name;
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
