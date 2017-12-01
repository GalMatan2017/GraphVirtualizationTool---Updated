using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace GraphVirtualizationTool
{
    public class MainViewModel: INotifyPropertyChanged
    {
        #region Collections
        private ObservableCollection<Node> _nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes ?? (_nodes = new ObservableCollection<Node>()); }
        }

        private ObservableCollection<Connector> _connectors;
        public ObservableCollection<Connector> Connectors
        {
            get { return _connectors ?? (_connectors = new ObservableCollection<Connector>()); }
        }

        #endregion

        #region Bool (Visibility) Options

        private bool _showNames;
        public bool ShowNames
        {
            get { return _showNames; }
            set
            {
                _showNames = value;
                OnPropertyChanged("ShowNames");
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _nodes = new ObservableCollection<Node>(NodesDataSource.GetRandomNodes());
            _connectors = new ObservableCollection<Connector>(NodesDataSource.GetRandomConnectors(Nodes.ToList()));
            CreateNewNode();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Creating New Objects

        private bool _creatingNewNode;
        public bool CreatingNewNode
        {
            get { return _creatingNewNode; }
            set
            {
                _creatingNewNode = value;
                OnPropertyChanged("CreatingNewNode");

                if (value)
                    for (int i = 0; i < 10; i++)
                        CreateNewNode();
            }
        }

        public void CreateNewNode()
        {
            var newnode = new Node()
                              {
                                  Name = ""+(Nodes.Count + 1),
                              };

            Nodes.Add(newnode);
        }


        private bool _creatingNewConnector;
        public bool CreatingNewConnector
        {
            get { return _creatingNewConnector; }
            set
            {
                _creatingNewConnector = value;
                OnPropertyChanged("CreatingNewConnector");

                if (value) { }
                    CreateNewConnector();
            }
        }

        public void CreateNewConnector()
        {
            var connector = new Connector()
                                {
                                    Name = "Connector" + (Connectors.Count + 1),
                                };

            //Connectors.Add(connector);
        }

        #endregion

        #region Scrolling support

        private double _areaHeight = 500;
        public double AreaHeight
        {
            get { return _areaHeight; }
            set
            {
                _areaHeight = value;
                OnPropertyChanged("AreaHeight");
            }
        }

        private double _areaWidth = 500;
        public double AreaWidth
        {
            get { return _areaWidth; }
            set
            {
                _areaWidth = value;
                OnPropertyChanged("AreaWidth");
            }
        }

        #endregion
    }
}
