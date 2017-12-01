using System.ComponentModel;
using System.Linq;

namespace GraphVirtualizationTool
{
    public abstract class DiagramObject:INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public abstract double X {get;set;}

        public abstract double Y {get;set;}


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}