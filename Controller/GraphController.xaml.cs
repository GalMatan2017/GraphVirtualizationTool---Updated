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
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };

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
                    if (aa.isBipartite(_graph.getGraph<List<List<bool>>>(), 0, colorArr))
                    {
                        graphType.DataContext = new TextBlockText() { textdata = "Bipartite Graph!" };
                    }
                    Tuple<IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readMatrix(_graph.getGraph<List<List<bool>>>());
                    MainViewModel.getInstance().Nodes = new System.Collections.ObjectModel.ObservableCollection<Node>(objecta.Item1);
                    MainViewModel.getInstance().Edges = new System.Collections.ObjectModel.ObservableCollection<Edge>(objecta.Item2);
                }

                if (GraphGlobalVariables.getInstance().FileButtonFlag == GraphGlobalVariables.LIST_FLAG)
                {
                    _graph = new SparseGraph();
                    
                    AdjacencyList am = new AdjacencyList();
                    _graph.setGraph(am.ParseFile<List<List<int>>>(GraphGlobalVariables.getInstance().FileNamePath));
                    Algorithms aa = new Algorithms();

                    int size = _graph.getGraph<List<List<int>>>().Count;
                    int[] colorArr = new int[size + 1]; // number of vertices to be "colored"
                    if (aa.isBipartite(_graph.getGraph<List<List<int>>>(), 1, colorArr))
                    {
                        graphType.DataContext = new TextBlockText() { textdata = "Bipartite Graph!" };
                    }
                    Tuple<IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readGraph(_graph.getGraph<List<List<int>>>());
                    MainViewModel.getInstance().Nodes = new System.Collections.ObjectModel.ObservableCollection<Node>(objecta.Item1);
                    MainViewModel.getInstance().Edges = new System.Collections.ObjectModel.ObservableCollection<Edge>(objecta.Item2);
                    
                }

            }
        }
    }
}
