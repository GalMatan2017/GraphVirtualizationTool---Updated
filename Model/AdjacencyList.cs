using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class AdjacencyList : FileHandlerInterface
    {
        public List<List<int>> ParseFile()
        {
            //return value
            List<List<int>> list = new List<List<int>>();

            //open file
            StreamReader reader = File.OpenText("filename.txt");

            string line;
            int columns = 0,
                rows = 0;

            //read line
            while ((line = reader.ReadLine()) != null)
            {
                //split by comma
                string[] items = line.Split(':',',');
                ++rows;
                if (!(items.Length > 1))
                    throw new Exception($"Row {rows} is corrupted!");
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

        public Tuple<IEnumerable<Node>, IEnumerable<Edge>> readGraph(List<List<int>> adjList)
        {
            List<Node> nodes = new List<Node>();
            List<Edge> edges = new List<Edge>();

            int rows = adjList.Count;

            for (int row = 0; row < rows; row++)
                nodes.Add(new Node() {
                    Name = $"node {adjList.ElementAt(row).ElementAt(0)}",
                    X = new Random().Next(100),
                    Y = new Random().Next(100) });

            for (int row = 0; row < adjList.Count; row++)
            {
                for (int col = 1; col < adjList.ElementAt(row).Count; col++)
                {
                    if (edges.Find(x => x.Start.Name.Equals($"node {adjList.ElementAt(row)}")
                                        && x.End.Name.Equals($"node {adjList.ElementAt(col)}")) != null)
                        continue;
                    else
                    {
                        edges.Add(new Edge()
                        {
                            Name = $"connector {new Random().Next(999)}",
                            Start = nodes.Single(x => x.Name.Equals($"node {adjList.ElementAt(row)}")),
                            End = nodes.Single(x => x.Name.Equals($"node {adjList.ElementAt(col)}"))
                        });
                    }
                }
            }

            return new Tuple<IEnumerable<Node>, IEnumerable<Edge>>(nodes, edges);

        }
    }
}
