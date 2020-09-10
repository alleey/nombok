namespace Nombok.Parser.Roslyn
{
    public class RoslynCodeParserOptions
    {
        public int LanguageVersion {get;set;} = (int)Microsoft.CodeAnalysis.CSharp.LanguageVersion.Latest;
        public bool IsScript {get;set;} = false;
    }
}

