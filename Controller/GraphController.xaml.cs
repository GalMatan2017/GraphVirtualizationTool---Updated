using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using GraphVirtualizationTool.Model;

namespace GraphVirtualizationTool
{
    public partial class GraphController : UserControl
    {
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
                AdjacencyMatrix am = new AdjacencyMatrix();
                List<List<bool>> a=am.ParseFile(GraphGlobalVariables.getInstance().MatrixFileNamePath);
                Algorithms aa = new Algorithms();
                int size =a.Count;
                int[] colorArr = new int[size]; // number of vertices to be "colored"
                if (aa.isBipartite(a, 0, colorArr))
                {
                    graphType.DataContext = new TextBlockText() { textdata = "Bipartite Graph!" };
                }
                Tuple <IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readMatrix(a);
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
                AdjacencyList am = new AdjacencyList();
                //Tuple<IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readGraph(am.ParseFile(GraphGlobalVariables.getInstance().ListFileNamePath));
                //MainViewModel.getInstance().Nodes = new System.Collections.ObjectModel.ObservableCollection<Node>(objecta.Item1);
                //MainViewModel.getInstance().Edges = new System.Collections.ObjectModel.ObservableCollection<Edge>(objecta.Item2);
            }
        }
    }
}
