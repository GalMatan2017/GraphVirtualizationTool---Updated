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
        private double _z;
        public override double Z
        {
            get { return _z; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 50.
                _z = value;
                OnPropertyChanged("Z");
            }
        }

        private bool _isHighlighted { get; set; }
        public bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }
            set
            {
                _isHighlighted = value;
                OnPropertyChanged("IsHighlighted");
            }
        }

        //Node ellipse resizing

        private int nodeSize = 50;
        public int NodeSize
        {
            get { return nodeSize; }
            set
            {
                if (value > 0)
                {
                    nodeSize = value;
                    OnPropertyChanged("NodeSize");
                }
            }
        }
    }
}