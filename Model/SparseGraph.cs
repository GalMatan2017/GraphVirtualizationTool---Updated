using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class SparseGraph : Graph, INotifyPropertyChanged
    {
        private List<List<int>> list;
        public List<List<T>> getGraph<T>()
        {
            return (List<List<T>>)Convert.ChangeType(list, typeof(List<List<T>>));
        }
        public void setGraph<T>(T graph)
        {
            list = new List<List<int>>();
            list = (List<List<int>>)Convert.ChangeType(graph, typeof(List<List<int>>));
        }
        public List<int> getNeighbours(int node)
        { 
            return list[node-1]; 
        }
        public GraphTypes GraphType { get; set; } = GraphTypes.Sparse;
        private string _graphinfo { get; set; }
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
