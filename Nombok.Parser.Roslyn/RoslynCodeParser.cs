using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Nombok.Parser.Roslyn
{
    public class RoslynCodeParser : IRoslynSourceParser
    {
        private bool _disposedValue;
        private readonly ILogger _logger;
        private readonly RoslynCodeParserOptions _options;

        public RoslynCodeParser(IOptions<RoslynCodeParserOptions> options, ILogger<RoslynCodeParser> logger)
        {
            _options = (options ?? throw new ArgumentNullException(nameof(options))).Value;
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

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public SyntaxTree ParseText(string source)
        {
            var parserOptions = new CSharpParseOptions(
                _options.LanguageVersion,
                _options.DocumentationMode,
                _options.IsScript ? SourceCodeKind.Script : SourceCodeKind.Regular
            );
            return CSharpSyntaxTree.ParseText(source, parserOptions);
        }
    }
}

