using System.Collections.Generic;

namespace GraphVirtualizationTool.Model
{
    interface Graph
    {
        GraphTypes GraphType { get; set; }
        string GraphInfo { get; set; }
        List<List<T>> getGraph<T>();
        void setGraph<T>(T graph);
        List<int> getNeighbours(int node);
    }
}
