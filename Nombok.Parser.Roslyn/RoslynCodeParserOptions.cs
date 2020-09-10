using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Nombok.Parser.Roslyn
{
    public class RoslynCodeParserOptions
    {
      public LanguageVersion LanguageVersion {get;set;} = LanguageVersion.Latest;
      public bool IsScript { get; set; } = false;
      public DocumentationMode DocumentationMode { get; set; } = DocumentationMode.None;
   }
}

