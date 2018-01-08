using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class DenseGraph : Graph, INotifyPropertyChanged
    {
        private List<List<bool>> matrix;
        public List<List<T>> getGraph<T>()
        {
            return (List<List<T>>)Convert.ChangeType(matrix, typeof(List<List<T>>));
        }
        public void setGraph<T>(T graph)
        {
            matrix = new List<List<bool>>();
            matrix = (List<List<bool>>)Convert.ChangeType(graph, typeof(List<List<bool>>));
        }
        public List<int> getNeighbours(int node)
        {
            List<int> neighbours = new List<int>();
            for(int i = 0; i < matrix.Count; i++)
            {
                if(matrix[node-1][i] == true)
                    neighbours.Add(i+1);
            }
            return neighbours;
        }
        public GraphTypes GraphType { get; set; } = GraphTypes.Dense;
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
