using Microsoft.Extensions.FileProviders;
using System;

namespace Nombok.Core
{
   public static class GenerationContextOptionsExtensions
   {
      public static GenerationContextOptions UseCodebaseProvider(this GenerationContextOptions options, IFileProvider provider)
      {
         options.CodebaseFileProvider = provider ?? throw new ArgumentNullException(nameof(provider));
         return options;
      }

      public static GenerationContextOptions UseCodebaseFolder(this GenerationContextOptions options, string root)
      {
         if (string.IsNullOrWhiteSpace(root))
            throw new ArgumentException($"Bad value for the {nameof(root)} argument");
         return options.UseCodebaseProvider(new PhysicalFileProvider(root));
      }

      public static IFileProvider BuildCodebaseProvider(this GenerationContextOptions options)
      {
         options = options ?? throw new ArgumentNullException(nameof(options));
         return options.CodebaseFileProvider;
      }
   }

}

