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
