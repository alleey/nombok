using Microsoft.Extensions.FileProviders;
using System;

namespace Nombok.Core.Codebase
{
   public static class ICodebaseProviderProviderExtensions
   {
      public static void Enumerate(this ICodebaseProvider provider, string folder, Action<IFileInfo> handler) 
         => provider.Enumerate(folder, new CodebaseSearchOptions(), handler);
   }
}
