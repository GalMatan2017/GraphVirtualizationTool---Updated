﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVirtualizationTool
{
    interface FileHandlerInterface
    {
        List<List<bool>> ParseFile(string filename);
    }
}
