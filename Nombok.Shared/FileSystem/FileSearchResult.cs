using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Nombok.Shared.FileSystem
{
    public class FileSearchResult
   {
      public FileSearchResult() {}
      public FileSearchResult(IEnumerable<IFileInfo> fileInfos, bool matches)
      {
          Files = fileInfos;
          HasMatches = matches;
      }
      
      public IEnumerable<IFileInfo> Files { get; }
      public bool HasMatches { get; }
   }
}
