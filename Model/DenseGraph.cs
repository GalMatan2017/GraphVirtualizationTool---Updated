using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class DenseGraph : Graph
    {
        private List<List<bool>> _matrix;

        
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

        
        
        public  TValue getGraph<TValue>()
        {

            return (TValue)Convert.ChangeType(_matrix, typeof(TValue));

        }

        public void setGraph<TValue>(TValue graph)
        {
            _matrix = new List<List<bool>>();
            _matrix = (List<List<bool>>)Convert.ChangeType(graph, typeof(List<List<bool>>));
        }
    }
}
