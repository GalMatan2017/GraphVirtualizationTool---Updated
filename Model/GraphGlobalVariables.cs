using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool
{
    public class GraphGlobalVariables
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
        
        public  string FileNamePath { get; set; }
        public  string FileName{ get; set; }
        public  int graphTypeFlag { get; set; }
        public  int FileButtonFlag { get; set; }

        public const int MATRIX_FLAG = 4;

        public const int LIST_FLAG = 5;

    }
}
