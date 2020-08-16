using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nombok.Parser.Roslyn
{
    public static partial class SyntaxExtensions
    {
        public const string kGet = "get";
        public const string kPartial = "partial";
        public const string kStatic = "static";

        public static bool Contains(this SyntaxTokenList list, string token) 
            => list.Where(x => x.Text == token).Any();

        public static bool HasPartialModifier(this MemberDeclarationSyntax syn) => syn.Modifiers.Contains(kPartial);
        public static bool HasStaticModifier(this MemberDeclarationSyntax syn) => syn.Modifiers.Contains(kStatic);
        public static bool HasPrivateModifier(this MemberDeclarationSyntax syn) => syn.Modifiers.Contains("private");
        public static bool HasProtectedModifier(this MemberDeclarationSyntax syn) => syn.Modifiers.Contains("protected");
        public static bool HasPublicModifier(this MemberDeclarationSyntax syn) => syn.Modifiers.Contains("public");
        public static bool HasReadonlyModifier(this MemberDeclarationSyntax syn) => syn.Modifiers.Contains("readonly");

        public static AttributeSyntax FindAttribute(this MemberDeclarationSyntax syn, string token) 
            => syn.AttributeLists.FindAttribute(token);

        public static IEnumerable<FieldDeclarationSyntax> Fields(this SyntaxNode syn) 
            => syn.DescendantNodes().OfType<FieldDeclarationSyntax>();

        public static IEnumerable<MethodDeclarationSyntax> Methods(this SyntaxNode syn) 
            => syn.DescendantNodes().OfType<MethodDeclarationSyntax>();


        public static IEnumerable<PropertyDeclarationSyntax> Properties(this SyntaxNode syn) 
            => syn.DescendantNodes().OfType<PropertyDeclarationSyntax>();

        public static AttributeSyntax FindAttribute(this SyntaxList<AttributeListSyntax> syn, string token) 
            => syn.Where(x => x.FindAttribute(token) != null).FirstOrDefault()?.FindAttribute(token);

        public static AttributeSyntax FindAttribute(this AttributeListSyntax syn, string token) 
            => syn.Attributes.Where(x => x.Name.NormalizeWhitespace().ToFullString() == token).FirstOrDefault();

    }
}

