using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Nombok.Parser.Roslyn
{
    public class RoslynCodeParser : IRoslynSourceParser
    {
        private bool _disposedValue;
        private ILogger _logger;

        public RoslynCodeParser(IOptions<RoslynCodeParserOptions> options, ILogger<RoslynCodeParser> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RoslynCodeParser()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

