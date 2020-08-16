using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Nombok
{
    public interface ICodeGenAction
    {
        void Execute();
    }
}

