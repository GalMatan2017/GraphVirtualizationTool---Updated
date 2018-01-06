using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class SparseGraph : Graph
    {
        private List<List<int>> _list;
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
            return (List<List<TValue>>)Convert.ChangeType(_list, typeof(List<List<TValue>>));
        }
        public void setGraph<TValue>(TValue graph)
        {
            _list = new List<List<int>>();
            _list = (List<List<int>>)Convert.ChangeType(graph, typeof(List<List<int>>));
        }
        List<int> Graph.getNeighbours(int node)
        { 
                return _list[node-1]; 
        }
    }
}
