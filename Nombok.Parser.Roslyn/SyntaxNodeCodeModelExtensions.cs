using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nombok.Parser.Roslyn
{
    interface ICodeModel
    {
    }

    class CodeTypeModel : ICodeModel {
        public string Name {get;set;}
    }

    static class SyntaxNodeCodeModelExtensions
    {
        public static CodeTypeModel FromSyntaxNode(TypeDeclarationSyntax syn) 
        {
            var model = new CodeTypeModel() {
                Name = syn.Identifier.Text
            };
            return model;
        }
    }
}

