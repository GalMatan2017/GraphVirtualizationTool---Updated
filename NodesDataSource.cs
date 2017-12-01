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
                Name = "Node" + random.Next(0, 100), //node id between 0 - 100
                X = random.Next(50, 500), //position x axis

                Y = random.Next(50, 500) //position y axis
            };

        }
        public static IEnumerable<Node> GetRandomNodes()
        {
            return Enumerable.Range(2, 2).Select(x => GetRandomNode());
        }

        public static Connector GetRandomConnector(IEnumerable<Node> nodes)
        {
            return new Connector
            {

            };
        }

        public static IEnumerable<Connector> GetRandomConnectors(List<Node> nodes)
        {
            var result = new List<Connector>();
            for (int i = 0; i < nodes.Count() - 1; i++)
            {
                result.Add(new Connector()
                {
                    Start = nodes[i],
                    End = nodes[i + 1],
                    Name = "Connector" + random.Next(1, 100).ToString()
                });
            }
            return result;
        }


        public static List<List<int>> parseAdjMatrix()
        {
            List<List<int>> matrix = new List<List<int>>();

            StreamReader reader = File.OpenText("filename.txt");

            string line;
            int columns = 0,
                rows = 0;
            //read line
            while ((line = reader.ReadLine()) != null)
            {
                //split by whitespace
                string[] items = line.Split(null);
                ++rows;
                //convert to integers
                int[] convertedItems = Array.ConvertAll(items, int.Parse);
                foreach (var item in convertedItems)
                    if (item != 0 && item != 1)
                        throw new Exception("Found illegal character");
                if (rows == 1)
                {
                    matrix.Add(convertedItems.ToList());
                    //columns constant integer is initiliazed
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

            return matrix;
        }

        public static Tuple<IEnumerable<Node>, IEnumerable<Connector>> readMatrix(List<List<int>> matrix)
        {
            List<Node> nodes = new List<Node>();
            List<Connector> connectors = new List<Connector>();

            int columns = matrix.Count;

            for (int i = 0; i < columns; i++)
                nodes.Add(new Node() { Name = $"node {i}", X = new Random().Next(100), Y = new Random().Next(100) });
            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = 0; col < matrix.Count - row; col++)
                {
                    if (col == row)
                        continue;
                    if (matrix.ElementAt(row).ElementAt(col) == 1)
                    {
                        connectors.Add(new Connector()
                        {
                            Name = $"connector {new Random().Next(999)}",
                            Start = nodes.Single(x => x.Name.Equals($"node {row}")),
                            End = nodes.Single(x => x.Name.Equals($"node {col}"))
                        });
                    }
                }
            }

            return new Tuple<IEnumerable<Node>, IEnumerable<Connector>>(nodes, connectors);

        }

        public static List<List<int>> parseAdjList()
        {
            List<List<int>> list = new List<List<int>>();

            StreamReader reader = File.OpenText("filename.txt");

            string line;
            int columns = 0,
                rows = 0;
            //read line
            while ((line = reader.ReadLine()) != null)
            {
                //split by whitespace
                string[] items = line.Split(null);
                ++rows;
                //convert to integers
                int[] convertedItems = Array.ConvertAll(items, int.Parse);
                if (rows == 1)
                {
                    list.Add(convertedItems.ToList());
                    //columns constant integer is initiliazed
                    columns = convertedItems.Length;
                }
                else if (convertedItems.Length == columns)
                {
                    list.Add(convertedItems.ToList());
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

            return list;
        }
    }
}