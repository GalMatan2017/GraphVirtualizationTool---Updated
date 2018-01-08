using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using GraphVirtualizationTool.Model;

namespace GraphVirtualizationTool
{
    public partial class GraphController : UserControl
    {
        Graph graph;
        GraphGlobalVariables globals;
        Algorithms algorithms;
        GraphTypes type;

        public GraphController()
        {
            InitializeComponent();
            globals = GraphGlobalVariables.getInstance();
            algorithms = new Algorithms();
            fileName.DataContext = globals;
            graphInfo.DataContext = null;
        }

        private void onOpenGraphFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                globals.Filepath = openFileDialog.FileName;
                globals.Filename = Path.GetFileName(globals.Filepath);

                StreamReader reader = File.OpenText(globals.Filepath);
                string line;
                if ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(":"))
                        type = GraphTypes.Sparse;
                    else
                        type = GraphTypes.Dense;
                    reader.Close();
                }

                if (type == GraphTypes.Dense)
                {
                    graph = new DenseGraph();
                    AdjacencyMatrix am = new AdjacencyMatrix();

                    graph.setGraph(am.ParseFile<bool>(globals.Filepath));

                    int size = graph.getGraph<bool>().Count;
                    //number of vertices to be "colored"
                    int[] colorArr = new int[size]; 
                    //number of vertices which each of vertex represented by the list index and the value is the component class number
                    int[] componentlist = new int[size];

                    if (algorithms.isBipartite(graph.getGraph<bool>(), size, colorArr, GraphTypes.Dense, componentlist))
                    {
                        graph.GraphInfo = "Bipartite!";
                    }
                    else
                    {
                        graph.GraphInfo = "";
                    }
                    GraphRealization.draw(graph.getGraph<bool>());
                }

                else
                {
                    graph = new SparseGraph();
                    AdjacencyList am = new AdjacencyList();

                    graph.setGraph(am.ParseFile<int>(globals.Filepath));

                    int size = graph.getGraph<int>().Count;
                    //number of vertices to be colored
                    int[] colorArr = new int[size]; 
                    //number of vertices which each of vertex represented by the list index and the value is the component class number
                    int[] componentlist = new int[size];

                    if (algorithms.isBipartite(graph.getGraph<int>(), size, colorArr, GraphTypes.Sparse, componentlist))
                    {
                        graph.GraphInfo = "Bipartite!";
                    }
                    else
                    {
                        graph.GraphInfo = "";
                    }
                    GraphRealization.draw(graph.getGraph<int>());
                }
                graphInfo.DataContext = graph;

            }
        }
    }
}
