using Microsoft.Win32;
using System.Windows.Controls;

namespace GraphVirtualizationTool
{
    public partial class GraphController : UserControl
    {
       
        public GraphController()
        {
            InitializeComponent();

        }

        private void onOpenFileClickButton(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                GraphFile.FileName  = openFileDialog.FileName;
            }
        }
    
    }
}
