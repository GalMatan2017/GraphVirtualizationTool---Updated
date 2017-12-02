using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool
{
  /*  public class GraphFile : INotifyPropertyChanged
    {
        private GraphFile() { }
        public GraphFile Instance { get; } = new GraphFile();
        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(IsReadOnly)));
                }
            }
        }



        public event PropertyChangedEventHandler StaticPropertyChanged;

        private string _fileName;

        public   string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }

        }

        private void OnPropertyChanged(String property)
        {
            if (StaticPropertyChanged != null)
            {
                StaticPropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

      
    }

    
*/
}
