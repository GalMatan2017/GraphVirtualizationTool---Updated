using System;
using System.Collections.Generic;

namespace GraphVirtualizationTool.Model
{
    interface  Graph
    {  
        string TypeName { get; set; }
        List<List<TValue>> getGraph<TValue>();
        void setGraph<TValue>(TValue graph);
        List<int> getNeighbours(int node);
    }

}
