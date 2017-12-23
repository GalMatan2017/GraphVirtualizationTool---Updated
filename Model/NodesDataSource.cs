using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphVirtualizationTool
{
    public static class NodesDataSource
    {
        public static Random random = new Random();

        public static Node GetRandomNode() //instead of random, create node with specific parameters for example biparitite graph with get a static X axis and random y axis
        {
            return new Node
                {
                    Name = "Node" + random.Next(0,100), //node id between 0 - 100
                    X = random.Next(50, 500), //position x axis

                    Y = random.Next(50,500) //position y axis
            }; 
            
        }
        internal static List<Node> setNodes(List<Node> listofnodes)
        {
            listofnodes.Add(GetRandomNode());
            listofnodes.Add(GetRandomNode());
            listofnodes.Add(GetRandomNode());
            return listofnodes;
          
        }
        public static IEnumerable<Node> GetRandomNodes()
        {
           // listofnodes.size()
                // return Enumerable.Range(2, random.Next(2, 4)).Select(x => GetRandomNode()); 
            return Enumerable.Range(2, 2).Select(x => GetRandomNode()); //decides how many nodes will be starting with 2 and + random
        }//instead of random, create a list of nodes with specific information 

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
                {//this is how to connect nodes
                    Start = nodes[i],  //getting info from fileparser 
                    End = nodes[i + 1], //getting info from fileparser
                    Name = "Edge" + random.Next(1, 100).ToString()
                });
            }
            return result;
        }

       
    }
}