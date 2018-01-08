using System.ComponentModel;

namespace GraphVirtualizationTool
{
    public class GraphGlobalVariables : INotifyPropertyChanged
    {
        private GraphGlobalVariables() { }

        private static GraphGlobalVariables instance = null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static GraphGlobalVariables getInstance()
        {
            if (instance == null)
            {
                instance = new GraphGlobalVariables();
            }
            return instance;
        }
        private string _filename { get; set; }
        public string Filepath { get; set; }
        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                if (value != null)
                    _filename = value;
                OnPropertyChanged("Filename");
            }
        }
        public int TryParseInt32(string text, ref int value)
        {
            int tmp;
            if (int.TryParse(text, out tmp))
            {
                value = tmp;
                return 1;
            }
            else
            {
                return -1; // Leave "value" as it was
            }
        }
    }
}
