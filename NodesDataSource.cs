using System;
using System.Collections.Generic;
using System.IO;
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
        public static IEnumerable<Node> GetRandomNodes()
        {
            return Enumerable.Range(2, 2).Select(x => GetRandomNode());
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

        
        public static IEnumerable<Node> parseAdjMatrix()
        {
            StreamReader reader = File.OpenText(GraphFile.FileName);
            IEnumerable<Node> retVal;
            List<List<int>> matrix = new List<List<int>>();
            string line;
            int columns = 0,
                rows = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(null);
                ++rows;
                int[] convertedItems = Array.ConvertAll(items, int.Parse);
                if (rows == 1)
                {
                    matrix.Add(convertedItems.ToList());
                    columns = convertedItems.Length;
                }
                else if (convertedItems.Length == columns)
                {
                    matrix.Add(convertedItems.ToList());
                }
                else
                {
                    throw new Exception($"Row #{rows} is corrupted!");
                }
            }

            if (columns != rows)
            {
                if (rows < columns)
                    throw new Exception("columns is bigger than rows");
                else
                    throw new Exception("rows is bigger than columns");
            }
            else
            {
                foreach (var row in matrix)
                {
                    foreach (var col in row)
                    {
                        ;
                    }
                }
            }
            return retVal = (IEnumerable<Node>)matrix ; // dummy cast in order to compile, need to change

        }

    
       
    }
}