using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using GraphVirtualizationTool.Model;

namespace GraphVirtualizationTool
{
    public partial class GraphController : UserControl
    {
        Graph _graph;
        public GraphController()
        {
            InitializeComponent();

        }

        private void onOpenGraphFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                GraphGlobalVariables.getInstance().FileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().FileNamePath);
                fileName.DataContext = new GraphInfo() { textdata = Path.GetFileName(openFileDialog.FileName) };

                StreamReader reader = File.OpenText(GraphGlobalVariables.getInstance().FileNamePath);
                string line;
                if ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(":"))
                        GraphGlobalVariables.getInstance().FileButtonFlag = GraphGlobalVariables.LIST_FLAG;

                    if (line.StartsWith("0,") || line.StartsWith("1,"))
                        GraphGlobalVariables.getInstance().FileButtonFlag = GraphGlobalVariables.MATRIX_FLAG;
                    reader.Close();
                }

                if (GraphGlobalVariables.getInstance().FileButtonFlag == GraphGlobalVariables.MATRIX_FLAG)
                {
                    _graph = new DenseGraph();

                    AdjacencyMatrix am = new AdjacencyMatrix();
                    _graph.setGraph(am.ParseFile<List<List<bool>>>(GraphGlobalVariables.getInstance().FileNamePath));
                    Algorithms aa = new Algorithms();
                    int size = _graph.getGraph<List<List<bool>>>().Count;

                    int[] colorArr = new int[size]; // number of vertices to be "colored"
                    int[] componentlist = new int[size];// number of vertices which each of vertex represented by the list index and the value is the component class number
                    if (aa.isBipartite(_graph.getGraph<List<List<bool>>>(), size, colorArr, GraphGlobalVariables.MATRIX_FLAG, componentlist))
                    {
                        graphType.DataContext = new GraphInfo() { textdata = "Bipartite Graph!" };
                    }
                    GraphRealization.draw(_graph.getGraph<List<List<bool>>>());
                }

                if (GraphGlobalVariables.getInstance().FileButtonFlag == GraphGlobalVariables.LIST_FLAG)
                {
                    _graph = new SparseGraph();

                    AdjacencyList am = new AdjacencyList();
                    _graph.setGraph(am.ParseFile<List<List<int>>>(GraphGlobalVariables.getInstance().FileNamePath));
                    Algorithms aa = new Algorithms();

                    int size = _graph.getGraph<List<List<int>>>().Count;
                    int[] colorArr = new int[size]; // number of vertices to be "colored"
                    int[] componentlist = new int[size];// number of vertices which each of vertex represented by the list index and the value is the component class number

                    if (aa.isBipartite(_graph.getGraph<List<List<int>>>(), size, colorArr, GraphGlobalVariables.LIST_FLAG, componentlist))
                    {
                        graphType.DataContext = new GraphInfo() { textdata = "Bipartite Graph!" };
                    }
                    GraphRealization.draw(_graph.getGraph<List<List<int>>>());
                }

            }
        }
    }
}
