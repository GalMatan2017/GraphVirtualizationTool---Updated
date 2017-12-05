using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GraphVirtualizationTool
{
    public partial class GraphController : UserControl
    {
        public GraphController()
        {
            InitializeComponent();
            if (GraphGlobalVariables.graphTypeFlag == 1) {
                graphType.DataContext = new TextBlockText() { textdata = "Bipartite Graph!" };
            }
        }


        private void onOpenFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                
            if (openFileDialog.ShowDialog() == true)
            {
                GraphGlobalVariables.FileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.FileName = Path.GetFileName(GraphGlobalVariables.FileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };

            }
        }
        
    
    }
}
