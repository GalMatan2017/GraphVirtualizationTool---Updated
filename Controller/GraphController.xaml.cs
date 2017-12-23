using System.IO;
using System.Windows.Controls;

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

        private void onOpenMatrixFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                GraphGlobalVariables.getInstance().FileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().FileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };
                MainViewModel.getInstance().N
            }
        }


        private void onOpenListFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                GraphGlobalVariables.getInstance().FileNamePath = openFileDialog.FileName;
                GraphGlobalVariables.getInstance().FileName = Path.GetFileName(GraphGlobalVariables.getInstance().FileNamePath);
                fileName.DataContext = new TextBlockText() { textdata = Path.GetFileName(openFileDialog.FileName) };
            }
        }
    }
}
