using System.Collections.Generic;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace Nombok.Shared.FileSystem
{
   public interface IFileSystemProvider
   {
      IEnumerable<string> Enumerate(FileSearchOptions options);
   }
}
