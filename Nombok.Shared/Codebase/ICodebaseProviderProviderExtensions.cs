using Microsoft.Extensions.FileProviders;
using Nombok.Shared.FileSystem;
using System;
using System.Collections.Generic;

namespace Nombok.Shared.Codebase
{
   public static class ICodebaseProviderProviderExtensions
   {
      public static IEnumerable<IFileInfo> Enumerate(this ICodebaseProvider provider) 
         => provider.Enumerate(new FileSearchOptions());
   }
}