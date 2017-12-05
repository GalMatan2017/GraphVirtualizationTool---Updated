using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    abstract class Graph
    {
        public Node nodes { get; set; }

        public Edge edges { get; set; }
        public  string TypeName { get; set; }


        abstract public void  GraphDensity();     
        
    }
}
