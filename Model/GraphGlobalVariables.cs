using System.ComponentModel;

namespace GraphVirtualizationTool
{
    public class GraphGlobalVariables : INotifyPropertyChanged
    {
        public enum GraphTypes { Dense, Sparse }
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
        private string _graphinfo { get; set; }
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
        public GraphTypes GraphType { get; set; }

        public string GraphInfo
        {
            get
            {
                return _graphinfo;
            }
            set
            {
                if (value != null)
                    _graphinfo = value;
                OnPropertyChanged("GraphInfo");
            }
        }

    }
}
