using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace GraphVirtualizationTool.Model
{
    class GraphRealization
    {

        public static Tuple<IEnumerable<Node>, IEnumerable<Edge>> draw<T>(Graph graph,int[] colorArr,int[] conn_comps,int marginX, int marginY)
        {
            List<Node> nodes = new List<Node>();
            List<Edge> edges = new List<Edge>();
            Random random = new Random();
            int rows = graph.getData<T>().Count;


            int startY = 50;
            int comps = 0;
            int total_nodes = graph.getData<T>().Count;

            List<Point> coordinates = new List<Point>();

            foreach (var comp in conn_comps)
            {
                if (comp > comps)
                    comps = comp;
            }

            double[] normalized = new double[comps];

            foreach (var comp in conn_comps)
            {
                ++normalized[comp - 1];
            }

            for (int i = 0; i < comps; i++)
            {
                normalized[i] /= total_nodes;
            }

            if (graph.IsBipartite)
            {
                int[] comps_zeros = new int[comps];
                int[] comps_ones = new int[comps];
                int maxValue = -1;
                int[] yFactor = new int[comps*2];

                for (int i = 0; i < comps * 2; i++)
                {
                    yFactor[i] = startY;
                }

                for (int i = 0; i < total_nodes; i++)
                {
                    if (colorArr[i] == 0)
                        ++comps_zeros[conn_comps[i] - 1];
                    else
                        ++comps_ones[conn_comps[i] - 1];
                }

                for (int i = 0; i < comps; i++)
                    if (comps_zeros[i] > maxValue)
                        maxValue = comps_zeros[i];

                for (int i = 0; i < comps; i++)
                    if (comps_ones[i] > maxValue)
                        maxValue = comps_ones[i];

                for (int i = 0; i < total_nodes; i++)
                {
                    coordinates.Add(new Point(2*conn_comps[i] * marginX - colorArr[i] * marginX,yFactor[2 * conn_comps[i] - colorArr[i] - 1]));
                    yFactor[2 * conn_comps[i] - colorArr[i] - 1] += marginY;
                }

                MainViewModel.getInstance().CanvasHeight = (maxValue * Node._nodeSize) + (marginY * (maxValue-1));
                MainViewModel.getInstance().CanvasWidth = (comps*2 * Node._nodeSize) + (marginX * (comps*2));
            }

            else
            {

            }
            //Matrix case
            if (typeof(T) == typeof(bool))
            {
                for (int row = 0; row < rows; row++)
                {
                    SolidColorBrush color;
                    if (colorArr[row] == 0)
                        color = new SolidColorBrush(Colors.Blue);
                    else
                        color = new SolidColorBrush(Colors.Orange);
                    nodes.Add(
                        new Node()
                        {
                            Name = $"node {row + 1}",
                            X = coordinates[row].X,
                            Y = coordinates[row].Y,
                            NodeColor = (color)
                        });
                }

                for (int row = 0; row < graph.getData<T>().Count; row++)
                {
                    for (int col = graph.getData<T>().Count - 1; col > row - 1; col--)
                    {
                        if (col == row)
                            continue;
                        if ((bool)Convert.ChangeType(graph.getData<T>().ElementAt(row).ElementAt(col),typeof(bool)) == true)
                        {
                            edges.Add(new Edge()
                            {
                                Name = $"connector {new Random().Next(999)}",
                                Start = nodes.Single(x => x.Name.Equals($"node {row+1}")),
                                End = nodes.Single(x => x.Name.Equals($"node {col+1}"))
                            });
                        }
                    }
                }
            }
            //List case
            else
            {
                for (int row = 0; row < rows; row++)
                {

                    SolidColorBrush color;
                    if (colorArr[row] == 0)
                        color = new SolidColorBrush(Colors.Blue);
                    else
                        color = new SolidColorBrush(Colors.Orange);
                    nodes.Add(
                        new Node()
                        {
                            Name = $"node {graph.getData<T>().ElementAt(row).ElementAt(0)}",
                            X = coordinates[row].X,
                            Y = coordinates[row].Y,
                            NodeColor = (color)
                        });
                }
                for (int row = 0; row < graph.getData<T>().Count; row++)
                {
                    for (int col = 1; col < graph.getData<T>().ElementAt(row).Count; col++)
                    {
                        if (edges.Find(x => x.Start.Name.Equals($"node {graph.getData<T>().ElementAt(row).ElementAt(col)}")
                                            && x.End.Name.Equals(($"node {graph.getData<T>().ElementAt(row).ElementAt(0)}"))) != null)
                            continue;
                        else
                        {
                            edges.Add(new Edge()
                            {
                                Name = $"edge {new Random().Next(999)}",
                                Start = nodes.Single(x => x.Name.Equals($"node {graph.getData<T>().ElementAt(row).ElementAt(0)}")),
                                End = nodes.Single(x => x.Name.Equals($"node {graph.getData<T>().ElementAt(row).ElementAt(col)}"))
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
