using System;
using System.Collections.Generic;

namespace Nombok.Shared.FileSystem
{
   public static class FileSearchOptionsExtensions
   {
      public static FileSearchOptions UseBaseFolder(this FileSearchOptions options, string folder)
      {
         options = options ?? throw new ArgumentNullException(nameof(options));
         options.BaseFolder = folder ?? throw new ArgumentNullException(nameof(folder));
         return options;
      }

      public static FileSearchOptions AddIncludePatterns(this FileSearchOptions options, IEnumerable<string> patterns)
      {
         options = options ?? throw new ArgumentNullException(nameof(options));
         patterns = patterns ?? throw new ArgumentNullException(nameof(patterns));
         foreach (var pattern in patterns)
            options.IncludePatterns.Add(pattern);
         return options;
      }

      public static FileSearchOptions AddExcludePatterns(this FileSearchOptions options, IEnumerable<string> patterns)
      {
         options = options ?? throw new ArgumentNullException(nameof(options));
         patterns = patterns ?? throw new ArgumentNullException(nameof(patterns));
         foreach (var pattern in patterns)
            options.ExcludePatterns.Add(pattern);
         return options;
      }
   }
}
