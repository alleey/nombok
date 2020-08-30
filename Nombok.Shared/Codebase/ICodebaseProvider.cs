using Microsoft.Extensions.FileProviders;
using Nombok.Shared.FileSystem;
using System;
using System.Collections.Generic;

namespace Nombok.Shared.Codebase
{
   public interface ICodebaseProvider
   {
      IEnumerable<IFileInfo> Enumerate(FileSearchOptions options);
   }
}