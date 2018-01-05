using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool.Model
{
    class GraphRealization
    {
        public static Tuple<IEnumerable<Node>, IEnumerable<Edge>> draw<T>(List<List<T>> graph)
        {

            List<Node> nodes = new List<Node>();
            List<Edge> edges = new List<Edge>();
            Random random = new Random();
            int rows = graph.Count;

            //Matrix case
            if (typeof(T) == typeof(bool))
            {
                for (int row = 0; row < rows; row++)
                    nodes.Add(
                        new Node()
                        {
                            Name = $"node {row}",
                            X = random.Next(50, 500),
                            Y = random.Next(50, 500)
                        });
                for (int row = 0; row < graph.Count; row++)
                {
                    for (int col = graph.Count - 1; col > row - 1; col--)
                    {
                        if (col == row)
                            continue;
                        if ((bool)Convert.ChangeType(graph.ElementAt(row).ElementAt(col),typeof(bool)) == true)
                        {
                            edges.Add(new Edge()
                            {
                                Name = $"connector {new Random().Next(999)}",
                                Start = nodes.Single(x => x.Name.Equals($"node {row}")),
                                End = nodes.Single(x => x.Name.Equals($"node {col}"))
                            });
                        }
                    }
                }
            }
            //List case
            else
            {
                for (int row = 0; row < rows; row++)
                    nodes.Add(
                        new Node()
                        {
                            Name = $"node {graph.ElementAt(row).ElementAt(0)}",
                            X = random.Next(50, 500),
                            Y = random.Next(50, 500)
                        });
                for (int row = 0; row < graph.Count; row++)
                {
                    for (int col = 1; col < graph.ElementAt(row).Count; col++)
                    {
                        if (edges.Find(x => x.Start.Name.Equals($"node {graph.ElementAt(row).ElementAt(col)}")
                                            && x.End.Name.Equals(($"node {graph.ElementAt(row).ElementAt(0)}"))) != null)
                            continue;
                        else
                        {
                            edges.Add(new Edge()
                            {
                                Name = $"edge {new Random().Next(999)}",
                                Start = nodes.Single(x => x.Name.Equals($"node {graph.ElementAt(row).ElementAt(0)}")),
                                End = nodes.Single(x => x.Name.Equals($"node {graph.ElementAt(row).ElementAt(col)}"))
                            });
                        }
                    }
                }
            }

            //draw
            MainViewModel.getInstance().Nodes = new System.Collections.ObjectModel.ObservableCollection<Node>(nodes);
            MainViewModel.getInstance().Edges = new System.Collections.ObjectModel.ObservableCollection<Edge>(edges);

            return new Tuple<IEnumerable<Node>, IEnumerable<Edge>>(nodes, edges);

        }
    }
}
