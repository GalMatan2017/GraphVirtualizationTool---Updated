using System;
using System.Collections.Generic;

namespace GraphVirtualizationTool.Model
{
    interface  Graph
    {  
         string TypeName { get; set; }

        TValue getGraph<TValue>();

        void setGraph<TValue>(TValue graph);


        List<int> getNeighbors(int node);
    }

}
