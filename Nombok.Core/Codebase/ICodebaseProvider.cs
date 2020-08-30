using Microsoft.Extensions.FileProviders;
using System;

namespace Nombok.Core.Codebase
{
   public interface ICodebaseProvider
   {
      void Enumerate(string folder, CodebaseSearchOptions options, Action<IFileInfo> handler);
   }
}