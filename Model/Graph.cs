using System.Collections.Generic;

namespace GraphVirtualizationTool.Model
{
    abstract class Graph
    {
        public List<Node> nodes { get; set; }

        public List<Edge> edges { get; set; }
        public string TypeName { get; set; }


        abstract public void GraphDensity();

    }
}
