using System;
using System.ComponentModel;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphVirtualizationTool
{
    public class GraphGlobalVariables : INotifyPropertyChanged
    {
        private GraphGlobalVariables() { }
        private static GraphGlobalVariables instance = null;
        public static GraphGlobalVariables getInstance()
        {
            if (instance == null)
            {
                instance = new GraphGlobalVariables();
            }
            return instance;
        }
        private string _filename { get; set; }
        public string Filepath { get; set; }
        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                if (value != null)
                    _filename = value;
                OnPropertyChanged("Filename");
            }
        }
        public int TryParseInt32(string text, out int value)
        {
            int tmp;
            if (int.TryParse(text, out tmp))
            {
                value = tmp;
                return value;
            }
            else
            {
                value = -1;
                return value;
            }
        }


        public void ExportToPng(Uri path, Canvas surface)
        {
            System.IO.Directory.CreateDirectory("C:/GraphVirtualizationSaves/");
            path = new Uri($"C:/GraphVirtualizationSaves/{DateTime.Now.ToString("MM-d-Y")}.bmp");
            if (path == null) return;
            DirectoryInfo dInfo = new DirectoryInfo("C:/GraphVirtualizationSaves");
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
            // Get the size of canvas
            Size size = new Size(surface.Width, surface.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            Canvas printCanvas = new Canvas();
            printCanvas.Background = new VisualBrush(surface);
            printCanvas.Measure(size);
            printCanvas.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(printCanvas);

            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
