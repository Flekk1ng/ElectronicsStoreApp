using System.ComponentModel;

namespace ElectronicsStoreApp.Models
{
    public class Type : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private Category category;
        public Category Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

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

        public Type() { }

        public Type(Category category, string name)
        {
            Category = category;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
