using System;
using System.Collections.Generic;

namespace Nombok.Core.Codebase
{
   public class CodebaseSearchOptions
   {
      public StringComparison ComparsionOption { get; set; } = StringComparison.InvariantCultureIgnoreCase;
      public HashSet<string> IncludePatterns { get; } = new HashSet<string>();
      public HashSet<string> ExcludePatterns { get; } = new HashSet<string>();
   }

   public static class CodebaseSearchOptionsExtensions
   {
      public static CodebaseSearchOptions AddIncludePatterns(this CodebaseSearchOptions options, IEnumerable<string> patterns)
      {
         patterns = patterns ?? throw new ArgumentNullException(nameof(patterns));
         foreach (var pattern in patterns)
            options.IncludePatterns.Add(pattern);
         return options;
      }

      public static CodebaseSearchOptions AddExcludePatterns(this CodebaseSearchOptions options, IEnumerable<string> patterns)
      {
         patterns = patterns ?? throw new ArgumentNullException(nameof(patterns));
         foreach (var pattern in patterns)
            options.ExcludePatterns.Add(pattern);
         return options;
      }
   }
}
