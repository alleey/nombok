

using System;
using System.Collections.Generic;

namespace Nombok.Parser
{
    public interface ISourceParser<T> : IDisposable
    {
        T ParseText(string source);
    }
}

