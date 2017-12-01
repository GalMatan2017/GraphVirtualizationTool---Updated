using System;
namespace GraphVirtualizationTool
{
    public class Node: DiagramObject
    {
        private double _x;
        public override double X
        {
            get { return _x; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 50.
                _x = value;
                OnPropertyChanged("X");
            }
        }

        private double _y;       
        public override double Y
        {
            get { return _y; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 50.
                _y = value;
                OnPropertyChanged("Y");
            }
        }
    }
}