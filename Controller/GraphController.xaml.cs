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
            if (GraphGlobalVariables.getInstance().graphTypeFlag == 1) {
                graphType.DataContext = new TextBlockText() { textdata = "Bipartite Graph!" };
            }
        }

        private void onOpenDenseFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                GraphGlobalVariables.getInstance().MatrixFileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().MatrixFileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };

               

                 _graph = new DenseGraph();
                AdjacencyMatrix am = new AdjacencyMatrix();
                _graph.setGraph(am.ParseFile<List<List<bool>>>(GraphGlobalVariables.getInstance().MatrixFileNamePath));
                Algorithms aa = new Algorithms();
                int size= _graph.getGraph<List<List<bool>>>().Count;
                
                int[] colorArr = new int[size]; // number of vertices to be "colored"
                if (aa.isBipartite(_graph.getGraph<List<List<bool>>>(), 0, colorArr))
                {
                    graphType.DataContext = new TextBlockText() { textdata = "Bipartite Graph!" };
                }
                Tuple <IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readMatrix(_graph.getGraph<List<List<bool>>>());
                MainViewModel.getInstance().Nodes = new System.Collections.ObjectModel.ObservableCollection<Node>(objecta.Item1);
                MainViewModel.getInstance().Edges = new System.Collections.ObjectModel.ObservableCollection<Edge>(objecta.Item2);
            }
        }
       

        
        private void onOpenSparseFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                GraphGlobalVariables.getInstance().ListFileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().ListFileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };

                _graph = new SparseGraph();


                AdjacencyList am = new AdjacencyList();
                _graph.setGraph( am.ParseFile<List<List<int>>>(GraphGlobalVariables.getInstance().ListFileNamePath));
                Algorithms aa = new Algorithms();

                int size = _graph.getGraph<List<List<int>>>().Count;
                int[] colorArr = new int[size+1]; // number of vertices to be "colored"
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
