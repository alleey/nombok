using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nombok.Parser.Roslyn
{
    public static class DebugSyntaxExtensions
    {
        private const int IndentSpaces = 2;

        public static void Debug(this PropertyDeclarationSyntax syn, int indent = 0)
        {
            var padding = "".PadLeft(indent * IndentSpaces);
            Console.WriteLine($"{padding}Property: {syn.Identifier.Text}");
            Console.WriteLine($"{padding} Modifiers: {syn.Modifiers}");
            foreach(var ac in syn.AccessorList.Accessors) {
                ac.Debug(1+indent);
            }
        }

        public static void Debug(this AccessorDeclarationSyntax syn, int indent = 0)
        {
            var padding = "".PadLeft(indent * IndentSpaces);
            Console.WriteLine($"{padding}Keyword: {syn.Keyword}");
            Console.WriteLine($"{padding} Modifiers: {syn.Modifiers}");
        }
    }
}

