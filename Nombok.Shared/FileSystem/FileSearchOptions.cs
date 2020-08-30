using System;
using System.Collections.Generic;

namespace Nombok.Shared.FileSystem
{
    public class FileSearchOptions
   {
      public string BaseFolder { get; set; }
      public StringComparison ComparsionOption { get; set; } = StringComparison.InvariantCultureIgnoreCase;
      public HashSet<string> IncludePatterns { get; } = new HashSet<string>();
      public HashSet<string> ExcludePatterns { get; } = new HashSet<string>();
   }
}
