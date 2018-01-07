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

        public GraphController()
        {
            InitializeComponent();
            globals = GraphGlobalVariables.getInstance();
            algorithms = new Algorithms();
            fileName.DataContext = globals;
            graphInfo.DataContext = globals;
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
                        globals.GraphType = GraphGlobalVariables.GraphTypes.Sparse;
                    else
                        globals.GraphType = GraphGlobalVariables.GraphTypes.Dense;
                    reader.Close();
                }

                if (globals.GraphType == GraphGlobalVariables.GraphTypes.Dense)
                {
                    graph = new DenseGraph();
                    AdjacencyMatrix am = new AdjacencyMatrix();

                    graph.setGraph(am.ParseFile<bool>(globals.Filepath));

                    int size = graph.getGraph<bool>().Count;
                    int[] colorArr = new int[size]; // number of vertices to be "colored"
                    int[] componentlist = new int[size];// number of vertices which each of vertex represented by the list index and the value is the component class number

                    if (algorithms.isBipartite(graph.getGraph<bool>(), size, colorArr, GraphGlobalVariables.GraphTypes.Dense, componentlist))
                    {
                        globals.GraphInfo = "Bipartite!";
                    }
                    else
                    {
                        globals.GraphInfo = "";
                    }
                    GraphRealization.draw(graph.getGraph<bool>());
                }

                else
                {
                    graph = new SparseGraph();
                    AdjacencyList am = new AdjacencyList();

                    graph.setGraph(am.ParseFile<int>(globals.Filepath));

                    int size = graph.getGraph<int>().Count;
                    int[] colorArr = new int[size]; // number of vertices to be "colored"
                    int[] componentlist = new int[size];// number of vertices which each of vertex represented by the list index and the value is the component class number

                    if (algorithms.isBipartite(graph.getGraph<int>(), size, colorArr, GraphGlobalVariables.GraphTypes.Sparse, componentlist))
                    {
                        globals.GraphInfo = "Bipartite!";
                    }
                    else
                    {
                        globals.GraphInfo = "";
                    }
                    GraphRealization.draw(graph.getGraph<int>());
                }
            }
        }
    }
}
