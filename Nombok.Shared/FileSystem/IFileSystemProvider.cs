using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace Nombok.Shared.FileSystem
{
    public interface IFileSystemProvider
   {
      FileSearchResult Enumerate(FileSearchOptions options);
   }
}
