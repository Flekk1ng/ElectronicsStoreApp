using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class Category : INotifyPropertyChanged
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

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
