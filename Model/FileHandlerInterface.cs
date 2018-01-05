using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool
{
    interface FileHandlerInterface
    {
         T ParseFile<T>(string filename);

    }
}
