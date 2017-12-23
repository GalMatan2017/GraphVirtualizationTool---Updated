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
                GraphGlobalVariables.getInstance().FileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().FileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };
                AdjacencyMatrix am = new AdjacencyMatrix();
                Tuple<IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readMatrix(am.ParseFile(GraphGlobalVariables.getInstance().FileNamePath));
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
                GraphGlobalVariables.getInstance().FileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().FileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };
                AdjacencyList am = new AdjacencyList();
                Tuple<IEnumerable<Node>, IEnumerable<Edge>> objecta = am.readGraph(am.ParseFile(GraphGlobalVariables.getInstance().FileNamePath));
                MainViewModel.getInstance().Nodes = new System.Collections.ObjectModel.ObservableCollection<Node>(objecta.Item1);
                MainViewModel.getInstance().Edges = new System.Collections.ObjectModel.ObservableCollection<Edge>(objecta.Item2);
            }
        }
    }
}
