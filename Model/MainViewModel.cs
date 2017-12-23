using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace GraphVirtualizationTool
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private MainViewModel() {

            ShowNames = true;

        }

        private static MainViewModel instance = null;
        public static MainViewModel getInstance()
        {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }
                return instance;
        }

        #region Collections

        private ObservableCollection<Node> _nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes ?? (_nodes = new ObservableCollection<Node>()); }
            set
            {
                if (value != null)
                {
                    _nodes = value;
                    OnPropertyChanged("Nodes");
                }
            }
        }

        private ObservableCollection<Edge> _edges;
        public ObservableCollection<Edge> Edges
        {
            get { return _edges ?? (_edges = new ObservableCollection<Edge>()); }
            set
            {
                if (value != null)
                {
                    _edges = value;
                    OnPropertyChanged("Edges");
                }
            }
        }
        private DiagramObject _selectedObject;

        public DiagramObject SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                Nodes.ToList().ForEach(x => x.IsHighlighted = false);

                _selectedObject = value;
                OnPropertyChanged("SelectedObject");

                //DeleteCommand.IsEnabled = value != null;

                var connector = value as Edge;
                if (connector != null)
                {
                    if (connector.Start != null)
                        connector.Start.IsHighlighted = true;

                    if (connector.End != null)
                        connector.End.IsHighlighted = true;
                }

            }
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Scrolling support

        private double _areaHeight = 768;
        public double AreaHeight
        {
            get { return _areaHeight; }
            set
            {
                _areaHeight = value;
                OnPropertyChanged("AreaHeight");
            }
        }

        private double _areaWidth = 1024;
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
