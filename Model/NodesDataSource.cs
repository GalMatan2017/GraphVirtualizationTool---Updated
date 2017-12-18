using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphVirtualizationTool
{
    public static class NodesDataSource
    {
        public static Random random = new Random();
        private static int nodeIndex=1;

        
        public static Node GetRandomNode() //instead of random, create node with specific parameters for example biparitite graph with get a static X axis and random y axis
        {

            return new Node
            {
               
                Name = "" + nodeIndex++,
                    X = random.Next(50, 1024), //position x axis // draw area can be change depends on the number of nodes
                    Y = random.Next(50,768) //position y axis //draw area can be change depends on the number of nodes
            };
            
        }
        public static IEnumerable<Node> GetRandomNodes()
        {
            return Enumerable.Range(500, 500).Select(x => GetRandomNode());
        }

        internal static List<Node> setNodes(List<Node> listofnodes)
        {
            for (int i=0; i<50;i++)
            listofnodes.Add(GetRandomNode());
            
            return listofnodes;

        }

        public static Edge GetRandomConnector(IEnumerable<Node> nodes)
        {
            return new Edge
                {
                      
                };
        }
       
        public static IEnumerable<Edge> GetRandomConnectors(List<Node> nodes)
        {
            var result = new List<Edge>();
            for (int i = 0; i < nodes.Count() - 1; i++)
            {
                result.Add(new Edge() 
                {
                    Start = nodes[i], 
                    End = nodes[i + 1],
                    Name = "Connector" + random.Next(1, 100).ToString()
                });
            }
            return result;
        }
    }
}

    
 