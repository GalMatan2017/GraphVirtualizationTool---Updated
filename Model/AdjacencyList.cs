using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class AdjacencyList : FileHandlerInterface
    {

        public T ParseFile<T>(string filename)
        {
            try
            {
                //return value
                List<List<int>> list = new List<List<int>>();
                //open file
                StreamReader reader = File.OpenText(filename);
                string line;
                int columns = 0,
                    rows = 0;
                //read line
                while ((line = reader.ReadLine()) != null)
                {
                    //split by comma
                    string[] items = line.Split(':', ',');
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
                    else
                    {
                        list.Add(convertedItems.ToList());
                    }
                }
                reader.Close();
                return   (T)Convert.ChangeType(list, typeof(T));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (T)Convert.ChangeType(new List<List<int>>(), typeof(T));
            }
        }

        public Tuple<IEnumerable<Node>, IEnumerable<Edge>> readGraph(List<List<int>> adjList)
        {
            List<Node> nodes = new List<Node>();
            List<Edge> edges = new List<Edge>();
            Random random = new Random();
            int rows = adjList.Count;
            for (int row = 0; row < rows; row++)
                nodes.Add(new Node()
                {
                    Name = $"node {adjList.ElementAt(row).ElementAt(0)}",
                    X = random.Next(50, 500),
                    Y = random.Next(50, 500)
                });
            for (int row = 0; row < adjList.Count; row++)
            {
                for (int col = 1; col < adjList.ElementAt(row).Count; col++)
                {
                    if (edges.Find(x => x.Start.Name.Equals($"node {adjList.ElementAt(row).ElementAt(col)}")
                                        && x.End.Name.Equals(($"node {adjList.ElementAt(row).ElementAt(0)}"))) != null)
                        continue;
                    else
                    {
                        edges.Add(new Edge()
                        {
                            Name = $"edge {new Random().Next(999)}",
                            Start = nodes.Single(x => x.Name.Equals($"node {adjList.ElementAt(row).ElementAt(0)}")),
                            End = nodes.Single(x => x.Name.Equals($"node {adjList.ElementAt(row).ElementAt(col)}"))
                        });
                    }
                }
            }

            return new Tuple<IEnumerable<Node>, IEnumerable<Edge>>(nodes, edges);
        }

        
    }
}
