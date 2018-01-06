using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class DenseGraph : Graph
    {
        private List<List<bool>> _matrix;
        private List<int> templist = new List<int>();
        public string TypeName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public List<List<TValue>> getGraph<TValue>()
        {
            return (List<List<TValue>>)Convert.ChangeType(_matrix, typeof(List<List<TValue>>));
        }
        public void setGraph<TValue>(TValue graph)
        {
            _matrix = new List<List<bool>>();
            _matrix = (List<List<bool>>)Convert.ChangeType(graph, typeof(List<List<bool>>));
        }
        List<int> Graph.getNeighbours(int node)
        {
            for(int i = 0; i < _matrix.Count; i++)
            {
                if(_matrix[node-1][i] == true)
                    templist.Add( i+1);
            }
            return templist;
        }
    }
}
