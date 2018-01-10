using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using GraphVirtualizationTool.Model;
using System.Windows;

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
            fileName.DataContext = globals;
            graphInfo.DataContext = null;
        }

        private void onOpenGraphFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            algorithms = new Algorithms();
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                globals.Filepath = openFileDialog.FileName;
                globals.Filename = Path.GetFileName(globals.Filepath);

                #region File Open
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
                #endregion
      
                if (type == GraphTypes.Dense)
                {
                    #region Dense
                    graph = new DenseGraph();
                    AdjacencyMatrix am = new AdjacencyMatrix();

                    graph.setData(am.ParseFile<bool>(globals.Filepath));

                    int size = graph.getData<bool>().Count;
                    //number of vertices to be "colored"
                    int[] colorArr = new int[size]; 
                    //number of vertices which each of vertex represented by the list index and the value is the component class number
                    int[] conn_comps = new int[size];

                    if (algorithms.isBipartite<bool>(graph, size, colorArr, GraphTypes.Dense, conn_comps))
                    {
                        graph.IsBipartite = true;
                    }

                    GraphRealization.draw<bool>(graph,colorArr,conn_comps);  
                    #endregion
                }

                else
                {
                    #region Sparse
                    graph = new SparseGraph();
                    AdjacencyList am = new AdjacencyList();

                    graph.setData(am.ParseFile<int>(globals.Filepath));

                    int size = graph.getData<int>().Count;
                    //number of vertices to be colored
                    int[] colorArr = new int[size]; 
                    //number of vertices which each of vertex represented by the list index and the value is the component class number
                    int[] conn_comps = new int[size];

                    if (algorithms.isBipartite<int>(graph, size, colorArr, GraphTypes.Sparse, conn_comps))
                    {
                        graph.IsBipartite = true;
                    }

                    GraphRealization.draw<int>(graph, colorArr, conn_comps);
                    #endregion
                }
                graphInfo.DataContext = graph;
            }
        }
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            MainViewModel.getInstance().CanvasHeight = 300;
            if (cb.Name == "showNamesBox") MainViewModel.getInstance().ShowNames = true;
        }
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Name == "showNamesBox") MainViewModel.getInstance().ShowNames = false;
        }
        private void SaveGraph(object sender, RoutedEventArgs e)
        {
            GraphGlobalVariables.getInstance().ExportToPng(null, MainViewModel.getInstance().MainCanvas);
        }
    }
}
