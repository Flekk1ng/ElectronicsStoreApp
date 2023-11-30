using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class DiscountCard : INotifyPropertyChanged
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

        public DiscountCard() { }

        public DiscountCard(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
